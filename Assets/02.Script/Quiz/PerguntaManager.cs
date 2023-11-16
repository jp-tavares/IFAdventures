using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PerguntaManager : MonoBehaviour
{
    [SerializeField] GameObject caixaPergunta;
    [SerializeField] GameObject resposta1;
    [SerializeField] GameObject resposta2;
    [SerializeField] GameObject resposta3;
    [SerializeField] GameObject resposta4;

    [SerializeField] GameObject modalFeedbackPreFab;

    int indexPergunta;

    bool IsShowing;

    ListaPerguntas listaPerguntas;
    private Action onPerguntasFineshed;

    private void Start()
    {
        ModalFeedbackAction.setModalFeedback(modalFeedbackPreFab);
    }

    public IEnumerator ShowQuestions(ListaPerguntas listaPerguntas, Action onFinished = null)
    {
        IsShowing = true;
        this.listaPerguntas = listaPerguntas;
        onPerguntasFineshed = onFinished;

        indexPergunta = 0;
        HandlePergunta(listaPerguntas.Perguntas[indexPergunta]);

        return null;
    }

    private void HandlePergunta(Pergunta pergunta)
    {
        caixaPergunta.GetComponentInChildren<Text>().text = pergunta.Enunciado;
        resposta1.GetComponentInChildren<Text>().text = pergunta.Alternativas[0].alternativa;
        resposta2.GetComponentInChildren<Text>().text = pergunta.Alternativas[1].alternativa;
        resposta3.GetComponentInChildren<Text>().text = pergunta.Alternativas[2].alternativa;
        resposta4.GetComponentInChildren<Text>().text = pergunta.Alternativas[3].alternativa;
    }

    public void CloseRespostaCorretaPanel()
    {
        IsShowing = false;
        if ((indexPergunta + 1) == listaPerguntas.Perguntas.Count)
        {
            onPerguntasFineshed?.Invoke();
        }
        else
        {
            indexPergunta++;
            HandlePergunta(listaPerguntas.Perguntas[indexPergunta]);
        }
    }

    public void Resposta1()
    {
        analisarResposta(0);
    }

    public void Resposta2()
    {
        analisarResposta(1);
    }

    public void Resposta3()
    {
        analisarResposta(2);
    }

    public void Resposta4()
    {
        analisarResposta(3);
    }

    private void analisarResposta(int indice)
    {
        var pergunta = listaPerguntas.Perguntas[indexPergunta];
        var alternativa = pergunta.Alternativas[indice];

        if (alternativa.isCorreta)
        {
            ModalFeedbackAction.OpenSuccessModal("Resposta correta, parabéns", alternativa.getFeedback(), CloseRespostaCorretaPanel);
        }
        else
        {
            ModalFeedbackAction.OpenErrorModal("Resposta errada, tente novamente!", alternativa.getFeedback(), () => { });
        }

        pergunta.isRepondida = alternativa.isCorreta;
    }

}
