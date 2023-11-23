using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GrupoItensManager : QuestionManagerBase
{

    [SerializeField] GameObject itemDragPreFab;
    [SerializeField] GameObject draggblesArea;


    [SerializeField] GameObject dropPreFab;
    [SerializeField] GameObject dropAreas;

    [SerializeField] GameObject Opcoes;

    GruposItens gruposItens { get => Pergunta?.GruposItens; }
    GruposItem[] listGruposItem { get => gruposItens?.ListGruposItens; }


    private void Update()
    {
        if (Opcoes.transform.childCount == 0)
        {
            var totalItensDraggbles = 0;

            for (int i = 1; i <= listGruposItem.Length; i++)
            {
                var grupo = dropAreas.transform.GetChild(i);
                totalItensDraggbles += grupo.transform
                         .GetComponentsInChildren<Button>()
                         .Count();
            }

            if (totalItensDraggbles == gruposItens.getAllItensGrupo().Count())
            {
                AnalisarRespostas();
            }
        }
    }

    public override void HandlerPergunta()
    {
        LoadDraggableItems(Pergunta.GruposItens);
        LoadDropsAreas(Pergunta.GruposItens);
    }

    private void LoadDropsAreas(GruposItens gruposItens)
    {
        var drops = gruposItens.ListGruposItens;

        foreach (var drop in drops)
        {
            GameObject itemDropPreFab = Instantiate(dropPreFab, new Vector3(0, 0, 0), Quaternion.identity);
            itemDropPreFab.transform.SetParent(dropAreas.transform, false);
            itemDropPreFab.GetComponentInChildren<Text>().text = drop.nomeGrupo;
        }
    }

    private void LoadDraggableItems(GruposItens gruposItens)
    {
        List<string> itemsDrag = gruposItens.getAllEmbaralhados();

        Debug.Log(String.Join(',', itemsDrag));

        foreach (var item in itemsDrag)
        {
            GameObject itemPreFab = Instantiate(itemDragPreFab, new Vector3(0, 0, 0), Quaternion.identity);
            Debug.Log(item);
            itemPreFab.transform.SetParent(draggblesArea.transform, false);
            itemPreFab.GetComponentInChildren<Text>().text = item;
        }
    }


    private void AnalisarRespostas()
    {
        var gruposItens = Pergunta.GruposItens.ListGruposItens;
        var nomesGrupos = gruposItens.Select(i => i.nomeGrupo);

        var correto = true;

        for (int i = 1; i <= nomesGrupos.Count() && correto; i++)
        {
            var grupo = dropAreas.transform.GetChild(i);
            correto = ValidarGrupo(grupo);
        }

        if (correto)
        {
            ModalFeedbackActions.OpenSuccessModal($"Resposta correta, parabéns",
                                                "Todos os itens estão corretos, parabéns",
                                                () =>
                                                {
                                                    Pergunta.RespondidaCorretamente();
                                                    ClosePanel();
                                                });
        }
    }

    private bool ValidarGrupo(Transform grupo)
    {
        var tituloGrupo = grupo.GetComponentInChildren<Text>().text;
        var itens = grupo.transform.GetComponentsInChildren<Button>();

        var gruposItem = listGruposItem.FirstOrDefault(g => g.NomeGrupo == tituloGrupo);

        foreach (var item in itens)
        {
            var descricaoItem = item.GetComponentInChildren<Text>().text;

            if (!gruposItem.getDescricoes().Contains(descricaoItem))
            {
                ModalFeedbackActions.OpenErrorModal($"Resposta errada, tente novamente!",
                                                    $"O item \"{descricaoItem}\" está no grupo errado\n " + gruposItens.getFeedback(descricaoItem),
                                                    () =>
                                                    {
                                                        item.transform.SetParent(Opcoes.transform, false);
                                                    });
                return false;
            }
        }

        return true;
    }


}
