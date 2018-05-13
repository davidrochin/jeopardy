using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour {

    public Question question;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Si se presiona la tecla R, mostrar la respuesta
        if (Input.GetKeyDown(KeyCode.R)) {
            GameObject textObject = transform.Find("question_text").gameObject;
            textObject.GetComponent<Text>().text = question.answer;
            GetComponent<Image>().color = new Color32(165, 28, 28, 255);
        }

        //Si se presiona la tecla O, mostrar las opciones
        if (Input.GetKeyDown(KeyCode.O) && question.options.Length > 1) {
            GameObject textObject = transform.Find("question_text").gameObject;

            string optionsText = "-" + question.options[0];
            for (int i = 1; i < question.options.Length; i++) {
                optionsText += "\n-" + question.options[i];
            }
            textObject.GetComponent<Text>().text = optionsText;
            GetComponent<Image>().color = new Color32(61, 168, 34, 255);
        }

        //Si se presiona la tecla P, volver a mostrar la pregunta
        if (Input.GetKeyDown(KeyCode.P)) {
            question.Show();
            Destroy(this.gameObject);
        }

        bool answered = false;

        //Si se presiona C, asignarle los puntos al equipo actual y pasar de turno
        if (Input.GetKeyDown(KeyCode.C)) {
            PlayManager playManager = FindObjectOfType<PlayManager>();
            playManager.teamInTurn.score += question.value;
            question.active = false;
            answered = true;
        }

        //Si se presiona I, restarle los puntos al equipo actual y pasar de turno
        if (Input.GetKeyDown(KeyCode.I)) {
            PlayManager playManager = FindObjectOfType<PlayManager>();
            playManager.teamInTurn.score -= question.value;
            question.active = false;
            answered = true;
        }

        if (answered) {
            PlayManager playManager = FindObjectOfType<PlayManager>();

            //Analizar las celdas. Si se encuentra una sin preguntas activas, desactivar toda la celda
            foreach (Cell cell in FindObjectsOfType<Cell>()) {
                if(cell.HasActiveQuestions() == false) { cell.SetActive(false); }
            }

            playManager.FinishTurn();

            //Destruir el panel de pregunta
            Destroy(gameObject);
        }

    }
}
