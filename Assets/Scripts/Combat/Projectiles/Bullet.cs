using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public float speed = 10f;
    public Transform line;
    public GameObject effectPrefab;

    private Rigidbody rigid;
   
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if velocity is not zero
        if(rigid.velocity.magnitude > 0f)
        {
            // Rotate using LookRotation
            line.rotation = Quaternion.LookRotation(rigid.velocity);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // If there is an attached effect
        if(effectPrefab)
        {
            // Get the contact point
            ContactPoint contact = col.contacts[0];
            // Spawn the effect - and rotate to contact normal
            Instantiate(effectPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        }
        //Destroy self
        Destroy(gameObject);
    }

    public override void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        rigid.AddForce(direction * speed, ForceMode.Impulse);
        line.position = lineOrigin;
    }
}
