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

        if (Input.GetKeyDown(KeyCode.R)) {
            GameObject textObject = transform.Find("question_text").gameObject;
            textObject.GetComponent<Text>().text = question.answer;
            GetComponent<Image>().color = new Color32(165, 28, 28, 255);
        }

        bool answered = false;

        //Si se presiona C, asignarle los puntos al equipo actual y pasar de turno
        if (Input.GetKeyDown(KeyCode.C)) {
            PlayManager playManager = FindObjectOfType<PlayManager>();
            playManager.teamInTurn.score += question.value;
            answered = true;
        }

        //Si se presiona I, restarle los puntos al equipo actual y pasar de turno
        if (Input.GetKeyDown(KeyCode.I)) {
            PlayManager playManager = FindObjectOfType<PlayManager>();
            playManager.teamInTurn.score -= question.value;
            answered = true;
        }

        if (answered) {
            PlayManager playManager = FindObjectOfType<PlayManager>();

            //Buscar la celda con la pregunta y desactivarla
            foreach (Cell cell in FindObjectsOfType<Cell>()) {
                if(cell.question == question) { cell.SetActive(false); break; }
            }

            playManager.FinishTurn();

            //Destruir el panel de pregunta
            Destroy(gameObject);
        }

    }
}
