using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private GameObject player;
    private GameObject player1Barrel;
    private GameObject player1Pivot;

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
        Debug.Log("verticalButton: " + verticalButton);
        Debug.Log("player1Barrel.transform.rotation.z: " + player1Barrel.transform.rotation.z);

        player.GetComponent<Transform>().Translate(horizontalButton * 0.1f, 0, 0);
        player1Barrel.GetComponent<Transform>().RotateAround(
                    player1Pivot.transform.position,
                    Vector3.forward,
                    angle: verticalButton);


    }
}
