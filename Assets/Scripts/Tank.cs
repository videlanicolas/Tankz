﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletTTL = 60f;
    public Color startColor = Color.cyan,
                 endColor = Color.red;

    private GameObject hull, barrel, lifeUI, powerUI;
    private Transform pivot, spawnPoint;
    private bool charge = false;
    private float power = 0f,
                  maxPower = 10f,
                  powerMultiplier = 8f,
                  forward = 1f,
                  maxLife,
                  life = 100f;

    public void SetPlayerNumber(int number)
    {
        lifeUI = GameObject.FindGameObjectWithTag("Player" + number + "Life");
        powerUI = GameObject.FindGameObjectWithTag("Player" + number + "Power");
    }

    private void Awake()
    {
        hull = gameObject;
        pivot = gameObject.transform.GetChild(3).gameObject.transform;
        barrel = pivot.GetChild(0).gameObject;
        spawnPoint = barrel.transform.GetChild(0).transform;
        maxLife = life;

        forward = ((gameObject.transform.GetChild(4).gameObject.transform.position - hull.transform.position).x > 0) ? 1f : -1f;
    }

    private void Update()
    {
        lifeUI.GetComponent<Slider>().value = (float)(life*100/maxLife);
        powerUI.GetComponent<Slider>().value = (float)(power*100/maxPower);
        powerUI.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = GetPowerColor();
    }

    private Color GetPowerColor() {
        return Color.Lerp(startColor, endColor, (float)(power / maxPower));
    }
    public GameObject Operate(float horizontalButton, float verticalButton, bool fire1Down, bool fire1Up)
    {
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
                return bullet;
            }
        }
        return null;
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

    public void Hit()
    {
        life -= 10f;
    }

    public GameObject Fire(GameObject bulletPrefab, float power) {
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(
            power * (this.spawnPoint.position - this.pivot.position),
            ForceMode.Impulse);
        Destroy(bullet, bulletTTL);
        return bullet;
    }
}
