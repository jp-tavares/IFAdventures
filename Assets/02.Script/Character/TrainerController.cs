using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerController : MonoBehaviour, Interactable
{
    [SerializeField] string trainerName;
    [SerializeField] Sprite sprite;
    [SerializeField] Lesson lesson;
    [SerializeField] GameObject exclamation;
    [SerializeField] GameObject fov;

    // State
    public bool terminouInteracao = false;
    Character character;

    // Props
    public string Name { get => trainerName; }

    public Sprite Sprite { get => sprite; }

    public Lesson Lesson { get => lesson; }


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        SetFovRotation(character.Animator.DefaultDirection);
    }

    private void Update()
    {
        character.HandleUpdate();
    }

    public void Interact(Transform initiator)
    {
        character.LookTowards(initiator.position);

        if (!terminouInteracao)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(lesson.FalasAntesDaInteracao, () =>
            {
                GameController.Instance.StartTrainerBattle(this);
            }));
        }
        else
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(lesson.FalasDepoisDaInteracao));
        }
        
    }

    public IEnumerator TriggerTrainerBattle(PlayerController player)
    {
        // Show Exclamation
        exclamation.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        exclamation.SetActive(false);

        // Walk towards the player
        Vector3 diff = player.transform.position - transform.position;
        Vector3 moveVec = diff - diff.normalized;
        moveVec = new Vector2(Mathf.Round(moveVec.x), Mathf.Round(moveVec.y));

        yield return character.Move(moveVec);

        // Show dialog
        StartCoroutine(DialogManager.Instance.ShowDialog(lesson.FalasAntesDaInteracao, () =>
        {
            GameController.Instance.StartTrainerBattle(this);
        }));
    }

    public void TerminarLicao()
    {
        terminouInteracao = true;
        fov.gameObject.SetActive(false);
    }

    private void SetFovRotation(FacingDirection dir)
    {
        float angle = 0f;
        if (dir == FacingDirection.Right) angle = 90f;
        else if (dir == FacingDirection.Up) angle = 180f;
        else if (dir == FacingDirection.Left) angle = 270;

        fov.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }
}
