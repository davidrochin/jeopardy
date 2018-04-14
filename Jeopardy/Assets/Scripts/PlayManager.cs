using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    Question[] questions;
    Board board;

	void Awake () {
        //StreamReader reader = new StreamReader("Assets/preguntas.csv", System.Text.Encoding.UTF8, true);
        questions = Question.FromCSV();
        board = FindObjectOfType<Board>();
    }

    void Start() {
        //board.cells[5, 0].SetActive(false);
    }

}

