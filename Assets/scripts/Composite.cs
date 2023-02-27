using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite : MonoBehaviour
{
    public float[] Pcomposition = new float[5];

    [System.Serializable]
    public class Deck
    {
        public List<Card> deck = new List<Card>();
        public int total;
        public CardGrade cardGrade;

        public Deck()
        {
            this.total = total;
            this.deck = deck;
            this.cardGrade = cardGrade;
        }
    }


    public Deck MythDeck = new Deck();
    public Deck LegendDeck = new Deck();
    public Deck HeroDeck = new Deck();
    public Deck RareDeck = new Deck();
    public Deck HighDeck = new Deck();
    public Deck NormalDeck = new Deck();


    public Deck[] decks = new Deck[6];

    public List<Card> deck = new List<Card>();  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합
    public UserInfo User;
    public bool isSelecting = false;

    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트

    public Transform parent;
    public GameObject cardprefab;

    void Start()
    {
        decks[0] = MythDeck;


    }

    public void composite(CardGrade cardGrade)
    {
        while (User.numCard[(int)cardGrade] >= 4)
        {
            List<Card> cardtmp = new List<Card>();
            StartCoroutine(delay());
            cardtmp = ResultSelect(cardGrade);
            for (int i = 0; i < cardtmp.Count; i++)
            {
                User.CardOwned.Add(cardtmp[i]);
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
            if (User.numCard[(int)cardGrade] >= 4)
            {
                // 확률을 돌리면서 결과 리스트에 넣어줍니다.
                User.numCard[(int)cardGrade] -= 4;
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
        float selectNum = Random.Range(0.0f, 1.0f);

        for (int i = 0; i < deck.Count; i++)
        {
            if (selectNum <= Pcomposition[(int)cardGrade])
            {
                //성공과 실패만 결정되었으므로 이하에는 성패에 따른 클래스 내 아바타 뽑기 구현 필요
                Card temp = new Card(deck[i]);
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
