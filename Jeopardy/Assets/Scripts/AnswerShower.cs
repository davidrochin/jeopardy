using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerShower : MonoBehaviour {

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

	}
}
