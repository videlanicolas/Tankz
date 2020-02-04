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

    private GameObject player1, player2;
    private bool charge = false;
    private float power = 1.0f,
                  thresholdSpeed = 0.001f,
                  bulletTimeToLive = 60f;
    private State state = State.Menu, prevState = State.Menu;
    private Turn turn = Turn.Player1;

    // Start is called before the first frame update
    void Start()
    {

        this.player1 = Instantiate(this.tankPrefab, new Vector3(-40f, -6f), new Quaternion(0, 0, 0, 0), this.transform);
        this.player2 = Instantiate(this.tankPrefab, new Vector3(40f, -6f), Quaternion.Euler(180, 0, 180), this.transform);

        //After Menu, it's player1 turn
        state = State.Player1Turn;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject bullet = null;
        if (prevState != state) {
            prevState = state;
            Debug.Log("State: " + state);
        }

        switch (state) {
            case State.Player1Turn:
                this.player1.SendMessage("Operate");
                break;
            case State.Player2Turn:
                this.player2.SendMessage("Operate");
                break;
            case State.Fired:
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

    IEnumerator WaitForBulletDestroyed(GameObject bullet) {
        yield return bullet != null;
        // Bullet got destroyed
        state = State.WaitingForThingsToStop;
        
        // Stop coroutine
        yield break;
    }

    void PlayerEndTurn(GameObject bullet) {
        state = State.Fired;
        Destroy(bullet, this.bulletTimeToLive);
        StartCoroutine(WaitForBulletDestroyed(bullet));
    }

    bool CheckIfThingsStoppedMoving() {
        if (this.player1.GetComponent<Rigidbody>().velocity.magnitude < thresholdSpeed &&
            this.player2.GetComponent<Rigidbody>().velocity.magnitude < thresholdSpeed) {
            return true;
        }
        return false;
    }
}
