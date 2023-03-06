using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UserInfo : MonoBehaviour
{

    public string Username;
    public int Diamond;
    public int ConsumedDiamond = 0;
    public int Gotcha_price;
    public ClassJob classJob;
    public bool isSelecting = false;

    public List<Card>[] CardOwned = new List<Card>[6];
    public RandomSelect[] classCards;
    public Text DiamondTxt, ConsumedDiaTxt;
    public GameObject OwnedCardsUI, TrysParent;
    public int[] Trys = { 0, 0, 0, 0 };

    private void Start()
    {
        for(int i=0; i < 6; i++)
        {
            CardOwned[i] = new List<Card>();
        }

    }

    private void Update()
    {
        DiamondTxt.text = Diamond.ToString();
        ConsumedDiaTxt.text = ConsumedDiamond.ToString();
        for(int i = 0; i < 4; i++)
        {
            TrysParent.transform.GetChild(i).GetComponent<Text>().text = Trys[i].ToString();
        }

        //보유중인 카드 카운팅
        if (CardOwned != null)
        {
            //보유중인 카드 수 표시
            for (int i = 0; i < 6; i++)
            {
                OwnedCardsUI.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = CardOwned[i].Count.ToString();
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

    public void DiamondRecharge(int amount = 100000)
    {
        Diamond += amount;
    }

    public void Gatcha()
    {
        if (Diamond >= Gotcha_price && !isSelecting)
        {
            Diamond -= Gotcha_price;
            ConsumedDiamond += Gotcha_price;
            List<Card> cardtmp = new List<Card>();
            StartCoroutine(delay());
            cardtmp = classCards[(int)this.classJob].ResultSelect();
            for(int i = 0; i < cardtmp.Count; i++)
            {
                CardOwned[(int)cardtmp[i].cardGrade].Add(cardtmp[i]);
            }
        }
        else
        {
            //다이아 충전하라고 하기
        }
    }
    public IEnumerator delay()
    {
        isSelecting = true;
        yield return new WaitForSeconds(1.5f);
        isSelecting = false;
    }

}
