using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickMessage : MonoBehaviour {

    CanvasGroup canvasGroup;
    bool dismissed = false;
    float lifetime = 0f;

	// Use this for initialization
	void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }
	
	// Update is called once per frame
	void Update () {

        lifetime += Time.deltaTime;

        if (dismissed) {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 0f, 2f * Time.deltaTime);
            if (canvasGroup.alpha <= 0f) {
                Destroy(gameObject);
            }
        } else {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1f, 2f * Time.deltaTime);
        }

        //Descartar mensaje si se presiona una tecla
        if (Input.anyKeyDown || lifetime >= 1.5f) {
            dismissed = true;
        }
    }

    public static void Show(string text) {
        if (FindObjectOfType<QuickMessage>() != null) return;
        QuickMessage message = Instantiate(Resources.Load("quick_message") as GameObject, FindObjectOfType<Canvas>().transform, false).GetComponent<QuickMessage>();
        message.transform.Find("text").GetComponent<Text>().text = text;
    }
}
