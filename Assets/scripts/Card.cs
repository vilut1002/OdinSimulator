using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardGrade { Myth, Legend, Hero, Rare, High, Normal}

[System.Serializable]
public class Card 
{
    public string cardName;
    public Sprite cardImage;
    public CardGrade cardGrade;
    public double weight;

    public Card(Card card)
    {
        this.cardName = card.cardName;
        this.cardImage = card.cardImage;
        this.cardGrade = card.cardGrade;
        this.weight = card.weight;
    }
}
