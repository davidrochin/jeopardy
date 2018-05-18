using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSelectorItem : MonoBehaviour {

    Question question;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static GameObject Create(Question newQuestion, Transform parent, string itemText) {
        GameObject itemPrefab = Resources.Load("selector_item") as GameObject;
        GameObject itemObject = Instantiate(itemPrefab, parent, false);
        QuestionSelectorItem item = itemObject.GetComponent<QuestionSelectorItem>();

        item.question = newQuestion;

        //item.GetComponentInChildren<Text>().text = item.question.text;
        item.GetComponentInChildren<Text>().text = itemText;

        item.GetComponent<Button>().onClick.AddListener(delegate() {
            item.question.Show();
            Destroy(parent.gameObject);
        });

        return itemObject;
    }
}
