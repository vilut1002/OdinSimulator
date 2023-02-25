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
        StartCoroutine(flipanim());
    }
    // 카드의 정보를 초기화
    public void CardUISet(Card card)
    {
        chr.sprite = card.cardImage;
        cardName.text = card.cardName;
    }
    public IEnumerator flipanim()
    {
        yield return new WaitForSeconds(0.7f);
        animator.SetTrigger("Flip");

    }

    /*
    // 카드가 클릭되면 뒤집는 애니메이션 재생
    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Flip");
    }
    */
}
