using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz", menuName = "ScriptableObjects/Quiz", order = 1)]
public class Quiz : ScriptableObject
{
    [System.Serializable]
    public struct Answer
    {
        public string Content;
        public bool Correct;
    }

    public QuestionType QuestionType;
    public string Question;
    public List<Answer> Answers;
}
