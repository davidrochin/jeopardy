using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    public static PlayManager instance;

    string[,] categories = { 
        { "Cultura general", "Anatomía I-II", "Bioquímica I-II", "Histología", "Embriología" },
        { "Cultura general", "Fisiología I-II", "Propedeutica I-II", "Microbiología", "Biología Molecular" },
        { "Cultura general", "Hematología", "Farmacología I-II", "Gastroenterología", "Infectología" },
        { "Cultura general", "Cardiología", "Urología", "Neurología", "Oncología" }
    };

    Board board;

    public Team teamA;
    public Team teamB;

    public Team teamInTurn;

    public int level = 1;

    //Eventos
    public event Action OnFinishTurn;
    public event Action OnGameStart;

    void Awake () {

        //Asegurarse que sea la unica instancia
        if(instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }

        board = FindObjectOfType<Board>();

        //Iniciar los equipos
        teamA = new Team("Equipo A");
        teamB = new Team("Equipo B");
        teamInTurn = teamA;
    }

    void Start() {

        //Popular el tablero con las preguntas
        board.Populate(1);

        if(OnGameStart != null) OnGameStart();

    }

    private void Update() {

        //Escuchar las teclas para añadir y restar puntaje
        /*if (Input.GetKeyDown(KeyCode.Keypad1)) { teamA.score += 200; }
        if (Input.GetKeyDown(KeyCode.Keypad2)) { teamB.score += 200; }
        if (Input.GetKeyDown(KeyCode.Keypad4)) { teamA.score -= 200; }
        if (Input.GetKeyDown(KeyCode.Keypad5)) { teamB.score -= 200; }*/

        //Escuchar la tecla ESC para abrir el panel de configuración
        if (Input.GetKeyDown(KeyCode.Escape) && FindObjectOfType<QuestionPanel>() == null && FindObjectOfType<QuestionSelector>() == null) {
            ConfigPanel.Show();
        }

        //Version de prueba
        if (isDemo) {
            demoTimeLeft -= Time.deltaTime;
            if(demoTimeLeft <= 0f) {
                isDemo = false;
                StartCoroutine(ShowDemoOverMessage());
            }
        }

    }

    public void FinishTurn() {
        teamInTurn = (teamInTurn == teamA ? teamB : teamA);
        if(OnFinishTurn != null) OnFinishTurn();
    }

    private void OnGUI() {
        /*GUILayout.Label("[Turno] " + teamInTurn.name);
        GUILayout.Label(teamA.name + " - " + teamA.score);
        GUILayout.Label(teamB.name + " - " + teamB.score);*/
        //GUILayout.Label("Periodo de prueba, se cerrará despues de un tiempo");
        if (isDemo) {
            string demoTimeString = "" + ((int)demoTimeLeft / 60) + ":" + (((int)demoTimeLeft % 60) <= 9 ? "0" : "") + ((int)demoTimeLeft % 60);
            GUI.Box(new Rect(new Vector2(Screen.width / 2f - 350f / 2f, Screen.height - 25f), new Vector2(350f, 25f)), "Versión de demostración, se cerrará despues de " + demoTimeString);
        }
    }

    bool isDemo = true;
    float demoTimeLeft = 300f;

    IEnumerator ShowDemoOverMessage() {
        GameMessage.Show("Se acabó el periodo de demostración");
        yield return new WaitForSecondsRealtime(3f);
        Application.Quit();
    }

    public delegate void Action();

}

