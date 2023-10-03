using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    FreeRoam,
    Battle,
    Dialog,
}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Camera worldCamera;

    GameState state;

    void Start()
    {
        DialogManager.instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        
        DialogManager.instance.OnCloseDialog += () =>
        {
            if(state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if(state == GameState.Dialog)
        {
            DialogManager.instance.HandleUpdate();
        }
    }
}
