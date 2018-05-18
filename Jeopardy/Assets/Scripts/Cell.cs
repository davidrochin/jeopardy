using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class Cell : MonoBehaviour {

    public Type type = Type.Question;
    bool cellActive = true;
    public TextMeshPro textMesh;
    public Question[] questions;

    Collider2D collider;

	// Use this for initialization
	void Awake () {

        //Crear el TextMeshPro para el texto de la celda
        GameObject textGameObject = new GameObject("Text");
        textGameObject.transform.parent = transform;
        textMesh = textGameObject.AddComponent<TextMeshPro>();
        textMesh.rectTransform.sizeDelta = transform.localScale;
        textMesh.rectTransform.position = transform.position;

        //Estilizar el TextMeshPro
        textMesh.text = "$1000";
        textMesh.enableAutoSizing = true; textMesh.fontSizeMin = 0f;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.margin = new Vector4(0.1f, 0.1f, 0.1f, 0.1f);
        textMesh.color = new Color32(255, 183, 73, 255);
        textMesh.font = Resources.Load("Fonts & Materials/Oswald Bold SDF") as TMP_FontAsset;
        textMesh.fontMaterial = Resources.Load("Fonts & Materials/Oswald Bold SDF - Drop Shadow") as Material;

        //Agregarle un collider para poder detectar clicks
        collider = gameObject.AddComponent<BoxCollider2D>();
	}

    private void OnMouseUpAsButton() {
        QuestionPanel questionPanel = FindObjectOfType<QuestionPanel>();
        if(type == Type.Question && EventSystem.current.IsPointerOverGameObject() == false && cellActive && questionPanel == null) {

            if(GetActiveQuestions().Length > 0) {

                //Si solo queda una pregunta activa, abrirla directamente en lugar de abrir el selector
                if (GetActiveQuestions().Length == 1) {
                    GetActiveQuestions()[0].Show();
                } else {
                    QuestionSelector.Create(questions);
                }
            }
        }
    }

    public void SetQuestions(Question[] questions) {
        this.questions = questions;
        textMesh.text = "" + this.questions[0].value;
    }

    public Question[] GetQuestions() {
        return questions;
    }

    public void SetActive(bool active) {
        cellActive = active;
        if (cellActive) {
            textMesh.enabled = true;
        } else {
            textMesh.enabled = false;
        }
    }

    public bool ContainsQuestion(Question question) {
        if (questions == null) return false;
        foreach (Question current in questions) {
            if(current == question) {
                return true;
            }
        }
        return false;
    }

    public bool HasActiveQuestions() {
        if(type == Type.Question) {
            foreach (Question question in questions) {
                if (question.active) { return true; }
            }
            return false;
        }
        return true;
    }

    public Question[] GetActiveQuestions() {
        List<Question> results = new List<Question>();
        foreach (Question question in questions) {
            if (question.active) {
                results.Add(question);
            }
        }
        return results.ToArray();
    }

    public enum Type { Header, Question }
}
