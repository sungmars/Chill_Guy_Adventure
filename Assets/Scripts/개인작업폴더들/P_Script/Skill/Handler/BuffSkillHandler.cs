using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 
public class BuffSkillHandler : SkillHandler
{
    [Header("Skill Sprite")]
    public GameObject buffObject; // �Ϸη� ����

    [Header("Skill Stats")]
    public float buffTime = 3f;
    public float buffDamage = 1.5f;
    public float buffDefense = 1.5f;
    public float buffSpeed = 1.5f;


    public override void AwakeningAction()
    {
        // ��Ÿ���� ����� Ȯ��
        if (!CheckCoolDown()) return;

        // TODO : ������ �Բ� Sprite ������Ʈ ����
        CreateBuff();
    }

    public override void NormalAction()
    {
        // ��Ÿ���� ����� Ȯ��
        if (!CheckCoolDown()) return;

        // TODO : ������ �Բ� Sprite ������Ʈ ����
        CreateBuff();
    }
    public void CreateBuff()
    {
        GameObject go = Instantiate(buffObject, SkillManager.Instance.player.transform);
        BuffSkillObject buffSkillobject = go.GetComponent<BuffSkillObject>();

        buffSkillobject.Init(Color.blue, duration);
    }
}
