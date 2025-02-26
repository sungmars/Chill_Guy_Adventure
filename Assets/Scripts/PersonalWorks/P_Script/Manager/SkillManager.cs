using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillManager : MonoSingleton<SkillManager>
{
    public Transform player;
    [Header("Skill Lists")]
    public List<SkillHandler> rangeSkillHandlerList;
    public List<SkillHandler> areaSkillHandlerList;
    public List<SkillHandler> buffSkillHandlerList;
    public List<SkillHandler> allSkillHandlerList;

    [Header("Skill UI")]
    public Transform currentSkillPivot;
    public List<SkillHandler> mouseSkillHandlers; // 왼쪽 0, 오른쪽 1
    public List<SkillUI> mouseSkillUIs; // 왼쪽 0, 오른쪽 1


    public List<SkillUI> getSkillUIs; // 1, 2, 3 UI
    public List<SkillHandler> getSkillHandlerUIs; // 1, 2, 3 UI에 대한 정보

    public GetSkillUI getSkillUIGroup;


    new void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        allSkillHandlerList = new List<SkillHandler>();
        allSkillHandlerList.AddRange(rangeSkillHandlerList);
        allSkillHandlerList.AddRange(areaSkillHandlerList);
        allSkillHandlerList.AddRange(buffSkillHandlerList);
    }

    void Start()
    {
        // OnClickSetRandomSkill();
        GetSkillSetting();
        // 왼쪽 오른쪽 스킬 등록
        SetSkillSettingLeftRight(GameManager.Instance.mouseSkill.left, GameManager.Instance.mouseSkill.right);
    }

    public void OpenGetSkillPannel()
    {
        getSkillUIGroup.gameObject.SetActive(true);
    }

    public void OnClickSetSkill()
    {
        int skillOrder = getSkillUIGroup.GetSkillOrder();
        if (getSkillUIGroup.GetMouseOrder() == 3)
        {
            SetSkill(0, getSkillHandlerUIs[skillOrder]);

            // 최적화 망했지만 머리와 손은 편해지는 마법
            for (int i = 0; i < allSkillHandlerList.Count; i++)
            {
                if (allSkillHandlerList[i]._name == getSkillHandlerUIs[skillOrder]._name)
                {
                    GameManager.Instance.mouseSkill.left = i;
                    break;
                }
            }
        }
        else
        {
            SetSkill(1, getSkillHandlerUIs[skillOrder]);
            // 최적화 망했지만 머리와 손은 편해지는 마법
            for (int i = 0; i < allSkillHandlerList.Count; i++)
            {
                if (allSkillHandlerList[i]._name == getSkillHandlerUIs[skillOrder]._name)
                {
                    GameManager.Instance.mouseSkill.right = i;
                    break;
                }
            }
        }
        OnClickNextStage();
    }

    public void OnClickNextStage()
    {
        Debug.Log("스킬 선택 후 라운드 넘어가기");
        GameManager.Instance.NextRound();
    }


    public void SetSkillSettingLeftRight(int left, int right)
    {
        SetSkill(0, allSkillHandlerList[left]);
        SetSkill(1, allSkillHandlerList[right]);
    }

    public void GetSkillSetting()
    {
        var temp = GetRandomSkillThree();
        for (int i = 0; i < getSkillUIs.Count; i++)
        {
            getSkillUIs[i].iconImage.sprite = temp[i].icon;
            getSkillUIs[i].tooltipTitle.text = temp[i]._name;
            getSkillUIs[i].tooltipDesc.text = temp[i].normalDesc;
            getSkillHandlerUIs.Add(temp[i]);
        }
    }

    public List<SkillHandler> GetRandomSkillThree()
    {
        return allSkillHandlerList.GetRandomItems(3);
    }

    public void OnClickSetRandomSkill()
    {
        SetSkill(0, GetRandomSkillHandlerList().GetRandomItem());
        SetSkill(1, GetRandomSkillHandlerList().GetRandomItem());
    }

    public List<SkillHandler> GetRandomSkillHandlerList()
    {
        int getListType = UnityEngine.Random.Range(0, 3);

        // 선택된 리스트를 저장할 변수 선언
        List<SkillHandler> selectedList = null;

        // switch 문을 사용해 리스트 선택
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