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

    public void TMP()
    {

    }

    public void Composite(int idx)
    {
        while (User.CardOwned[idx].Count >= 4)
        {
            List<Card> cardtmp = new List<Card>();
            StartCoroutine(delay());
            cardtmp = ResultSelect((CardGrade)idx);
            for (int i = 0; i < cardtmp.Count; i++)
            {
                User.CardOwned[idx].Add(cardtmp[i]);
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
                //���� ī�� 4�� �����ؾ���!!!!
                // Ȯ���� �����鼭 ��� ����Ʈ�� �־��ݴϴ�.
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
