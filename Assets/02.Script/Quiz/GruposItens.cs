using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GruposItens
{
    [SerializeField] GruposItem[] gruposItens;

    public GruposItem[] ListGruposItens { get => gruposItens; } 

    public List<string> getAllItensGrupo()
    {
        var itens = new List<string>();

        foreach (var grupoItem in gruposItens)
        {
            itens.AddRange(grupoItem.itens.Select(i => i.descricao));
        }

        return itens;
    }

    public List<string> getAllEmbaralhados()
    {
        var lista = getAllItensGrupo();
        int qtdItens = lista.Count();

        System.Random random = new System.Random();

        for (int i = qtdItens - 1; i > 0; i--)
        {
            int indiceAleatorio = random.Next(0, i + 1);

            string temp = lista[i];
            lista[i] = lista[indiceAleatorio];
            lista[indiceAleatorio] = temp;
        }

        return lista.ToList();
    }

    public string getFeedback(string descricao)
    {
        var feedback = "";
        foreach (var grupo in gruposItens)
        {
            feedback = grupo.Itens.FirstOrDefault(i => i.descricao == descricao)?.feedback ?? "";
            if (!string.IsNullOrWhiteSpace(feedback)) return feedback;
        }
        return feedback;
    }

}

[System.Serializable]
public class GruposItem
{
    [SerializeField] public string nomeGrupo;
    [SerializeField] public List<Descricao> itens;

    public string NomeGrupo { get => nomeGrupo;  }
    public List<Descricao> Itens { get => itens;  }

    public string getFeedback(string descricao)
    {
        return this.itens.FirstOrDefault(i => i.descricao == descricao)?.feedback;
    }

    public List<string> getDescricoes()
    {
        return this.itens.Select(i => i.descricao).ToList();
    }
}

[System.Serializable]
public class Descricao
{
    [SerializeField] public string descricao;
    [SerializeField] public string feedback;

}