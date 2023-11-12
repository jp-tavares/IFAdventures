using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, ActionSelection, MoveSelection, RunningTurn, Busy, PartyScreen, AboutToUse, BattleOver }
public enum BattleAction { Move, SwitchPokemon, UseItem, Run }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] PartyScreen partyScreen;
    [SerializeField] Image playerImage;
    [SerializeField] Image professorImage;

    [SerializeField] GameObject DialogoUI;
    [SerializeField] AulaDialogManager AulaDialogManager;

    public event Action<bool> OnBattleOver;

    BattleState state;
    BattleState? prevState;
    int currentAction;
    int currentMove;
    int currentMember;
    bool aboutToUseChoice = true;

    bool isInDialogue = false;

    ListaPerguntas perguntasDoProfessor;

    bool isTrainerBattle = false;
    PlayerController player;
    TrainerController professor;

    public void StartTrainerBattle(ListaPerguntas perguntasDoProfessor, Dialog falasDoProfessor)
    {
        this.perguntasDoProfessor = perguntasDoProfessor;

        isTrainerBattle = true;
        professor = perguntasDoProfessor.GetComponent<TrainerController>();

        StartCoroutine(SetupInteracao());
    }

    public IEnumerator SetupInteracao()
    {

        // Show Dialog UI
        isInDialogue = true;
        DialogoUI.SetActive(true);        

        // Start Dialog
         return AulaDialogManager.Instance.ShowDialog(professor.FalasDuranteInteracao);

        // Hide Dialog UI


        // Show question UI


        // Start question


        // Hide question UI


    }

    void BattleOver(bool won)
    {
        state = BattleState.BattleOver;
        OnBattleOver(won);
    }

    void ActionSelection()
    {
        state = BattleState.ActionSelection;
        dialogBox.SetDialog("Choose an action");
    }

    void OpenPartyScreen()
    {
        state = BattleState.PartyScreen;
        //partyScreen.SetPartyData(playerParty.Pokemons);
        partyScreen.gameObject.SetActive(true);
    }

    void MoveSelection()
    {
        state = BattleState.MoveSelection;
        dialogBox.EnableDialogText(false);
    }

    internal void StartBattle(ListaPerguntas playerParty, Pergunta wildPokemon)
    {
        
    }

    internal void HandleUpdate()
    {
        if(isInDialogue) 
            AulaDialogManager.HandleUpdate();

    }

    //    IEnumerator AboutToUse(Pokemon newPokemon)
    //    {
    //        state = BattleState.Busy;
    //        yield return dialogBox.TypeDialog($"{professor.Name} is about to use {newPokemon.Base.Name}. Do you want to change pokemon?");

    //        state = BattleState.AboutToUse;
    //        dialogBox.EnableChoiceBox(true);
    //    }

    //    IEnumerator RunTurns(BattleAction playerAction)
    //    {
    //        state = BattleState.RunningTurn;

    //        if (playerAction == BattleAction.Move)
    //        {
    //            playerUnit.Pergunta.CurrentMove = playerUnit.Pergunta.Alternativas[currentMove];
    //            enemyUnit.Pergunta.CurrentMove = enemyUnit.Pergunta.GetRandomMove();

    //            int playerMovePriority = playerUnit.Pergunta.CurrentMove.Base.Priority;
    //            int enemyMovePriority = enemyUnit.Pergunta.CurrentMove.Base.Priority;

    //            // Check who goes first
    //            bool playerGoesFirst = true;
    //            if (enemyMovePriority > playerMovePriority)
    //                playerGoesFirst = false;
    //            else if (enemyMovePriority == playerMovePriority)
    //                playerGoesFirst = playerUnit.Pergunta.Speed >= enemyUnit.Pergunta.Speed;

    //            var firstUnit = (playerGoesFirst) ? playerUnit : enemyUnit;
    //            var secondUnit = (playerGoesFirst) ? enemyUnit : playerUnit;

    //            var secondPokemon = secondUnit.Pergunta;

    //            // First Turn
    //            yield return RunMove(firstUnit, secondUnit, firstUnit.Pergunta.CurrentMove);
    //            yield return RunAfterTurn(firstUnit);
    //            if (state == BattleState.BattleOver) yield break;

    //            if (secondPokemon.HP > 0)
    //            {
    //                // Second Turn
    //                yield return RunMove(secondUnit, firstUnit, secondUnit.Pergunta.CurrentMove);
    //                yield return RunAfterTurn(secondUnit);
    //                if (state == BattleState.BattleOver) yield break;
    //            }
    //        }
    //        else
    //        {
    //            if (playerAction == BattleAction.SwitchPokemon)
    //            {
    //                var selectedPokemon = playerParty.Pokemons[currentMember];
    //                state = BattleState.Busy;
    //                yield return SwitchPokemon(selectedPokemon);
    //            }

    //            // Enemy Turn
    //            var enemyMove = enemyUnit.Pergunta.GetRandomMove();
    //            yield return RunMove(enemyUnit, playerUnit, enemyMove);
    //            yield return RunAfterTurn(enemyUnit);
    //            if (state == BattleState.BattleOver) yield break;
    //        }

    //        if (state != BattleState.BattleOver)
    //            ActionSelection();
    //    }

    //    IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnit, Move move)
    //    {
    //        bool canRunMove = sourceUnit.Pergunta.OnBeforeMove();
    //        if (!canRunMove)
    //        {
    //            yield return ShowStatusChanges(sourceUnit.Pergunta);
    //            yield return sourceUnit.Hud.UpdateHP();
    //            yield break;
    //        }
    //        yield return ShowStatusChanges(sourceUnit.Pergunta);

    //        move.PP--;
    //        yield return dialogBox.TypeDialog($"{sourceUnit.Pergunta.Base.Name} used {move.Base.Name}");

    //        if (CheckIfMoveHits(move, sourceUnit.Pergunta, targetUnit.Pergunta))
    //        {

    //            sourceUnit.PlayAttackAnimation();
    //            yield return new WaitForSeconds(1f);
    //            targetUnit.PlayHitAnimation();

    //            if (move.Base.Category == MoveCategory.Status)
    //            {
    //                yield return RunMoveEffects(move.Base.Effects, sourceUnit.Pergunta, targetUnit.Pergunta, move.Base.Target);
    //            }
    //            else
    //            {
    //                var damageDetails = targetUnit.Pergunta.TakeDamage(move, sourceUnit.Pergunta);
    //                yield return targetUnit.Hud.UpdateHP();
    //                yield return ShowDamageDetails(damageDetails);
    //            }

    //            if (move.Base.Secondaries != null && move.Base.Secondaries.Count > 0 && targetUnit.Pergunta.HP > 0)
    //            {
    //                foreach (var secondary in move.Base.Secondaries)
    //                {
    //                    var rnd = UnityEngine.Random.Range(1, 101);
    //                    if (rnd <= secondary.Chance)
    //                        yield return RunMoveEffects(secondary, sourceUnit.Pergunta, targetUnit.Pergunta, secondary.Target);
    //                }
    //            }

    //            if (targetUnit.Pergunta.HP <= 0)
    //            {
    //                yield return dialogBox.TypeDialog($"{targetUnit.Pergunta.Base.Name} Fainted");
    //                targetUnit.PlayFaintAnimation();
    //                yield return new WaitForSeconds(2f);

    //                CheckForBattleOver(targetUnit);
    //            }

    //        }
    //        else
    //        {
    //            yield return dialogBox.TypeDialog($"{sourceUnit.Pergunta.Base.Name}'s attack missed");
    //        }
    //    }

    //    IEnumerator RunMoveEffects(MoveEffects effects, Pokemon source, Pokemon target, MoveTarget moveTarget)
    //    {
    //        // Stat Boosting
    //        if (effects.Boosts != null)
    //        {
    //            if (moveTarget == MoveTarget.Self)
    //                source.ApplyBoosts(effects.Boosts);
    //            else
    //                target.ApplyBoosts(effects.Boosts);
    //        }

    //        // Status Condition
    //        if (effects.Status != ConditionID.none)
    //        {
    //            target.SetStatus(effects.Status);
    //        }

    //        // Volatile Status Condition
    //        if (effects.VolatileStatus != ConditionID.none)
    //        {
    //            target.SetVolatileStatus(effects.VolatileStatus);
    //        }

    //        yield return ShowStatusChanges(source);
    //        yield return ShowStatusChanges(target);
    //    }

    //    IEnumerator RunAfterTurn(BattleUnit sourceUnit)
    //    {
    //        if (state == BattleState.BattleOver) yield break;
    //        yield return new WaitUntil(() => state == BattleState.RunningTurn);

    //        // Statuses like burn or psn will hurt the pokemon after the turn
    //        sourceUnit.Pergunta.OnAfterTurn();
    //        yield return ShowStatusChanges(sourceUnit.Pergunta);
    //        yield return sourceUnit.Hud.UpdateHP();
    //        if (sourceUnit.Pergunta.HP <= 0)
    //        {
    //            yield return dialogBox.TypeDialog($"{sourceUnit.Pergunta.Base.Name} Fainted");
    //            sourceUnit.PlayFaintAnimation();
    //            yield return new WaitForSeconds(2f);

    //            CheckForBattleOver(sourceUnit);
    //            yield return new WaitUntil(() => state == BattleState.RunningTurn);
    //        }
    //    }

    //    bool CheckIfMoveHits(Move move, Pokemon source, Pokemon target)
    //    {
    //        if (move.Base.AlwaysHits)
    //            return true;

    //        float moveAccuracy = move.Base.Accuracy;

    //        int accuracy = source.StatBoosts[Stat.Accuracy];
    //        int evasion = target.StatBoosts[Stat.Evasion];

    //        var boostValues = new float[] { 1f, 4f / 3f, 5f / 3f, 2f, 7f / 3f, 8f / 3f, 3f };

    //        if (accuracy > 0)
    //            moveAccuracy *= boostValues[accuracy];
    //        else
    //            moveAccuracy /= boostValues[-accuracy];

    //        if (evasion > 0)
    //            moveAccuracy /= boostValues[evasion];
    //        else
    //            moveAccuracy *= boostValues[-evasion];

    //        return UnityEngine.Random.Range(1, 101) <= moveAccuracy;
    //    }

    //    IEnumerator ShowStatusChanges(Pokemon pokemon)
    //    {
    //        while (pokemon.StatusChanges.Count > 0)
    //        {
    //            var message = pokemon.StatusChanges.Dequeue();
    //            yield return dialogBox.TypeDialog(message);
    //        }
    //    }

    //    void CheckForBattleOver(BattleUnit faintedUnit)
    //    {
    //        if (faintedUnit.IsPlayerUnit)
    //        {
    //            var nextPokemon = playerParty.GetPerguntaNaoRespondida();
    //            if (nextPokemon != null)
    //                OpenPartyScreen();
    //            else
    //                BattleOver(false);
    //        }
    //        else
    //        {
    //            if (!isTrainerBattle)
    //            {
    //                BattleOver(true);
    //            }
    //            else
    //            {
    //                var nextPokemon = trainerParty.GetPerguntaNaoRespondida();
    //                if (nextPokemon != null)
    //                    StartCoroutine(AboutToUse(nextPokemon));
    //                else
    //                    BattleOver(true);
    //            }
    //        }
    //    }

    //    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    //    {
    //        if (damageDetails.Critical > 1f)
    //            yield return dialogBox.TypeDialog("A critical hit!");

    //        if (damageDetails.TypeEffectiveness > 1f)
    //            yield return dialogBox.TypeDialog("It's super effective!");
    //        else if (damageDetails.TypeEffectiveness < 1f)
    //            yield return dialogBox.TypeDialog("It's not very effective!");
    //    }

    //    public void HandleUpdate()
    //    {
    //        if (state == BattleState.ActionSelection)
    //        {
    //            HandleActionSelection();
    //        }
    //        else if (state == BattleState.MoveSelection)
    //        {
    //            HandleMoveSelection();
    //        }
    //        else if (state == BattleState.PartyScreen)
    //        {
    //            HandlePartySelection();
    //        }
    //        else if (state == BattleState.AboutToUse)
    //        {
    //            HandleAboutToUse();
    //        }
    //    }

    //    void HandleActionSelection()
    //    {
    //        if (Input.GetKeyDown(KeyCode.RightArrow))
    //            ++currentAction;
    //        else if (Input.GetKeyDown(KeyCode.LeftArrow))
    //            --currentAction;
    //        else if (Input.GetKeyDown(KeyCode.DownArrow))
    //            currentAction += 2;
    //        else if (Input.GetKeyDown(KeyCode.UpArrow))
    //            currentAction -= 2;

    //        currentAction = Mathf.Clamp(currentAction, 0, 3);

    //        dialogBox.UpdateActionSelection(currentAction);

    //        if (Input.GetKeyDown(KeyCode.Z))
    //        {
    //            if (currentAction == 0)
    //            {
    //                // Fight
    //                MoveSelection();
    //            }
    //            else if (currentAction == 1)
    //            {
    //                // Bag
    //            }
    //            else if (currentAction == 2)
    //            {
    //                // Pokemon
    //                prevState = state;
    //                OpenPartyScreen();
    //            }
    //            else if (currentAction == 3)
    //            {
    //                // Run
    //            }
    //        }
    //    }

    //    void HandleMoveSelection()
    //    {
    //        if (Input.GetKeyDown(KeyCode.RightArrow))
    //            ++currentMove;
    //        else if (Input.GetKeyDown(KeyCode.LeftArrow))
    //            --currentMove;
    //        else if (Input.GetKeyDown(KeyCode.DownArrow))
    //            currentMove += 2;
    //        else if (Input.GetKeyDown(KeyCode.UpArrow))
    //            currentMove -= 2;

    //        currentMove = Mathf.Clamp(currentMove, 0, playerUnit.Pergunta.Alternativas.Count - 1);

    //        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pergunta.Alternativas[currentMove]);

    //        if (Input.GetKeyDown(KeyCode.Z))
    //        {
    //            var move = playerUnit.Pergunta.Alternativas[currentMove];
    //            if (move.PP == 0) return;

    //            dialogBox.EnableMoveSelector(false);
    //            dialogBox.EnableDialogText(true);
    //            StartCoroutine(RunTurns(BattleAction.Move));
    //        }
    //        else if (Input.GetKeyDown(KeyCode.X))
    //        {
    //            dialogBox.EnableMoveSelector(false);
    //            dialogBox.EnableDialogText(true);
    //            ActionSelection();
    //        }
    //    }

    //    void HandlePartySelection()
    //    {
    //        if (Input.GetKeyDown(KeyCode.RightArrow))
    //            ++currentMember;
    //        else if (Input.GetKeyDown(KeyCode.LeftArrow))
    //            --currentMember;
    //        else if (Input.GetKeyDown(KeyCode.DownArrow))
    //            currentMember += 2;
    //        else if (Input.GetKeyDown(KeyCode.UpArrow))
    //            currentMember -= 2;

    //        currentMember = Mathf.Clamp(currentMember, 0, playerParty.Pokemons.Count - 1);

    //        partyScreen.UpdateMemberSelection(currentMember);

    //        if (Input.GetKeyDown(KeyCode.Z))
    //        {
    //            var selectedMember = playerParty.Pokemons[currentMember];
    //            if (selectedMember.HP <= 0)
    //            {
    //                partyScreen.SetMessageText("You can't send out a fainted pokemon");
    //                return;
    //            }
    //            if (selectedMember == playerUnit.Pergunta)
    //            {
    //                partyScreen.SetMessageText("You can't switch with the same pokemon");
    //                return;
    //            }

    //            partyScreen.gameObject.SetActive(false);

    //            if (prevState == BattleState.ActionSelection)
    //            {
    //                prevState = null;
    //                StartCoroutine(RunTurns(BattleAction.SwitchPokemon));
    //            }
    //            else
    //            {
    //                state = BattleState.Busy;
    //                StartCoroutine(SwitchPokemon(selectedMember));
    //            }
    //        }
    //        else if (Input.GetKeyDown(KeyCode.X))
    //        {
    //            if (playerUnit.Pergunta.HP <= 0)
    //            {
    //                partyScreen.SetMessageText("You have to choose a pokemon to continue");
    //                return;
    //            }

    //            partyScreen.gameObject.SetActive(false);

    //            if (prevState == BattleState.AboutToUse)
    //            {
    //                prevState = null;
    //                StartCoroutine(SendNextTrainerPokemon());
    //            }
    //            else
    //                ActionSelection();
    //        }
    //    }

    //    void HandleAboutToUse()
    //    {
    //        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
    //            aboutToUseChoice = !aboutToUseChoice;

    //        dialogBox.UpdateChoiceBox(aboutToUseChoice);

    //        if (Input.GetKeyDown(KeyCode.Z))
    //        {
    //            dialogBox.EnableChoiceBox(false);
    //            if (aboutToUseChoice == true)
    //            {
    //                // Yes Option
    //                prevState = BattleState.AboutToUse;
    //                OpenPartyScreen();
    //            }
    //            else
    //            {
    //                // No Option
    //                StartCoroutine(SendNextTrainerPokemon());
    //            }
    //        }
    //        else if (Input.GetKeyDown(KeyCode.X))
    //        {
    //            dialogBox.EnableChoiceBox(false);
    //            StartCoroutine(SendNextTrainerPokemon());
    //        }
    //    }

    //    IEnumerator SwitchPokemon(Pokemon newPokemon)
    //    {
    //        if (playerUnit.Pergunta.HP > 0)
    //        {
    //            yield return dialogBox.TypeDialog($"Come back {playerUnit.Pergunta.Base.Name}");
    //            playerUnit.PlayFaintAnimation();
    //            yield return new WaitForSeconds(2f);
    //        }

    //        playerUnit.Setup(newPokemon);
    //        dialogBox.SetMoveNames(newPokemon.Alternativas);
    //        yield return dialogBox.TypeDialog($"Go {newPokemon.Base.Name}!");

    //        if (prevState == null)
    //        {
    //            state = BattleState.RunningTurn;
    //        }
    //        else if (prevState == BattleState.AboutToUse)
    //        {
    //            prevState = null;
    //            StartCoroutine(SendNextTrainerPokemon());
    //        }
    //    }

    //    IEnumerator SendNextTrainerPokemon()
    //    {
    //        state = BattleState.Busy;

    //        var nextPokemon = trainerParty.GetPerguntaNaoRespondida();
    //        enemyUnit.Setup(nextPokemon);
    //        yield return dialogBox.TypeDialog($"{professor.Name} send out {nextPokemon.Base.Name}!");

    //        state = BattleState.RunningTurn;
    //    }
}
