﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Question {

    public string text;
    public string answer;

    public int value;

    public int category;
    public int level;

    public bool active = true;

    public string[] options;

    public Question(string questionText, string answer, int value, int category, int level, string[] options) {
        this.text = questionText;
        this.answer = answer;
        this.value = value;
        this.category = category;
        this.level = level;
        this.options = options;
    }

    public override string ToString() {
        return "Pregunta: " + text + ", Respuesta: " + answer + ", Valor: " + value + ", Categoría: " + category;
    }

    public void Show() {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if(canvas == null) { canvas = new GameObject("Canvas").AddComponent<Canvas>(); }

        GameObject panel = Resources.Load("question_panel") as GameObject;
        panel = GameObject.Instantiate(panel, canvas.transform, false);

        panel.transform.Find("question_text").GetComponent<Text>().text = text;

        //Si no hay opciones, ocultar el aviso
        if (options == null || options.Length <= 1) {
            panel.transform.Find("options_notice").gameObject.SetActive(false);
        } 

        //Agregar el componente que muestra la respuesta
        AnswerManager answerShower = panel.AddComponent<AnswerManager>();
        answerShower.question = this;

    }

    public static Question[] FromCSV() {
        List<Question> questions = new List<Question>();
        TextAsset textAsset = (TextAsset) Resources.Load("preguntas");

        CSVReader.LoadFromString(textAsset.text, delegate(int index, List<string> line) {
            Question q = new Question(line[3], line[5], int.Parse(line[2]), int.Parse(line[1]), int.Parse(line[0]), line[4].Split(';').Length > 1 ? line[4].Split(';') : new string[0]);
            //Debug.Log(q);
            questions.Add(q);
        });
        return questions.ToArray();
    }

}
