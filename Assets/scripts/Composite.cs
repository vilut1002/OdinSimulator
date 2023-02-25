using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite : MonoBehaviour
{
    public float[] Pcomposition = new float[5];

    public List<Card> deck = new List<Card>();  // ī�� ��
    public int total = 0;  // ī����� ����ġ �� ��
    public UserInfo User;
    public bool isSelecting = false;

    void Start()
    {
        // ����
        //ResultSelect();
    }

    public List<Card> result = new List<Card>();  // �����ϰ� ���õ� ī�带 ���� ����Ʈ

    public Transform parent;
    public GameObject cardprefab;

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

        for (int i = 0; i < deck.Count; i++)
        {
            if (selectNum <= Pcomposition[(int)cardGrade])
            {
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
