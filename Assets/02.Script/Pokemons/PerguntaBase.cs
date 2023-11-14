using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question/Create new question")]
public class PerguntaBase : ScriptableObject
{
    [SerializeField] string titulo;

    [TextArea]
    [SerializeField] string enunciado;

    [SerializeField] string[] alternativas;

    [SerializeField] int alternativaCorretaIndex;


    public string Name
    {
        get { return titulo; }
    }

    public string Description
    {
        get { return enunciado; }
    }

    public Alternativa[] Alternativas
    {
        get { return returnAlternativas(); }
    }

    public Alternativa[] returnAlternativas()
    {
        Alternativa[] alternativasReturn = default;

        for (int i = 0; i < alternativas.Length; i++)
        {
            if (i == alternativaCorretaIndex)
                alternativasReturn[i] = new Alternativa(alternativas[i], true);
            else
                alternativasReturn[i] = new Alternativa(alternativas[i], false);
        }

        return alternativasReturn;
    }


}