using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GruposItens
{
    [SerializeField] GruposItem[] gruposItens;

    public List<string> getAllItensGrupo()
    {
        var itens = new List<string>();

        foreach (var grupoItem in gruposItens)
        {
            itens.AddRange(grupoItem.itens);
        }

        return itens;
    }

    public List<string> getAllEmbaralhados()
    {
        var lista = getAllItensGrupo();
        int qtdItens = lista.Count();

        var order = new string[qtdItens];

        System.Random random = new System.Random();

        for (int i = qtdItens - 1; i > 0; i--)
        {
            int indiceAleatorio = random.Next(0, i + 1);

            string temp = lista[i];
            lista[i] = lista[indiceAleatorio];
            lista[indiceAleatorio] = temp;
        }

        return order.ToList();
    }

}

[System.Serializable]
public class GruposItem
{
    [SerializeField] public string nomeGrupo;
    [SerializeField] public List<string> itens;
}