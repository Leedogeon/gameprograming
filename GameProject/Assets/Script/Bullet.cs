using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알
   public int damage;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall"){
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Floor"){
            Destroy(gameObject);
        }
    }
}
