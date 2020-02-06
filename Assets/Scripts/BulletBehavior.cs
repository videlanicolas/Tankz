using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            var exp = GetComponent<ParticleSystem>();
            var mesh = GetComponent<MeshRenderer>();
            mesh.enabled = false;
            exp.Play();
            Destroy(gameObject, exp.main.duration);
        }
    }
}
