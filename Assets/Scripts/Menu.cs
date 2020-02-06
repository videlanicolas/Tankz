using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Play() {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        gameController.GetComponent<UserInput>().enabled = true;
        canvas.SetActive(false);
    }
}
