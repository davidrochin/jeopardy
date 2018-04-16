using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Question {

    public string text;
    public string answer;

    public int value;
    public Category category;

    public Question(string questionText, string answer, int points, Category category) {
        this.text = questionText;
        this.answer = answer;
        this.value = points;
        this.category = category;
    }

    public override string ToString() {
        return "Pregunta: " + text + ", Respuesta: " + answer + ", Valor: " + value + ", Categoría: " + category.ToString();
    }

    public void Show() {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if(canvas == null) { canvas = new GameObject("Canvas").AddComponent<Canvas>(); }

        GameObject panel = Resources.Load("question_panel") as GameObject;
        panel = GameObject.Instantiate(panel, canvas.transform, false);

        panel.transform.Find("question_text").GetComponent<Text>().text = text;

        //Agregar el componente que muestra la respuesta
        AnswerShower answerShower = panel.AddComponent<AnswerShower>();
        answerShower.question = this;

    }

    public static Question[] FromCSV() {
        List<Question> questions = new List<Question>();
        TextAsset textAsset = (TextAsset) Resources.Load("preguntas");

        CSVReader.LoadFromString(textAsset.text, delegate(int index, List<string> line) {
            Question q = new Question(line[2], line[3], int.Parse(line[1]), (Category)int.Parse(line[0]) - 1);
            //Debug.Log(q);
            questions.Add(q);
        });
        return questions.ToArray();
    }

    public enum Category { Anatomy, Category2, Category3, Category4, Category5, Category6 }
}
