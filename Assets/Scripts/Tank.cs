using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject bulletPrefab;

    private GameObject hull, barrel;
    private Transform pivot, spawnPoint;
    private bool charge = false;
    private float power = 0f,
                  maxPower = 10f,
                  powerMultiplier = 8f,
                  forward = 1f;

    private void Awake()
    {
        hull = gameObject;
        pivot = gameObject.transform.GetChild(3).gameObject.transform;
        barrel = pivot.GetChild(0).gameObject;
        spawnPoint = barrel.transform.GetChild(0).transform;

        forward = ((gameObject.transform.GetChild(4).gameObject.transform.position - hull.transform.position).x > 0) ? 1f : -1f;
    }

    void Operate()
    {
        float horizontalButton = Input.GetAxis("Horizontal");
        float verticalButton = Input.GetAxis("Vertical");
        bool fire1Down = Input.GetButtonDown("Fire1"),
             fire1Up = Input.GetButtonUp("Fire1");

        this.Move(forward * horizontalButton * 0.1f);
        this.Aim(forward * verticalButton);

        if (fire1Down)
        {
            charge = true;
        }

        if (charge)
        {
            power += Time.deltaTime * powerMultiplier;
            power = Mathf.Clamp(power, 0.0f, maxPower);
            if (fire1Up || power >= maxPower)
            {
                charge = false;
                Debug.Log("Bullet fired with power: " + power);
                GameObject bullet = Fire(bulletPrefab, power);
                power = 0;
                this.SendMessageUpwards("PlayerEndTurn", bullet, SendMessageOptions.RequireReceiver);
            }
        }
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
