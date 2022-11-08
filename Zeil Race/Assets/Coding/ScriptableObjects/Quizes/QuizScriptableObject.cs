using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz", menuName = "ScriptableObjects/QuizScriptableObject", order = 1)]
public class QuizScriptableObject : ScriptableObject
{
    [System.Serializable]
    public struct Answer
    {
        public string Content;
        public bool Correct;
    }

    public bool IsFinalQuiz;
    public string Question;
    public List<Answer> Answers;
}
