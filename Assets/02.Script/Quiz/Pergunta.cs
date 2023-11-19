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
    [SerializeField] GruposItens[] gruposItens;

    [HideInInspector]
    public bool isRepondida { get; set; }

    public string Enunciado { get => enunciado; }

    public Alternativa[] Alternativas { get => alternativas;}
    public GruposItens[] GruposItens { get => gruposItens; }
    public TipoPerguntaEnum Tipo { get => tipoPergunta; }


    public List<string> getAllItensGrupo()
    {
        var itens = new List<string>();

        foreach (var grupoItem in gruposItens)
        {
            itens.AddRange(grupoItem.itens);
        }

        return itens;
    }
}



