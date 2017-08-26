using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {
    public GUIStyle lblScorePlayer;
    public GUIStyle lblScoreComputer;
    public GUIStyle styleWin;
    public GUIStyle styleLose;
    [HideInInspector] public static int scorePlayer = 0;
    [HideInInspector] public static int scoreComputer = 0;
    private int gameEnd = 0;

    Rect lblScorePlayerRect = new Rect (Screen.width * 0.15f, Screen.height * 0.15f, Screen.width * 0.2f, Screen.height * 0.2f);
    Rect lblScoreComputerRect = new Rect (Screen.width * 0.80f, Screen.height * 0.15f, Screen.width * 0.2f, Screen.height * 0.2f);
    Rect playerRect = new Rect (0.0f, 0.0f, Screen.width * 0.5f, Screen.height);
    Rect computerRect = new Rect (Screen.width * 0.5f, 0.0f, Screen.width * 0.5f, Screen.height);

    private void Update () {
        // Someone won
        if (scorePlayer >= 10 || scoreComputer >= 10) {
            // Player won
            if (scorePlayer >= 10) {
                gameEnd = -1;
            // Computer won
            } else {
                gameEnd = 1;
            }
        }
    }

    private void OnGUI () {
        GUI.Label (lblScorePlayerRect, scorePlayer.ToString (), lblScorePlayer);
        GUI.Label (lblScoreComputerRect, scoreComputer.ToString (), lblScoreComputer);

        if (gameEnd != 0) {
            if (gameEnd == -1) {
                GUI.Label (playerRect, "WIN", styleWin);
                GUI.Label (computerRect, "LOSE", styleLose);
            } else {
                GUI.Label (playerRect, "LOSE", styleLose);
                GUI.Label (computerRect, "WIN", styleWin);
            }
        }
    }
}
