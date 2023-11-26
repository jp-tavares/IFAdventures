using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Dialog, Cutscene }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] SistemaDeInteracao sistemaInteracao;
    [SerializeField] Camera worldCamera;

    GameState state;

    public static GameController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        Time.timeScale = 1f;
        sistemaInteracao.OnBattleOver += EndBattle;

        playerController.OnEnterTrainersView += (Collider2D trainerCollider) =>
        {
            var trainer = trainerCollider.GetComponentInParent<TrainerController>();
            if (trainer != null)
            {
                state = GameState.Cutscene;
                StartCoroutine(trainer.TriggerTrainerBattle(playerController));
            }
        };

        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }


    TrainerController trainer;
    public void StartTrainerBattle(TrainerController trainer)
    {
        state = GameState.Battle;
        sistemaInteracao.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        this.trainer = trainer;
        //var playerParty = playerController.GetComponent<ListaPerguntas>();

        var lesson = trainer.GetComponent<TrainerController>().Lesson;
        sistemaInteracao.StartSistemaDeInteracao(lesson);
    }

    void EndBattle(bool won)
    {
        state = GameState.FreeRoam;
        sistemaInteracao.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);

        Instance.StartCoroutine(finalPergunta());

        IEnumerator finalPergunta()
        {
            yield return new WaitForEndOfFrame();

            if (trainer != null && won == true)
            {
                trainer.TerminarLicao();
                trainer = null;
            }
        }

    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            sistemaInteracao.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }
}
