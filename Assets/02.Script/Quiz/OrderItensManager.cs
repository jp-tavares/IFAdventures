using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderItensManager : QuestionManagerBase
{

    [SerializeField] GameObject draggblesArea;
    [SerializeField] GameObject itemOrdemPrefab;


    [SerializeField] GameObject dropArea;

    OrdenacaoItens ordemItem { get => Pergunta?.OrdenacaoItens; }
    OrdemItem[] orderItens { get => ordemItem.OrderItens; }

    private void Update()
    {

        if (dropArea.transform.childCount == orderItens.Length)
        {
            AnalisarRespostas();
        }
    }

    public override void HandlerPergunta()
    {
        LoadOpcoesSprites();
    }

    private void LoadOpcoesSprites()
    {
        ordemItem.embaralhar();

        foreach (var item in orderItens)
        {
            GameObject orderm = Instantiate(itemOrdemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            orderm.transform.SetParent(draggblesArea.transform, false);

            orderm.name = item.Ordem.ToString();
            orderm.GetComponent<Image>().sprite = item.Image;
        }
    }

    public override void AnalisarRespostas()
    {
        var correto = true;

        for (int indice = 0; indice < orderItens.Length && correto; indice++)
        {
            var item = dropArea.transform.GetChild(indice);

            if(item.name != indice.ToString() && correto)
            {
                ModalFeedbackActions.OpenErrorModal($"Resposta errada, tente novamente!",
                                                ordemItem.getFeedback(indice),
                                                () =>
                                                {
                                                    for (int i = dropArea.transform.childCount - 1; i >= indice - 1; i--)
                                                    {
                                                        var itemErrado = dropArea.transform.GetChild(i);
                                                        itemErrado.transform.SetParent(draggblesArea.transform, false);
                                                    }
                                                });
                correto = false;
            }
        }

        if (correto)
        {
            ModalFeedbackActions.OpenSuccessModal($"Resposta correta, parabéns",
                                                "Todos os itens estão em ordem, parabéns",
                                                () =>
                                                {
                                                    Pergunta.RespondidaCorretamente();
                                                    ClosePanel();
                                                });
        }
    }
}
