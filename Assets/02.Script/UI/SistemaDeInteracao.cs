using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InteracaoState { Start, ActionSelection, MoveSelection, RunningTurn, Busy, PartyScreen, AboutToUse, InteracaoAcabou, Dilog, Question }

public class SistemaDeInteracao : MonoBehaviour
{
    [SerializeField] GameObject DialogoUI;
    [SerializeField] AulaDialogManager AulaDialogManager;

    [SerializeField] GameObject PerguntaUI;
    [SerializeField] PerguntaManager AulaPerguntaManager;

    public event Action<bool> OnBattleOver;

    InteracaoState state;
    InteracaoState? prevState;
    int currentAction;
    int currentMove;
    int currentMember;

    bool isInDialogue = false;

    Dialog FalasDuranteInteracao;
    List<Pergunta> Perguntas;

    bool isTrainerBattle = false;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void StartSistemaDeInteracao(Lesson lesson)
    {
        this.Perguntas = lesson.Perguntas;
        this.FalasDuranteInteracao = lesson.FalasDuranteInteracao;

        isTrainerBattle = true;

        StartCoroutine(IniciaInteracao());
    }

    public IEnumerator IniciaInteracao()
    {

        // Show Dialog UI
        isInDialogue = true;
        state = InteracaoState.Dilog;
        DialogoUI.SetActive(true);

        // Start Dialog
        return AulaDialogManager.Instance.ShowDialog(FalasDuranteInteracao, () => {
            StartPergunta();
        });
    }

    private void StartPergunta()
    {
        Debug.Log("Start Pergunta");
        // Show Pergunta UI
        isInDialogue = false;
        state = InteracaoState.Question;
        PerguntaUI.SetActive(true);
        AulaPerguntaManager.ShowQuestions(Perguntas, () =>
        {
            Debug.Log("Pergunta Fineshed");
            PerguntaUI.SetActive(false);
            InteracaoAcabou(true);
        });
    }

    void InteracaoAcabou(bool won)
    {
        state = InteracaoState.InteracaoAcabou;
        OnBattleOver(won);
    }

    internal void HandleUpdate()
    {
        if (isInDialogue)
            AulaDialogManager.HandleUpdate();

    }
}
