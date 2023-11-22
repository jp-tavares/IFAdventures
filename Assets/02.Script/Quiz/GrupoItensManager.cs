using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GrupoItensManager : MonoBehaviour
{
    [SerializeField] GameObject caixaPergunta;
    [SerializeField] GameObject modalFeedbackPreFab;

    [SerializeField] GameObject itemDragPreFab;
    [SerializeField] GameObject draggblesArea;

    int indexPergunta;

    bool IsShowing;

    Pergunta Pergunta;
    private Action onPerguntasFineshed;

    private void Start()
    {
        ModalFeedbackActions.setModalFeedback(modalFeedbackPreFab);
    }

    public IEnumerator ShowQuestions(Pergunta pergunta, Action onFinished = null)
    {
        IsShowing = true;
        Pergunta = pergunta;
        onPerguntasFineshed = onFinished;

        indexPergunta = 0;
        caixaPergunta.GetComponentInChildren<Text>().text = pergunta.Enunciado;

        LoadDraggableItems(pergunta);

        return null;
    }

    private void LoadDraggableItems(Pergunta pergunta)
    {
        List<string> itemsDrag = pergunta.GruposItens.getAllEmbaralhados();

        Debug.Log(String.Join(',', itemsDrag));

        foreach (var item in itemsDrag)
        {
            GameObject itemPreFab = Instantiate(itemDragPreFab, new Vector3(0, 0, 0), Quaternion.identity);
            Debug.Log(item);
            itemPreFab.transform.SetParent(draggblesArea.transform, false);
            itemPreFab.GetComponentInChildren<Text>().text = item;
        }
    }

}
