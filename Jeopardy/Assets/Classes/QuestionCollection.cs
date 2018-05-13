using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCollection {

    public List<Question> questions;

    public QuestionCollection(Question[] questions) {
        this.questions = new List<Question>(questions);
    }

    public Question[] RandomWithValue(int value) {
        Question[] results = questions.FindAll(x => x.value == value).ToArray();
        return results;
    }

    public Question[] RandomWithValueAndCategory(int value, int category) {
        Question[] results = questions.FindAll(x => x.value == value && x.category == category).ToArray();
        return results;
    }

    public Question[] GetAllWith(int value, int level, int category) {
        Question[] results = questions.FindAll(x => x.value == value && x.category == category && x.level == level).ToArray();
        return results;
    }

    public Question[] GetNumberWith(int value, int level, int category, int max) {
        Question[] results = GetAllWith(value, level, category);
        List<Question> maxed = new List<Question>();
        for (int i = 0; i < max; i++) {
            if(i < results.Length && results[i] != null) {
                maxed.Add(results[i]);
            }
        }
        return maxed.ToArray();
    }

}
