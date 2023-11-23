using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuestionManagerBase : MonoBehaviour
{
    [SerializeField] protected GameObject caixaPergunta;
    [SerializeField] protected GameObject modalFeedbackPreFab;

    protected bool IsShowing;

    protected Pergunta Pergunta { get; set; }

    protected Action onPerguntasFineshed;

    private void Start()
    {
        ModalFeedbackActions.setModalFeedback(modalFeedbackPreFab);
    }

    public abstract void HandlerPergunta();

    public IEnumerator ShowQuestions(Pergunta pergunta, Action onFinished = null)
    {
        IsShowing = true;
        Pergunta = pergunta;
        onPerguntasFineshed = onFinished;

        LoadCaixaPergunta();

        HandlerPergunta();

        return null;
    }

    protected void LoadCaixaPergunta()
    {
        caixaPergunta.GetComponentInChildren<Text>().text = Pergunta.Enunciado;
    }

    public void ClosePanel()
    {
        IsShowing = false;
        onPerguntasFineshed?.Invoke();
    }
}
