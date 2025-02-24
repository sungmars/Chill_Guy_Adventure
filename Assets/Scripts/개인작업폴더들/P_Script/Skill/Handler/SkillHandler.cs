using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class SkillHandler : MonoBehaviour
{
    [Header("Skill Info")]
    public string _name; // 스킬 이름
    public string normalDesc; // 기본 스킬 설명
    public string awakeningDesc; // 각성 스킬 설명
    public Sprite icon; // 스킬 아이콘
    public bool isAwakening = false; // 각성 여부

    [Header("Skill CoolTime")]
    public float coolDown = 10f;
    public float currentCoolDown = 10f;

    [Header("Skill Duration")]
    public float duration = 5f; // 최대 생존 시간



    [HideInInspector] private SkillUI skillUI;

    public abstract void NormalAction(); // 각성 업글 시 설명과 효과가 바뀜
    public abstract void AwakeningAction(); // 각성 업글 시 설명과 효과가 바뀜

    public void Init(SkillUI skillUI)
    {
        this.skillUI = skillUI;
        skillUI.iconImage.sprite = icon;
        ImageFillAmount();

        skillUI.tooltipTitle.text = _name;
        skillUI.tooltipDesc.text = normalDesc;
    }

    protected bool CheckCoolDown()
    {
        if (currentCoolDown < coolDown) return false;
        CoolDownStart();
        return true;
    }

    public void CoolDownStart()
    {
        currentCoolDown = 0;
        StartCoroutine(CoolDownTimer());
    }

    protected IEnumerator CoolDownTimer()
    {
        while (currentCoolDown < coolDown)
        {
            currentCoolDown += Time.deltaTime;
            ImageFillAmount();
            yield return null;// + 조건
        }
    }

    public void ImageFillAmount()
    {
        skillUI.coolImage.fillAmount = 1 - currentCoolDown / coolDown;
    }

}
