using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite : MonoBehaviour
{
    public float[] Pcomposition = new float[6];

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

    public List<Card> CompositeDeck = new List<Card>();  // �ռ� ī�� ��
    public int total = 0;  // ī����� ����ġ �� ��
    public UserInfo User;
    public bool isSelecting = false;

    public List<Card> result = new List<Card>();  // �����ϰ� ���õ� ī�带 ���� ����Ʈ

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
                // Ȯ���� �����鼭 ��� ����Ʈ�� �־��ݴϴ�.
                User.numCard[(int)cardGrade] -= 4;
                result.Add(compositeCard(cardGrade));
                // ��� �ִ� ī�带 �����ϰ�
                CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
                // ���� �� ī�忡 ��� ����Ʈ�� ������ �־��ݴϴ�.
                cardUI.CardUISet(result[i]);
            }
        }
        return result;
    }
    // ����ġ ������ ������ ������ ����.
    public Card compositeCard(CardGrade cardGrade)
    {
        float selectNum = Random.Range(0.0f, 1.0f);

        for (int i = 0; i < CompositeDeck.Count; i++)
        {
            if (selectNum <= Pcomposition[(int)cardGrade])
            {
                //������ ���и� �����Ǿ����Ƿ� ���Ͽ��� ���п� ���� Ŭ���� �� �ƹ�Ÿ �̱� ���� �ʿ�
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
