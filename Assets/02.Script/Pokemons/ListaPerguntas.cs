using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListaPerguntas : MonoBehaviour
{
    [SerializeField] List<Pergunta> perguntas;

    public List<Pergunta> Pokemons {
        get {
            return perguntas;
        }
    }

    public Pergunta GetPerguntaNaoRespondida()
    {
        return perguntas.Where(x => !x.isRepondida).FirstOrDefault();
    }
}
