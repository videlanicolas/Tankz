﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{

    [Header("Player 1")]
    public GameObject tankPrefab;
    public GameObject tankBarrel;
    public GameObject tankPivot;
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    [Range(0.1f, 2.0f)]
    public float speed = 0.1f;
    public float bulletTimeToLive = 10.0f;
    [Range(1.0f, 10.0f)]
    public float maxPower = 1.0f;
    [Space]

    private Tank player1;
    private bool charge = false;
    private float power = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.player1 = new Tank(this.tankPrefab, this.tankBarrel, this.tankPivot.transform, this.bulletSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalButton = Input.GetAxis("Horizontal");
        float verticalButton = Input.GetAxis("Vertical");
        bool fire1Down = Input.GetButtonDown("Fire1"),
             fire1Up = Input.GetButtonUp("Fire1");
        //Debug.Log("verticalButton: " + verticalButton);
        //Debug.Log("fire1: " + fire1);
        //Debug.Log("player1Barrel.transform.rotation.z: " + player1Barrel.transform.rotation.z);

        this.player1.Move(horizontalButton * 0.1f);
        this.player1.Aim(verticalButton);

        if (fire1Down) {
            charge = true;
        }

        if (charge) {
            power += Time.deltaTime;
            Mathf.Clamp(power, 0.0f, this.maxPower);
        }

        if (fire1Up) {
            Debug.Log("power: " + power);
            charge = false;
            Destroy(this.player1.Fire(this.bulletPrefab, power), this.bulletTimeToLive);
        }
    }
}
