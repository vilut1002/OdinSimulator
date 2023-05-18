using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public double[] pGotcha = new double[5];
    public double total = 0;  // 카드들의 가중치 총 합
    public Transform deckparent;
    void Start()
    { 
        for (int i = 0; i < pGotcha.Length; i++)
        {
            // 스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구해줍니다.
            total += pGotcha[i];
        }
        
    }

    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트

    public Transform cardparent;
    public GameObject cardprefab;

    
    public List<Card> ResultSelect()
    {
        result = new List<Card>();
        for (int j = 0; j < cardparent.childCount; j++)
        {
            Destroy(cardparent.GetChild(j).gameObject);
        }
        for (int i = 0; i < 11; i++)
        {
            //등급을 정해주기
            int grade = SelectGrade();
            Deck gradedeck = deckparent.GetChild(grade).GetComponent<Deck>();
            //가중치로 등급 내 카드에서 골라 결과리스트에 넣어주기
            result.Add(RandomCard(gradedeck)); 
            // 비어 있는 카드를 생성하고
            CardUI cardUI = Instantiate(cardprefab, cardparent).GetComponent<CardUI>();
            // 생성 된 카드에 결과 리스트의 정보를 넣어줍니다.
            cardUI.CardUISet(result[i]);
        }
        return result;
    }
    public int SelectGrade()
    {
        double weight = 0;
        double selectNum = 0;

        selectNum = total * Random.Range(0.0f, 1.0f);
       
        for (int i = 0; i < pGotcha.Length; i++)
        {
            Debug.Log(selectNum);
            Debug.Log(weight);
            weight += pGotcha[i];
            if (selectNum <= weight)
            {
                int gradeIdx = i + 1;
                return gradeIdx;
            }
        }
        return 0;
    }

    public Card RandomCard(Deck gradedeck)
    {
        List<Card> deck = gradedeck.deck;
        double weight = 0;
        double selectNum = 0;

        selectNum = gradedeck.total_weight * Random.Range(0.0f, 1.0f);

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].GetWeight();
            if (selectNum <= weight)
            {
                Card temp = new Card(deck[i]);
                return temp;
            }
        }
        return null;
    }

   

}
