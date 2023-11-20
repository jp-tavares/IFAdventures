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

    int indexPergunta;

    bool IsShowing;

    List<Pergunta> Perguntas;
    private Action onPerguntasFineshed;

    private void Start()
    {
        ModalFeedbackActions.setModalFeedback(modalFeedbackPreFab);
    }

    public IEnumerator ShowQuestions(List<Pergunta> perguntas, Action onFinished = null)
    {
        IsShowing = true;
        Perguntas = perguntas;
        onPerguntasFineshed = onFinished;

        indexPergunta = 0;
        HandlePergunta(Perguntas[indexPergunta]);

        return null;
    }

    private void HandlePergunta(Pergunta pergunta)
    {
        caixaPergunta.GetComponentInChildren<Text>().text = pergunta.Enunciado;
    }

}
