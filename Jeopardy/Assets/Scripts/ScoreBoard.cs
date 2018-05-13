using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

	void Start () {
        PlayManager.instance.OnFinishTurn += UpdateScore;
        PlayManager.instance.OnGameStart += UpdateScore;
        ConfigPanel.OnConfigUpdate += UpdateScore;
    }

    void UpdateScore() {
        GameObject scoreboardA = transform.Find("scoreboard_A").gameObject;
        GameObject scoreboardB = transform.Find("scoreboard_B").gameObject;

        scoreboardA.transform.Find("score").GetComponent<Text>().text = "" + PlayManager.instance.teamA.score;
        scoreboardB.transform.Find("score").GetComponent<Text>().text = "" + PlayManager.instance.teamB.score;

        scoreboardA.transform.Find("name").GetComponent<Text>().text = "" + PlayManager.instance.teamA.name;
        scoreboardB.transform.Find("name").GetComponent<Text>().text = "" + PlayManager.instance.teamB.name;
    }
}
