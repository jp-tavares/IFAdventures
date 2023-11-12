using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pergunta
{
    [SerializeField] PerguntaBase _base;

    public PerguntaBase Base
    {
        get
        {
            return _base;
        }
    }

    public string Titulo
    {
        get
        {
            return _base.Name;
        }
    }

    public string Enunciado
    {
        get
        {
            return _base.Description;
        }
    }

    public Alternativa[] Alternativas
    {
        get
        {
            return _base.Alternativas;
        }
    }

    public bool isRepondida { get; set; }

}
