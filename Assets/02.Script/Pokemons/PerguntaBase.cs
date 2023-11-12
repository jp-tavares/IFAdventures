using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
public class PerguntaBase : ScriptableObject
{
    [SerializeField] string titulo;
    
    [TextArea]
    [SerializeField] string enunciado;

    [SerializeField] Alternativa[] alternativas;


    public string Name {
        get { return titulo; }
    }

    public string Description {
        get { return enunciado; }
    }

    public Alternativa[] Alternativas
    {
        get { return alternativas; }
    }
}
