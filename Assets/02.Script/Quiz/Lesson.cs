using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Lesson", menuName = "Lesson/Create new lesson")]
public class Lesson : ScriptableObject
{
    [SerializeField] Dialog falasAntesDaInteracao;
    [SerializeField] Dialog falasDuranteInteracao;
    [SerializeField] Dialog falasDepoisDaInteracao; 
    [SerializeField] List<Pergunta> perguntas;

    public List<Pergunta> Perguntas { get =>  perguntas; }
    public Dialog FalasAntesDaInteracao { get => falasAntesDaInteracao; }
    public Dialog FalasDuranteInteracao { get => falasDuranteInteracao; }
    public Dialog FalasDepoisDaInteracao { get => falasDepoisDaInteracao; }
}