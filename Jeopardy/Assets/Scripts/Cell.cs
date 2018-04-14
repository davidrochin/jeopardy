using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour {

    TextMeshPro textMesh;
    Question question;

	// Use this for initialization
	void Awake () {

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
        textMesh.font = Resources.Load("Fonts & Materials/Oswald Bold SDF") as TMP_FontAsset;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
