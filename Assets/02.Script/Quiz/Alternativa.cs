using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alternativa
{
    [SerializeField] public string alternativa;
    [SerializeField] private string feedback;
    [SerializeField] public bool isCorreta;

    public Alternativa(string alternativa, bool isCorreta)
    {
        this.alternativa = alternativa;
        this.isCorreta = isCorreta;
    }

    public void setFeedback(string feedback)
    {
        this.feedback = feedback;
    }

    public string getFeedback()
    {
        return this.feedback;
    }
}
