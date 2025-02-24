using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillManager : MonoSingleton<SkillManager>
{
    public Transform player;
    [Header("Skill Lists")]
    public List<SkillHandler> rangeSkillHandlerList;
    public List<SkillHandler> areaSkillHandlerList;
    public List<SkillHandler> buffSkillHandlerList;

    [Header("Skill UI")]
    public Transform currentSkillPivot;
    public List<SkillHandler> mouseSkillHandlers; // ���� 0, ������ 1
    public List<SkillUI> mouseSkillUIs; // ���� 0, ������ 1
    new void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        OnClickSetRandomSkill();
    }

    public void OnClickSetRandomSkill()
    {

        SetSkill(0, GetRandomSkillHandlerList().GetRandomItem());
        SetSkill(1, GetRandomSkillHandlerList().GetRandomItem());
    }

    public List<SkillHandler> GetRandomSkillHandlerList()
    {
        int getListType = UnityEngine.Random.Range(0, 3);

        // ���õ� ����Ʈ�� ������ ���� ����
        List<SkillHandler> selectedList = null;

        // switch ���� ����� ����Ʈ ����
        switch (getListType)
        {
            case 0:
                selectedList = rangeSkillHandlerList;
                break;
            case 1:
                selectedList = areaSkillHandlerList;
                break;
            case 2:
                selectedList = buffSkillHandlerList;
                break;
            default:
                selectedList = rangeSkillHandlerList;
                break;
        }
        return selectedList;
    }

    public void SetSkill(int order, SkillHandler skillHandler)
    {
        if (mouseSkillHandlers[order] != null) Destroy(mouseSkillHandlers[order].gameObject);

        GameObject go = Instantiate(skillHandler.gameObject, currentSkillPivot);
        SkillHandler tempSkillHandler = go.GetComponent<SkillHandler>();
        mouseSkillHandlers[order] = tempSkillHandler;
        mouseSkillHandlers[order].Init(mouseSkillUIs[order]);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            SkillAction(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            SkillAction(1);
        }

    }

    public void SkillAction(int _index)
    {
        if (mouseSkillHandlers[_index].isAwakening)
        {
            mouseSkillHandlers[_index].AwakeningAction();
        }
        else
        {
            mouseSkillHandlers[_index].NormalAction();
        }
    }

}