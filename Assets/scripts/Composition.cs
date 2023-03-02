using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composition : MonoBehaviour
{
    public float[] Pcomposition = new float[6];

    [System.Serializable]
    public class Deck
    {
        public List<Card> deck = new List<Card>();
        public double total = 0;
        public CardGrade cardGrade;

        public Deck()
        {
            this.deck = deck;
            this.cardGrade = cardGrade;
            for (int i = 0; i < this.deck.Count; i++)
            {
                this.total += deck[i].weight;
            }
        }
    }


    public Deck MythDeck = new Deck();
    public Deck LegendDeck = new Deck();
    public Deck HeroDeck = new Deck();
    public Deck RareDeck = new Deck();
    public Deck HighDeck = new Deck();
    public Deck NormalDeck = new Deck();


    public Deck[] decks = new Deck[6];

    public List<Card> CompositeDeck = new List<Card>();  // 합성 카드 덱
    public UserInfo User;
    public bool isSelecting = false;

    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트

    public Transform parent;
    public GameObject cardprefab;

    void Start()
    {
        decks[0] = MythDeck;
        decks[1] = LegendDeck;
        decks[2] = HeroDeck;
        decks[3] = RareDeck;
        decks[4] = HighDeck;
        decks[5] = NormalDeck;
        for(int i=0; i < 6; i++)
        {
            for(int j=0; j < decks[i].deck.Count; j++)
            {
                decks[i].total += decks[i].deck[j].weight;
            }
        }
    }

    public void TMP()
    {

    }

    public void Composite(int idx)
    {
        Debug.Log("들어옴");
        if (User.CardOwned[idx].Count >= 4)
        {
            List<Card> cardtmp = new List<Card>();
            StartCoroutine(delay());
            cardtmp = ResultSelect((CardGrade)idx);
            for (int i = 0; i < cardtmp.Count; i++)
            {
                User.CardOwned[(int)cardtmp[i].cardGrade].Add(cardtmp[i]);
            }
        }
    }

    public List<Card> ResultSelect(CardGrade cardGrade)
    {
        result = new List<Card>();
        for (int j = 0; j < parent.childCount; j++)
        {
            Destroy(parent.GetChild(j).gameObject);
        }
        for (int i = 0; i < 11; i++)
        {   
            
            if (User.CardOwned[(int)cardGrade].Count >= 4)
            {
                //유저 카드 4개 삭제해야함!!!!
                User.CardOwned[(int)cardGrade].RemoveAt(0);
                User.CardOwned[(int)cardGrade].RemoveAt(0);
                User.CardOwned[(int)cardGrade].RemoveAt(0);
                User.CardOwned[(int)cardGrade].RemoveAt(0);
                // 확률을 돌리면서 결과 리스트에 넣어줍니다.
                result.Add(compositeCard(cardGrade));
                // 비어 있는 카드를 생성하고
                CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
                // 생성 된 카드에 결과 리스트의 정보를 넣어줍니다.
                cardUI.CardUISet(result[i]);
            }
        }
        return result;
    }
    // 가중치 랜덤의 설명은 영상을 참고.
    public Card compositeCard(CardGrade cardGrade)
    {
        double weight = 0;
        double selectNum = 0;

        CompositeDeck = decks[(int)cardGrade].deck;

        selectNum = decks[(int)cardGrade].total*Random.Range(0.0f, 1.0f);

        for (int i = 0; i < CompositeDeck.Count; i++)
        {
            weight += CompositeDeck[i].weight;
            if (selectNum <= weight)
            {
                Card temp = new Card(CompositeDeck[i]);
                return temp;
            }
        }
        return null;
    }

    public IEnumerator delay()
    {
        isSelecting = true;
        yield return new WaitForSeconds(1.7f);
        isSelecting = false;
    }



}
