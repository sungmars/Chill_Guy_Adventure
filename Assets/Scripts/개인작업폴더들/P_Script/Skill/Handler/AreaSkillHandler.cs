using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AreaSkillHandler : SkillHandler
{
    [Header("Skill Sprite")]
    public GameObject areaObject; // �Ϸη� ����

    [Header("Skill Stats")]
    public float damage = 1f;


    public override void AwakeningAction()
    {
        // ��Ÿ���� ����� Ȯ��
        if (!CheckCoolDown()) return;

        // ���� ������Ʈ ����
        CreateArea();
    }

    public override void NormalAction()
    {
        // ��Ÿ���� ����� Ȯ��
        if (!CheckCoolDown()) return;

        // ���� ������Ʈ ����
        CreateArea();
    }

    public void CreateArea()
    {
        GameObject go = Instantiate(areaObject, SkillManager.Instance.player);
        AreaSkillObject areaSkillobject = go.GetComponent<AreaSkillObject>();

        areaSkillobject.Init(Color.white, duration);
    }
}
