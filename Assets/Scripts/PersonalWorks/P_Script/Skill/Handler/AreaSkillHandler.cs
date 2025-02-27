using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AreaSkillHandler : SkillHandler
{
    [Header("Skill Sprite")]
    public GameObject areaObject; // 뾰로롱 버프

    [Header("Skill Stats")]
    public float damage = 1f;
    public float damageInterval = 1f;
    //오디오 선언
    public AudioClip skillAudio;

    public override void AwakeningAction()
    {
        // 쿨타임이 됬는지 확인
        if (!CheckCoolDown()) return;

        // 지역 오브젝트 생성
        CreateArea();
    }

    public override void NormalAction()
    {
        // 쿨타임이 됬는지 확인
        if (!CheckCoolDown()) return;

        // 지역 오브젝트 생성
        CreateArea();
    }

    public void CreateArea()
    {
        AudioManager.Instance.PlayPlayerSound(skillAudio);
        GameObject go = Instantiate(areaObject, SkillManager.Instance.player);
        AreaSkillObject areaSkillobject = go.GetComponent<AreaSkillObject>();

        areaSkillobject.Init(SkillManager.Instance.player.gameObject.GetComponent<BaseController>(), Color.white, duration, damageInterval, knockbackPower, knockbackDuration);
    }
}
