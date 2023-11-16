using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InteracaoState { Start, ActionSelection, MoveSelection, RunningTurn, Busy, PartyScreen, AboutToUse, InteracaoAcabou, Dilog, Question }
public enum BattleAction { Move, SwitchPokemon, UseItem, Run }

public class SistemaDeInteracao : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] PartyScreen partyScreen;
    [SerializeField] Image playerImage;
    [SerializeField] Image professorImage;

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

    ListaPerguntas perguntasDoProfessor;

    bool isTrainerBattle = false;
    PlayerController player;
    TrainerController professor;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void StartTrainerBattle(ListaPerguntas perguntasDoProfessor, Dialog falasDoProfessor)
    {
        this.perguntasDoProfessor = perguntasDoProfessor;

        isTrainerBattle = true;
        professor = perguntasDoProfessor.GetComponent<TrainerController>();

        StartCoroutine(IniciaInteracao());
    }

    public IEnumerator IniciaInteracao()
    {

        // Show Dialog UI
        isInDialogue = true;
        state = InteracaoState.Dilog;
        DialogoUI.SetActive(true);        

        // Start Dialog
         return AulaDialogManager.Instance.ShowDialog(professor.FalasDuranteInteracao, () => { 
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
        AulaPerguntaManager.ShowQuestions(perguntasDoProfessor, () =>
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
        if(isInDialogue) 
            AulaDialogManager.HandleUpdate();

    }
}
