using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alternativa
{
    [SerializeField] public string alternativa;
    [SerializeField] public bool isCorreta;

    public Alternativa(string alternativa, bool isCorreta)
    {
        this.alternativa = alternativa;
        this.isCorreta = isCorreta;
    }
}
