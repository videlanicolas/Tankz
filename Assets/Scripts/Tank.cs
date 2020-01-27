using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : ScriptableObject
{
    private GameObject hull, barrel;
    private Transform pivot, spawnPoint;

    public Tank(GameObject gameObject, GameObject barrel, Transform pivot, Transform spawnPoint) {
        if (gameObject == null) this.hull = GameObject.FindGameObjectWithTag("Player");
        else this.hull = gameObject;

        if (pivot == null) this.pivot = GameObject.FindGameObjectWithTag("Player1Pivot").transform;
        else this.pivot = pivot;

        if (spawnPoint == null) this.spawnPoint = GameObject.FindGameObjectWithTag("Player1Spawn").transform;
        else this.spawnPoint = spawnPoint;

        if (barrel == null) this.barrel = GameObject.FindGameObjectWithTag("Player1Barrel");
        else this.barrel = barrel;
    }

    public void Move(float translate) {
        this.hull.GetComponent<Transform>().Translate(translate, 0, 0);
    }

    public void Aim(float speed) {
        this.barrel.GetComponent<Transform>().RotateAround(
                this.pivot.position,
                Vector3.forward,
                angle: speed);
    }

    public GameObject Fire(GameObject bulletPrefab, float power) {
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(
            power * (this.spawnPoint.position - this.pivot.position),
            ForceMode.Impulse);
        return bullet;
    }
}
