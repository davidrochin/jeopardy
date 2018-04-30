using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCollection {

    public List<Question> questions;

    public QuestionCollection(Question[] questions) {
        this.questions = new List<Question>(questions);
    }

    public Question RandomWithValue(int value) {
        Question[] results = questions.FindAll(x => x.value == value).ToArray();
        return results[Random.Range(0, results.Length - 1)];
    }

    public Question RandomWithValueAndCategory(int value, Question.Category category) {
        Question[] results = questions.FindAll(x => x.value == value && x.category == category).ToArray();
        Debug.Log(value + ", " + category + ", " + results.Length);
        return results[Random.Range(0, results.Length - 1)];
    }

}
