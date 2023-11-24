using Assets._02.Script.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class AulaDialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBoxPref;
    [SerializeField] int lettersPerSecond;

    Text dialogText;
    GameObject dialogBox;

    List<GameObject> dialogBoxes = new List<GameObject>();

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

        createDialog(dialog.Lines[0]);
    }

    public void HandleUpdate()
    {
        if (Controls.confirmAction() && !isTyping)
        {
            if (dialogText.text == dialog.Lines[currentLine].Text)
            {
                ++currentLine;
                if (currentLine < dialog.Lines.Count)
                {
                    createDialog(dialog.Lines[currentLine]);
                }
                else
                {
                    currentLine = 0;
                    IsShowing = false;
                    foreach (var dialogBox in dialogBoxes)
                    {
                        Destroy(dialogBox);
                    }
                    dialogBox.SetActive(false);
                    onDialogFinished?.Invoke();
                    OnCloseDialog?.Invoke();
                }
            }

        }
        else if (isTyping && Controls.confirmAction())
        {
            isTyping = false;
            StopAllCoroutines();
            dialogText.text = dialog.Lines[currentLine].Text;
        }
    }

    public IEnumerator TypeDialog(DialogLine line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.Text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }

    private void createDialog(DialogLine dialogLine)
    {
        var panel = createPanelAlign(dialogLine);

        dialogBox = Instantiate(dialogBoxPref, new Vector3(0, 0, 0), Quaternion.identity);
        dialogBox.transform.SetParent(panel.transform, false);
        dialogText = dialogBox.GetComponentInChildren<Text>();
        if(dialogLine.Image == null) { 
            StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
        }
        else
        {
            dialogText.text = "";
            dialogBox.GetComponentInChildren<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            var image = dialogBox.transform.GetChild(1).GetComponentInChildren<Image>();
            image.sprite = dialogLine.Image;    
        }
    }

    private GameObject createPanelAlign(DialogLine dialogLine)
    {
        var panel = new GameObject();
        panel.name = "PanelDialog";

        panel.AddComponent<LayoutElement>();

        var horizontal = panel.AddComponent<HorizontalLayoutGroup>();
        horizontal.childAlignment = dialogLine.AlignLeft ? TextAnchor.MiddleLeft : TextAnchor.MiddleRight;
        horizontal.childForceExpandHeight = false;
        horizontal.childForceExpandWidth = false;

        dialogBoxes.Add(panel);

        panel.transform.SetParent(transform.parent, false);

        return panel;
    }
}
