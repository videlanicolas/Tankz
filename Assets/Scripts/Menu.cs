using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    GameObject gameController, panel, player1ui, player2ui;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        panel = GameObject.FindGameObjectWithTag("MenuPanel");
        player1ui = GameObject.FindGameObjectWithTag("Player1Life");
        player2ui = GameObject.FindGameObjectWithTag("Player2Life");
        player1ui.SetActive(false);
        player2ui.SetActive(false);
    }

    public void Play() {
        gameController.GetComponent<UserInput>().enabled = true;
        player1ui.SetActive(true);
        player2ui.SetActive(true);
        panel.SetActive(false);
    }
}
