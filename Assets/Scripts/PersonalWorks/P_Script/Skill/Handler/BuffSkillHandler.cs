using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 
public class BuffSkillHandler : SkillHandler
{
    [Header("Skill Sprite")]
    public GameObject buffObject; // 뾰로롱 버프

    [Header("Skill Stats")]
    public float buffTime = 3f;
    public float buffDamage = 1.5f;
    public float buffDefense = 1.5f;
    public float buffSpeed = 1.5f;


    public override void AwakeningAction()
    {
        // 쿨타임이 됬는지 확인
        if (!CheckCoolDown()) return;

        // TODO : 버프와 함께 Sprite 오브젝트 생성
        CreateBuff();
    }

    public override void NormalAction()
    {
        // 쿨타임이 됬는지 확인
        if (!CheckCoolDown()) return;

        // TODO : 버프와 함께 Sprite 오브젝트 생성
        CreateBuff();
    }
    public void CreateBuff()
    {
        GameObject go = Instantiate(buffObject, SkillManager.Instance.player.transform);
        BuffSkillObject buffSkillobject = go.GetComponent<BuffSkillObject>();

        buffSkillobject.Init(SkillManager.Instance.player.gameObject.GetComponent<BaseController>(), Color.blue, duration, knockbackPower, knockbackDuration);
    }
}
