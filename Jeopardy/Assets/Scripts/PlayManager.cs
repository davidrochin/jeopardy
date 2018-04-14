using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    Question[] questions;
    BoardBuilder gridBuilder;

	void Awake () {
        //StreamReader reader = new StreamReader("Assets/preguntas.csv", System.Text.Encoding.UTF8, true);
        questions = Question.FromCSV();
        gridBuilder = FindObjectOfType<BoardBuilder>();
	}
	
}
