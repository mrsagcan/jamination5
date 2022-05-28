using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody _rb;
    public float bulletForce = 0.1f;

    private void Awake()
    {
        attack();
    }

    private void FixedUpdate()
    {
        //_rb.AddForce(_rb.velocity.normalized * bulletForce);
        transform.LookAt(transform.position + _rb.velocity.normalized);   
    }



    private void attack()
    {
        _rb.AddForce(transform.forward*bulletSpeed);
        Invoke("destroyBullet",4f);
        
    }

    private void destroyBullet()
    {
        Destroy(gameObject);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Bullet"))
        {
            Debug.Log("Bullet collision");
            Destroy(gameObject);
        }
        else if(collision.transform.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
