using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody _rb;


    private void Start()
    {
        attack();
    }

    private void attack()
    {
     
        _rb.AddForce(transform.parent.forward*bulletSpeed);
        Invoke("destroyBullet",4);
        
    }

    private void destroyBullet()
    {
        Destroy(gameObject);
        
    }

    

    
}
