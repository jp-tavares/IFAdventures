using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PerguntaExtension
{
    public static Pergunta toGetPerguntaNaoRespondida(this List<Pergunta> perguntas)
    {
        return perguntas.Where(x => !x.isRepondida).FirstOrDefault();
    }
}
