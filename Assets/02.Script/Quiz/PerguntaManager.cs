using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerguntaManager : MonoBehaviour
{
    [SerializeField] GameObject caixaPergunta;
    [SerializeField] GameObject resposta1;
    [SerializeField] GameObject resposta2;
    [SerializeField] GameObject resposta3;
    [SerializeField] GameObject resposta4;

    [SerializeField] GameObject RespostaCorretaPanel;
    [SerializeField] GameObject RespostaErradaPanel;

    int indexPergunta;

    bool IsShowing;

    ListaPerguntas listaPerguntas;
    private Action onPerguntasFineshed;



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

    private void RespostaCorreta(Pergunta pergunta)
    {
        Debug.Log("Resposta Correta");
        RespostaCorretaPanel.SetActive(true);
        //pergunta.isRepondida = true;
    }

    private void RespostaErrada(Pergunta pergunta)
    {
        Debug.Log("Resposta Errada");
        RespostaErradaPanel.SetActive(true);
        //pergunta.isRepondida = false;
    }

    public void CloseRespostaCorretaPanel()
    {
        RespostaCorretaPanel.SetActive(false);
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

    public void CloseRespostaErradaPanel()
    {
        RespostaErradaPanel.SetActive(false);
        IsShowing = false;
    }

    public void Resposta1()
    {
        if (listaPerguntas.Perguntas[indexPergunta].Alternativas[0].isCorreta)
        {
            RespostaCorreta(listaPerguntas.Perguntas[indexPergunta]);
        }
        else
        {
            RespostaErrada(listaPerguntas.Perguntas[indexPergunta]);
        }
    }

    public void Resposta2()
    {
        if (listaPerguntas.Perguntas[indexPergunta].Alternativas[1].isCorreta)
        {
            RespostaCorreta(listaPerguntas.Perguntas[indexPergunta]);
        }
        else
        {
            RespostaErrada(listaPerguntas.Perguntas[indexPergunta]);
        }
    }

    public void Resposta3()
    {
        if (listaPerguntas.Perguntas[indexPergunta].Alternativas[2].isCorreta)
        {
            RespostaCorreta(listaPerguntas.Perguntas[indexPergunta]);
        }
        else
        {
            RespostaErrada(listaPerguntas.Perguntas[indexPergunta]);
        }
    }

    public void Resposta4()
    {
        if (listaPerguntas.Perguntas[indexPergunta].Alternativas[3].isCorreta)
        {
            RespostaCorreta(listaPerguntas.Perguntas[indexPergunta]);
        }
        else
        {
            RespostaErrada(listaPerguntas.Perguntas[indexPergunta]);
        }
    }


}
