using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shoot : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D myRigidBody;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        myRigidBody.velocity = direction * speed;
    }

    public void Initialize(Vector2 direction){
        this.direction = direction;
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("Land") || col.gameObject.tag.Equals("Monster")){
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible(){
        Destroy(gameObject);
    }

}
