using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;

    [Range(0.0f, 10.0f)]
    public float forceMultiplier = 1.0f;

    private GameObject player;
    private GameObject player1Barrel;
    private GameObject player1Pivot;
    private float bulletTTL = 10.0f;

    public float lowLimit = -45f, highLimit = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player1Barrel = GameObject.FindGameObjectWithTag("Player1Barrel");
        player1Pivot = GameObject.FindGameObjectWithTag("Player1Pivot");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalButton = Input.GetAxis("Horizontal");
        float verticalButton = Input.GetAxis("Vertical");
        bool fire1 = Input.GetButtonDown("Fire1");
        //Debug.Log("verticalButton: " + verticalButton);
        //Debug.Log("fire1: " + fire1);
        //Debug.Log("player1Barrel.transform.rotation.z: " + player1Barrel.transform.rotation.z);

        player.GetComponent<Transform>().Translate(horizontalButton * 0.1f, 0, 0);
        player1Barrel.GetComponent<Transform>().RotateAround(
                    player1Pivot.transform.position,
                    Vector3.forward,
                    angle: verticalButton);

        if (fire1) {
            Debug.Log("Fire!");
            GameObject bullet;
            bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(
                forceMultiplier * (spawnPoint.position - player1Pivot.transform.position), 
                ForceMode.Impulse);
            Destroy(bullet, bulletTTL);
        }
    }
}
