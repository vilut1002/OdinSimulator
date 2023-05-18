using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck : MonoBehaviour
{

    public List<Card> deck = new List<Card>();
    public double total_weight = 0;
    public CardGrade cardGrade;

    private void Start()
    {

        for (int i = 0; i < this.deck.Count; i++)
        {
            this.total_weight += deck[i].GetWeight();
        }
    }

}
