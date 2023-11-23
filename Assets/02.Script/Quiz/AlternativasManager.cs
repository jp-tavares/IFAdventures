using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AlternativasManager : QuestionManagerBase
{
    [SerializeField] GameObject resposta1;
    [SerializeField] GameObject resposta2;
    [SerializeField] GameObject resposta3;
    [SerializeField] GameObject resposta4;


    public override void HandlerPergunta()
    {
        resposta1.GetComponentInChildren<Text>().text = Pergunta.Alternativas[0].alternativa;
        resposta2.GetComponentInChildren<Text>().text = Pergunta.Alternativas[1].alternativa;
        resposta3.GetComponentInChildren<Text>().text = Pergunta.Alternativas[2].alternativa;
        resposta4.GetComponentInChildren<Text>().text = Pergunta.Alternativas[3].alternativa;
    }

    public void Resposta1()
    {
        AnalisarRespostas(0);
    }

    public void Resposta2()
    {
        AnalisarRespostas(1);
    }

    public void Resposta3()
    {
        AnalisarRespostas(2);
    }

    public void Resposta4()
    {
        AnalisarRespostas(3);
    }

    private void AnalisarRespostas(int indice)
    {
        var alternativa = Pergunta.Alternativas[indice];

        if (alternativa.isCorreta)
        {
            ModalFeedbackActions.OpenSuccessModal("Resposta correta, parabéns", alternativa.getFeedback(), ClosePanel);
            Pergunta.RespondidaCorretamente();
        }
        else
        {
            ModalFeedbackActions.OpenErrorModal("Resposta errada, tente novamente!", alternativa.getFeedback(), () => { });
        }
    }
}
