using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AulaDialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBoxPref;
    [SerializeField] int lettersPerSecond;

    Text dialogText;
    GameObject dialogBox;
    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public static AulaDialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    Action onDialogFinished;

    int currentLine = 0;
    bool isTyping;

    public bool IsShowing { get; private set; }

    public IEnumerator ShowDialog(Dialog dialog, Action onFinished = null)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        IsShowing = true;
        this.dialog = dialog;
        onDialogFinished = onFinished;

        dialogBox = Instantiate(dialogBoxPref, new Vector3(0,0,0), Quaternion.identity);
        dialogBox.transform.SetParent(transform.parent, false);
        dialogText = dialogBox.GetComponentInChildren<Text>();
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            if (dialogText.text == dialog.Lines[currentLine])
            {
                ++currentLine;
                if (currentLine < dialog.Lines.Count)
                {
                    dialogBox = Instantiate(dialogBoxPref, new Vector3(0, 0, 0), Quaternion.identity);
                    dialogBox.transform.SetParent(transform.parent, false);
                    dialogText = dialogBox.GetComponentInChildren<Text>();
                    StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
                }
                else
                {
                    currentLine = 0;
                    IsShowing = false;
                    dialogBox.SetActive(false);
                    onDialogFinished?.Invoke();
                    OnCloseDialog?.Invoke();
                }
            }

        }
        else if (isTyping && Input.GetKeyDown(KeyCode.Z))
        {
            isTyping = false;
            StopAllCoroutines();
            dialogText.text = dialog.Lines[currentLine];
        }
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }

}
