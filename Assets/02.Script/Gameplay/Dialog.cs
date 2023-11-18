using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<DialogLine> lines;

    public List<DialogLine> Lines {
        get { return lines; }
    }
}


[System.Serializable]
public class DialogLine
{
    [SerializeField] string text;
    [SerializeField] bool alignLeft = false;


    public string Text { get => text; }

    public bool AlignLeft { get => alignLeft; }
}