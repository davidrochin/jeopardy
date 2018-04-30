using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPanel : MonoBehaviour {

    RectTransform rectTransform;
    Quaternion initialRotation;

    void Awake () {
        rectTransform = GetComponent<RectTransform>();
        //initialRotation = rectTransform.rotation;
        //rectTransform.rotation = Quaternion.Euler(90f, 0f, 0f);
        rectTransform.localScale = Vector3.zero;
    }
	
	void Update () {
        rectTransform.rotation = Quaternion.RotateTowards(rectTransform.rotation, Quaternion.identity, 200f * Time.deltaTime);
        rectTransform.localScale = Vector3.MoveTowards(rectTransform.localScale, Vector3.one, 3f * Time.deltaTime);
	}

    public IEnumerator FadeIn() {
        RectTransform rectTransform = GetComponent<RectTransform>();
        yield return new WaitForEndOfFrame();
    }
}
