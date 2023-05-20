using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composition : MonoBehaviour
{
    public float[] pComposition = new float[6];


    public List<Deck> decks = new List<Deck>();

    public List<Card> compositeDeck = new List<Card>();  // �ռ� ī�� ��
    public UserInfo user;
    public bool isSelecting = false;
    public Transform deckparent;
    public List<Card> result = new List<Card>();  // �����ϰ� ���õ� ī�带 ���� ����Ʈ

    public Transform cardparent;
    public GameObject cardprefab;

    void Start()
    {
        for(int i=0; i < 6; i++)
        {
            decks.Add(deckparent.GetChild(i).GetComponent<Deck>());
        }
    }

    public void Composite(int idx)
    {
        if (user.CardOwned[idx].Count >= 4 && !isSelecting)
        {
            List<Card> cardtmp = new List<Card>();
            StartCoroutine(delay());
            cardtmp = ResultSelect((CardGrade)idx);
            for (int i = 0; i < cardtmp.Count; i++)
            {
                user.CardOwned[(int)cardtmp[i].GetCardGrade()].Add(cardtmp[i]);
            }
        }
    }

    public List<Card> ResultSelect(CardGrade cardGrade)
    {
        result = new List<Card>();
        for (int j = 0; j < cardparent.childCount; j++)
        {
            Destroy(cardparent.GetChild(j).gameObject);
        }

        for (int i = 0; i < 11; i++)
        {   
            
            if (user.CardOwned[(int)cardGrade].Count >= 4)
            {
                //���� ī�� 4�� �����ؾ���!!!!
                user.CardOwned[(int)cardGrade].RemoveAt(0);
                user.CardOwned[(int)cardGrade].RemoveAt(0);
                user.CardOwned[(int)cardGrade].RemoveAt(0);
                user.CardOwned[(int)cardGrade].RemoveAt(0);
                if ((int)cardGrade < 5 && (int)cardGrade>0)
                {
                    user.Trys[(int)cardGrade-1]+=1;
                }
                int gradeIdx = SelectGrade(cardGrade); 
                // Ȯ���� �����鼭 ��� ����Ʈ�� �־��ݴϴ�.
                result.Add(compositeCard(gradeIdx));
                // ��� �ִ� ī�带 �����ϰ�
                CardUI cardUI = Instantiate(cardprefab, cardparent).GetComponent<CardUI>();
                // ���� �� ī�忡 ��� ����Ʈ�� ������ �־��ݴϴ�.
                cardUI.CardUISet(result[i]);
            }
        }
        return result;
    }
    public int SelectGrade(CardGrade cardGrade)
    {
        float probability = pComposition[(int)cardGrade];
        double selectNum = Random.Range(0.0f, 1.0f);
        if (selectNum <= probability)
        {
            return (int)cardGrade-1;
        }
        return (int)cardGrade;
    }
    public Card compositeCard(int cardGrade)
    {
        double weight = 0;
        double selectNum = 0;

        compositeDeck = decks[cardGrade].deck;

        selectNum = decks[cardGrade].total_weight*Random.Range(0.0f, 1.0f);

        for (int i = 0; i < compositeDeck.Count; i++)
        {
            weight += compositeDeck[i].GetWeight();
            if (selectNum <= weight)
            {
                Card temp = new Card(compositeDeck[i]);
                return temp;
            }
        }
        return null;
    }

    public IEnumerator delay()
    {
        isSelecting = true;
        yield return new WaitForSeconds(1.5f);
        isSelecting = false;
    }



}
