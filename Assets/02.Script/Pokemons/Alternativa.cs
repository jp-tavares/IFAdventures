using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alternativa
{
    [SerializeField] string alternativa;
    [SerializeField] bool isCorrect;

    public Alternativa(string alternativa, bool isCorrect)
    {
        this.alternativa = alternativa;
        this.isCorrect = isCorrect;
    }
}
