using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour
{
    public Image chr;
    public Text cardName;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(FlipAnim());
    }
    // 카드의 정보를 초기화
    public void CardUISet(Card card)
    {
        chr.sprite = card.cardImage;
        cardName.text = card.cardName;
    }
    public IEnumerator FlipAnim()
    {
        yield return new WaitForSeconds(0.7f);
        animator.SetTrigger("Flip");

    }
}
