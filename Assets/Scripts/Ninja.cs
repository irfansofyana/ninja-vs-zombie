using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ninja : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Rigidbody2D myRigidBody;
    private bool facingRight;
    private bool attack;
    private bool shoot;
    private Animator myAnimator;

    [SerializeField]
    private Transform[] groundPoints;

    
    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;
    private bool jump;
    
    [SerializeField]
    private bool airControl;
    
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private GameObject kunaiPrefab;

    public static float health = 100;
    public static int score;
    
    public int damageZombie = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        if(myRigidBody == null) {
            Debug.LogError("Player::Start cant find RigidBody2D </sadface>");
        }
        facingRight = true;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        if (isNinjaDead()){
            SoundManager.PlaySound("ninjaDead");
            SoundManager.PlaySound("ninjaLose");
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene");
        }
        HandleInput();
    }

    void FixedUpdate() {
        float horizontalMove = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();
        
        HandleMovement(horizontalMove);
        
        FlipHandle(horizontalMove);

        HandleAttacks();

        HandleShoot();

        HandleLayers();

        ResetValues();
    }

    void HandleMovement(float horizontalMove){
        if (myRigidBody.velocity.y < 0){
            myAnimator.SetBool("land", true);
        }
        if (isGrounded && jump) {
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
        }
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            myRigidBody.velocity = new Vector2(horizontalMove * moveSpeed, myRigidBody.velocity.y);
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontalMove));
    }

    void HandleAttacks(){
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            myAnimator.SetTrigger("attack");
            myRigidBody.velocity = Vector2.zero;
        }
    }

    void HandleShoot(){
        if (shoot && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Shoot")){
            myAnimator.SetTrigger("shoot");
            myRigidBody.velocity = Vector2.zero;
        }
    }


    void HandleInput(){
        if (Input.GetKeyDown(KeyCode.Q)){
            attack = true;
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.E)){
            shoot = true;
            shootKunai(0);
            SoundManager.PlaySound("shoot");
        }
    }

    void FlipHandle(float horizontalMove){
        if (horizontalMove > 0 && !facingRight || horizontalMove <  0 && facingRight){
            facingRight = !facingRight;
            
            Vector3 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }
    }

    void ResetValues(){
        attack = false;
        jump = false;
        shoot = false;
    }

    bool IsGrounded(){
        if (myRigidBody.velocity.y <= 0){
            foreach (Transform point in groundPoints){
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; ++i){
                    if (colliders[i].gameObject != gameObject){
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void HandleLayers(){
        if (!isGrounded){
            myAnimator.SetLayerWeight(1, 1);
        }else{
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    void shootKunai(int val){
        Vector3 pos = transform.position;
        if (facingRight){
            pos.x += 0.5f;
            GameObject tmp = (GameObject)Instantiate(kunaiPrefab, pos, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Shoot>().Initialize(Vector2.right);
        }else{
            pos.x -= 0.5f;
            GameObject tmp = (GameObject)Instantiate(kunaiPrefab, pos, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Shoot>().Initialize(Vector2.left);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag.Equals("Enemy")){
            SoundManager.PlaySound("zombieAttack");
            health -= damageZombie;
        }else if (col.gameObject.tag.Equals("Monster")){
            health -= 5*damageZombie;
            SoundManager.PlaySound("zombieAttack");
        }
    }

    bool isNinjaDead(){
        return (health <= 0 || transform.position.y <= -5f);
    }
}
