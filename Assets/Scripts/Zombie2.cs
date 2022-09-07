using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie2 : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public int health = 30;
    public int damageKunai = 6;
    private Transform target;
    private Rigidbody2D myRigidBody;
    private bool facingRight;
    private Animator myAnimator;
    private bool attack;
    public static int scoreNinja;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        if(myRigidBody == null) {
            Debug.LogError("Player::Start cant find RigidBody2D </sadface>");
        }
        if (GameObject.FindGameObjectWithTag("Ninja") == null){
            target = null;
        }else {
            target = GameObject.FindGameObjectWithTag("Ninja").GetComponent<Transform>();
        }
        myAnimator = GetComponent<Animator>();
        facingRight = false;
        attack = false;
        myAnimator.SetTrigger("move");
    }

    bool isMoveRight(Vector3 pos, Vector3 zombie){
        float px = pos.x;
        float py = pos.y;
        float zx = zombie.x;
        float zy = zombie.y;
        return (px < zx);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            scoreNinja+=3;
            Destroy(gameObject);
        }
        if (transform.position.y <= -5f){
            Destroy(gameObject);
        }
        if (target != null){
            float distance = Mathf.Abs(transform.position.x - target.position.x);
            if (distance < 5f && !isMoveRight(transform.position, target.position) && facingRight){
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                facingRight = false;

                transform.localScale = scale;
            }else if (distance < 5f && isMoveRight(transform.position, target.position) && !facingRight){
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                facingRight = true;

                transform.localScale = scale;
            }
            
            if (distance < 5f && !attack){
                transform.position =  Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }

            ResetValues();
        }
    }

    void FixedUpdate(){
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag.Equals("Kunai")){
            health -= damageKunai;
            myRigidBody.velocity = Vector2.zero;
        }
        else if (col.gameObject.tag.Equals("Ninja")){
            attack = true;
            if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
                myAnimator.SetTrigger("attack");
                myRigidBody.velocity = Vector2.zero;
            }
        }
    }

    void ResetValues(){
        attack = false;
    }
}
