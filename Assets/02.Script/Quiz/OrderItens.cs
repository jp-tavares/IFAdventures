using UnityEngine;
using System.Linq;

[System.Serializable]
public class OrdenacaoItens
{
    [SerializeField] OrdemItem[] orderItens;

    public OrdemItem[] getItensEmbaralhados() {
        int qtdItens = orderItens.Count();

        var order = new OrdemItem[qtdItens];

        System.Random random = new System.Random();

        for (int i = qtdItens - 1; i > 0; i--)
        {
            int indiceAleatorio = random.Next(0, i + 1);

            OrdemItem temp = orderItens[i];
            orderItens[i] = orderItens[indiceAleatorio];
            orderItens[indiceAleatorio] = temp;
        }

        return order;
    }
}

[System.Serializable]
public class OrdemItem
{
    [SerializeField] public int Ordem;
    [SerializeField] public Sprite Image;
}