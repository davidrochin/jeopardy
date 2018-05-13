using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeboard : MonoBehaviour {

    Text timeText;

    private void Start() {
        timeText = transform.Find("time").GetComponent<Text>();
    }

    void Update () {
        float timeLeft = PlayManager.instance.timeLeft;
        timeText.text = ((int)timeLeft / 60) +  ":" + (((int)timeLeft % 60) <= 9 ? "0" : "") + ((int)timeLeft % 60);
	}
}
