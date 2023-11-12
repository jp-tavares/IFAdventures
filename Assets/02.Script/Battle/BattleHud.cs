using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] Text statusText;
    [SerializeField] HPBar hpBar;

    [SerializeField] Color psnColor;
    [SerializeField] Color brnColor;
    [SerializeField] Color slpColor;
    [SerializeField] Color parColor;
    [SerializeField] Color frzColor;

    Pergunta _pergunta;

    public void SetData(Pergunta pergunta)
    {
        _pergunta = pergunta;

        //Seta o nome e o level
        nameText.text = pergunta.Base.Name;

        //Seta o enunciado


        //Seta as alternativas
    }
}
