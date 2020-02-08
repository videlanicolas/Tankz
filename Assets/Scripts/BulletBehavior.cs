using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.SendMessage("Hit", SendMessageOptions.RequireReceiver);
        }
        Hit();
    }

    void Hit()
    {
        var exp = GetComponent<ParticleSystem>();
        var mesh = GetComponent<MeshRenderer>();
        var collider = GetComponent<CapsuleCollider>();
        mesh.enabled = false;
        collider.enabled = false;
        exp.Play();
        Destroy(gameObject, exp.main.duration);
    }
}
