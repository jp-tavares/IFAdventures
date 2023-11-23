using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Lesson", menuName = "Lesson/Create new question")]
public class Pergunta : ScriptableObject
{
    [TextArea]
    [SerializeField] string enunciado;
    [SerializeField] TipoPerguntaEnum tipoPergunta;

    [SerializeField] Alternativa[] alternativas;
    [SerializeField] GruposItens gruposItens;
    [SerializeField] OrdenacaoItens ordenacaoItens;

    [HideInInspector]
    public bool IsRepondida { get; private set; }

    public string Enunciado { get => enunciado; }

    public Alternativa[] Alternativas { get => alternativas;}
    public GruposItens GruposItens { get => gruposItens; }
    public OrdenacaoItens OrdenacaoItens { get => ordenacaoItens; }
    public TipoPerguntaEnum Tipo { get => tipoPergunta; }


    public void RespondidaCorretamente()
    {
        IsRepondida = true;
    }
}



