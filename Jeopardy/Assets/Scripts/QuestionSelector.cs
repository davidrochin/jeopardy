using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSelector : MonoBehaviour {

    public static Color[] colors = {
        new Color32(255, 55, 55, 255),
        new Color32(92, 209, 108, 255),
        new Color32(72, 115, 216, 255),
        new Color32(216, 182, 72, 255),
        new Color32(168, 101, 200, 255),
    };

    public static QuestionSelector Create(Question[] questions) {

        //Buscar los prefabs necesarios
        GameObject selectorPrefab = Resources.Load("question_selector") as GameObject;
        GameObject itemPrefab = Resources.Load("selector_item") as GameObject;

        //Instanciar el selector y emparentarlo al Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        QuestionSelector selector = Instantiate(selectorPrefab, canvas.transform, false).GetComponent<QuestionSelector>();

        int count = 0;
        foreach (Question question in questions) {

            //Agregar la pregunta al selector solo si está activa
            if (question.active) {
                GameObject item = QuestionSelectorItem.Create(question, selector.transform);
                item.GetComponent<Image>().color = colors[count];
            }
            count++;
        }
        return selector;
    }
}
