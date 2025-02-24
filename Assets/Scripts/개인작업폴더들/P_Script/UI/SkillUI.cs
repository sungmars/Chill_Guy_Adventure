using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    public Image coolImage;

    public GameObject tooltipUI; // ��ų ���� UI
    public TextMeshProUGUI tooltipTitle; // ��ų ���� UI
    public TextMeshProUGUI tooltipDesc; // ��ų ���� UI

    public void Awake()
    {
        tooltipUI.SetActive(false); // ���� UI ��Ȱ��ȭ
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipUI.SetActive(true); // ���콺 �ø��� ���� UI Ȱ��ȭ
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipUI.SetActive(false); // ���콺 ������ ���� UI ��Ȱ��ȭ
    }
}
