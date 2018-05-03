using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    public static PlayManager instance;

    public Question[] questions;
    string[] categories = { "Anatomía", "Otra categoría", "Otra categoría más", "Categoría", "Penúltima categoría" };
    Board board;

    public Team teamA;
    public Team teamB;

    public Team teamInTurn;

    public int level;

    void Awake () {

        //Asegurarse que sea la unica instancia
        if(instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }

        board = FindObjectOfType<Board>();

        //Leer preguntas del CSV
        questions = Question.FromCSV();

        //Iniciar los equipos
        teamA = new Team("Equipo A");
        teamB = new Team("Equipo B");
        teamInTurn = teamA;
    }

    void Start() {
        //Popular el tablero con las preguntas
        board.Populate(questions, categories);
    }

    private void Update() {

        //Escuchar las teclas para añadir y restar puntaje
        /*if (Input.GetKeyDown(KeyCode.Keypad1)) { teamA.score += 200; }
        if (Input.GetKeyDown(KeyCode.Keypad2)) { teamB.score += 200; }
        if (Input.GetKeyDown(KeyCode.Keypad4)) { teamA.score -= 200; }
        if (Input.GetKeyDown(KeyCode.Keypad5)) { teamB.score -= 200; }*/

        //Escuchar la tecla ESC para abrir el panel de configuración
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ConfigPanel.Show();
        }

    }

    public void FinishTurn() {
        teamInTurn = (teamInTurn == teamA ? teamB : teamA);
    }

    private void OnGUI() {
        GUILayout.Label("[Turno] " + teamInTurn.name);
        GUILayout.Label(teamA.name + " - " + teamA.score);
        GUILayout.Label(teamB.name + " - " + teamB.score);
    }

}

