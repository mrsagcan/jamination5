using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrailParticleSystem : MonoBehaviour
{
    public GameObject bulletToBeFollowed;
    public GameObject bulletFirePS;
    private bool hasFired;

    private void Awake()
    {
        hasFired = true;
    }

    void Update()
    {
        if(bulletToBeFollowed != null)
        {
            if (hasFired){

                Debug.Log("BUM!");
                //Instantiate(bulletFirePS, bulletToBeFollowed.transform.position, bulletToBeFollowed.transform.rotation);
                hasFired = false;
            }
            transform.position = bulletToBeFollowed.transform.position;
            transform.rotation = bulletToBeFollowed.transform.rotation;
        }
        else
        {
            var em = gameObject.transform.GetComponent<ParticleSystem>().emission;
            em.enabled = false;
        }
    }
}
