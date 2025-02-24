using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class SkillHandler : MonoBehaviour
{
    [Header("Skill Info")]
    public string _name; // ��ų �̸�
    public string normalDesc; // �⺻ ��ų ����
    public string awakeningDesc; // ���� ��ų ����
    public Sprite icon; // ��ų ������
    public bool isAwakening = false; // ���� ����

    [Header("Skill CoolTime")]
    public float coolDown = 10f;
    public float currentCoolDown = 10f;

    [Header("Skill Duration")]
    public float duration = 5f; // �ִ� ���� �ð�



    [HideInInspector] private SkillUI skillUI;

    public abstract void NormalAction(); // ���� ���� �� ����� ȿ���� �ٲ�
    public abstract void AwakeningAction(); // ���� ���� �� ����� ȿ���� �ٲ�

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
            yield return null;// + ����
        }
    }

    public void ImageFillAmount()
    {
        skillUI.coolImage.fillAmount = 1 - currentCoolDown / coolDown;
    }

}
