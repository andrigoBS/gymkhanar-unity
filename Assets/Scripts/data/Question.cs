using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DataHelpers
{
    [System.Serializable]
    public class Question
    {
        public static Question fromJSON(string json)
        {
            return JsonUtility.FromJson<Question>(json);
        }

        public string error;
        public int id;
        public string title;
        public string text;
        public QuestionOption[] options;
    }
    
    [System.Serializable]
    public class QuestionOption
    {
        public string text;
        public string correction;
        public bool isCorrect;
    }
}
