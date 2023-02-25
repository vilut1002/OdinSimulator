using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    public string Username;
    public int Diamond;
    public int Gotcha_price;
    public ClassJob classJob;
    public bool isSelecting = false;

    public List<Card> CardOwned = new List<Card>();
    public int[] numCard = new int[6];
    public RandomSelect[] classCards;
    public Text DiamondTxt;
    public GameObject OwnedCardsUI;

    private void Update()
    {
        DiamondTxt.text = Diamond.ToString();

        //보유중인 카드 카운팅
        if (CardOwned != null)
        {
            numCard = new int[6];
            foreach (var cards in CardOwned)
            {
                switch (cards.cardGrade)
                {
                    case CardGrade.Normal:
                        numCard[(int)cards.cardGrade] += 1;
                        break;
                    case CardGrade.High:
                        numCard[(int)cards.cardGrade] += 1;
                        break;
                    case CardGrade.Rare:
                        numCard[(int)cards.cardGrade] += 1;
                        break;
                    case CardGrade.Hero:
                        numCard[(int)cards.cardGrade] += 1;
                        break;
                    case CardGrade.Legend:
                        numCard[(int)cards.cardGrade] += 1;
                        break;
                    case CardGrade.Myth:
                        numCard[(int)cards.cardGrade] += 1;
                        break;
                }
            }
            //보유중인 카드 수 표시
            for (int i = 0; i < 6; i++)
            {
                OwnedCardsUI.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = numCard[i].ToString();
            }
        }
    }

    public enum ClassJob
    {
        Warrior,
        Rogue,
        Sorceress,
        Priest
    }

    public void Gatcha()
    {
        if (Diamond >= Gotcha_price && !isSelecting)
        {
            Diamond -= Gotcha_price;
            List<Card> cardtmp = new List<Card>();
            StartCoroutine(delay());
            cardtmp = classCards[(int)this.classJob].ResultSelect();
            for(int i = 0; i < cardtmp.Count; i++)
            {
                CardOwned.Add(cardtmp[i]);
            }
            
        }
        else
        {
            //충전하라고 하기
        }
    }
    public IEnumerator delay()
    {
        isSelecting = true;
        yield return new WaitForSeconds(1.7f);
        isSelecting = false;
    }

}
