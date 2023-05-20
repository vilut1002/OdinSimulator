using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UserInfo : MonoBehaviour
{

    public string username;
    private int diamond;
    public int consumed_diamond = 0;
    public int gotcha_price;
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
        DiamondTxt.text = string.Format("{0:#,0}", diamond).ToString();
        ConsumedDiaTxt.text = string.Format("{0:#,0}", consumed_diamond).ToString();
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
        diamond += amount;
    }

    public void DiamondPlus(int amount = 0)
    {
        diamond += amount;
    }
    
    public void DiamondMinus(int amount = 0)
    {   
        if (diamond > amount){
            diamond -= amount;
        }
    }

    public void ConsumedDiamondPlus(int amount = 0)
    {
        consumed_diamond += amount;
    }

    public int GetDiamond()
    {
        return diamond;
    }

}
