using UnityEngine;
using System.Linq;

[System.Serializable]
public class OrdenacaoItens
{
    [SerializeField] OrdemItem[] orderItens;

    public void embaralhar() {
        int qtdItens = orderItens.Count();

        System.Random random = new System.Random();

        for (int i = qtdItens - 1; i > 0; i--)
        {
            int indiceAleatorio = random.Next(0, i + 1);

            OrdemItem temp = orderItens[i];
            orderItens[i] = orderItens[indiceAleatorio];
            orderItens[indiceAleatorio] = temp;
        }
    }

    public string getFeedback(int ordem)
    {
        var item = orderItens.FirstOrDefault(o => o.Ordem == ordem);
        return item.Feedback ?? "";
    }

    public OrdemItem[] OrderItens { get => orderItens; }
}

[System.Serializable]
public class OrdemItem
{
    [SerializeField] public int Ordem;
    [SerializeField] public Sprite Image;
    [SerializeField] public string Feedback;
}