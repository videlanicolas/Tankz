using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { 
    Menu,
    Player1Turn,
    Player2Turn,
    Fired,
    WaitingForThingsToStop,
    NothingMoving,
}

enum Turn { 
    Player1,
    Player2,
}

public class UserInput : MonoBehaviour
{
    public GameObject tankPrefab;

    private GameObject player1, player2, bullet;
    private float thresholdSpeed = 0.001f;
    private State state = State.Menu, prevState = State.Menu;
    private Turn turn = Turn.Player1;

    // Start is called before the first frame update
    void Start()
    {

        this.player1 = Instantiate(this.tankPrefab, new Vector3(-40f, -6f), new Quaternion(0, 0, 0, 0));
        this.player2 = Instantiate(this.tankPrefab, new Vector3(40f, -6f), Quaternion.Euler(180, 0, 180));

        player1.GetComponent<Tank>().SetLifeUI("Player1Life");
        player2.GetComponent<Tank>().SetLifeUI("Player2Life");

        //After Menu, it's player1 turn
        state = State.Player1Turn;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalButton = Input.GetAxis("Horizontal");
        float verticalButton = Input.GetAxis("Vertical");
        bool fire1Down = Input.GetButtonDown("Fire1"),
             fire1Up = Input.GetButtonUp("Fire1");

        if (prevState != state) {
            prevState = state;
            Debug.Log("State: " + state);
        }

        switch (state) {
            case State.Player1Turn:
                bullet = player1.GetComponent<Tank>().Operate(horizontalButton, verticalButton, fire1Down, fire1Up);
                if (bullet != null) state = State.Fired;
                break;
            case State.Player2Turn:
                bullet = player2.GetComponent<Tank>().Operate(horizontalButton, verticalButton, fire1Down, fire1Up);
                if (bullet != null) state = State.Fired;
                break;
            case State.Fired:
                if (bullet == null) state = State.WaitingForThingsToStop;
                break;
            case State.WaitingForThingsToStop:
                if (CheckIfThingsStoppedMoving()) {
                    state = State.NothingMoving;
                }
                break;
            case State.NothingMoving:
                if (turn == Turn.Player1) {
                    turn = Turn.Player2;
                    state = State.Player2Turn;
                }
                else {
                    turn = Turn.Player1;
                    state = State.Player1Turn;
                }
                break;
            default:
                Debug.LogError("Unknown state " + state);
                break;
        }
    }

    bool CheckIfThingsStoppedMoving() {
        if (this.player1.GetComponent<Rigidbody>().velocity.magnitude < thresholdSpeed &&
            this.player2.GetComponent<Rigidbody>().velocity.magnitude < thresholdSpeed) {
            return true;
        }
        return false;
    }
}
