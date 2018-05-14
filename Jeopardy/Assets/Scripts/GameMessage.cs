using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMessage : MonoBehaviour {

    CanvasGroup canvasGroup;

    bool dismissed = false;

    // Use this for initialization
    void Awake () {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        if (dismissed) {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 0f, 2f * Time.deltaTime);

            if (canvasGroup.alpha <= 0f) {
                Destroy(gameObject);
            }
        } else {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1f, 2f * Time.deltaTime);
        }

        //Descartar mensaje si se presiona una tecla
        if (Input.anyKeyDown) {
            dismissed = true;
        }  
	}

    public static void Show(string text) {
        if (FindObjectOfType<GameMessage>() != null) return;
        GameMessage message = Instantiate(Resources.Load("game_message") as GameObject, FindObjectOfType<Canvas>().transform, false).GetComponent<GameMessage>();
        message.transform.Find("panel").Find("text").GetComponent<Text>().text = text;
    }
}
