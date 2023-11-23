using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GrupoItensManager : MonoBehaviour
{
    [SerializeField] GameObject caixaPergunta;
    [SerializeField] GameObject modalFeedbackPreFab;

    [SerializeField] GameObject itemDragPreFab;
    [SerializeField] GameObject draggblesArea;


    [SerializeField] GameObject dropPreFab;
    [SerializeField] GameObject dropAreas;

    [SerializeField] GameObject Opcoes;

    int indexPergunta;

    bool IsShowing;

    Pergunta Pergunta;
    GruposItens gruposItens { get => Pergunta?.GruposItens; }
    GruposItem[] listGruposItem { get => gruposItens?.ListGruposItens; }

    private Action onPerguntasFineshed;

    private void Start()
    {
        ModalFeedbackActions.setModalFeedback(modalFeedbackPreFab);
    }

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

    public IEnumerator ShowQuestions(Pergunta pergunta, Action onFinished = null)
    {
        IsShowing = true;
        Pergunta = pergunta;
        onPerguntasFineshed = onFinished;

        indexPergunta = 0;

        LoadPergunta(pergunta);

        return null;
    }

    private void LoadPergunta(Pergunta pergunta)
    {
        caixaPergunta.GetComponentInChildren<Text>().text = pergunta.Enunciado;

        LoadDraggableItems(pergunta.GruposItens);
        LoadDropsAreas(pergunta.GruposItens);
    }

    private void LoadDropsAreas(GruposItens gruposItens)
    {
        var drops = gruposItens.ListGruposItens;

        foreach (var drop in drops)
        {
            GameObject itemDropPreFab = Instantiate(dropPreFab, new Vector3(0, 0, 1), Quaternion.identity);
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
                                                    CloseRespostaCorretaPanel();
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
                Debug.Log("gruposItem: " + string.Join(", ", gruposItem.getDescricoes()));
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

    public void CloseRespostaCorretaPanel()
    {
        IsShowing = false;
        onPerguntasFineshed?.Invoke();
    }
}
