using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question {

    public string text;
    public string answer;

    public int points;
    public Category category;

    public Question(string questionText, string answer, int points, Category category) {
        this.text = questionText;
        this.answer = answer;
        this.points = points;
        this.category = category;
    }

    public override string ToString() {
        return "Pregunta: " + text + ", Respuesta: " + answer + ", Valor: " + points + ", Categoría: " + category.ToString();
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
