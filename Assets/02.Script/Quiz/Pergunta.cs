using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Lesson", menuName = "Lesson/Create new question")]
public class Pergunta : ScriptableObject
{
    [TextArea]
    [SerializeField] string enunciado;
    [SerializeField] TipoPerguntaEnum tipoPergunta;

    [SerializeField] Alternativa[] alternativas;
    [SerializeField] GruposItens[] gruposItens;

    [HideInInspector]
    public bool isRepondida { get; set; }

    public string Enunciado { get => enunciado; }

    public Alternativa[] Alternativas { get => alternativas;}
    public GruposItens[] GruposItens { get => gruposItens; }
}



