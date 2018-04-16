using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour {

    public Type type = Type.Question;
    public bool activeCell = true;
    public TextMeshPro textMesh;
    Question question;

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
	
	// Update is called once per frame
	void Update () {

    }

    private void OnMouseUpAsButton() {
        if(type == Type.Question && EventSystem.current.IsPointerOverGameObject() == false) {
            question.Show();
            //Debug.LogWarning("Click");
        }
    }

    private void OnMouseOver() {
        if(type == Type.Question && EventSystem.current.IsPointerOverGameObject() == false) {

            //Si le hacen clic derecho a la celda
            if (Input.GetMouseButtonDown(1)) {
                if (activeCell) {
                    activeCell = false;
                    textMesh.enabled = false;
                } else {
                    activeCell = true;
                    textMesh.enabled = true;
                }
            }
        }
    }

    public void SetQuestion(Question question) {
        this.question = question;
        textMesh.text = "$" + this.question.value;
    }

    public enum Type { Header, Question }
}
