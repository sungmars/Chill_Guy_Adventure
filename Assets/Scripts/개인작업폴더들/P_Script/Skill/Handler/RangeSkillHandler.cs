using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RangeSkillHandler : SkillHandler
{
    [Header("Skill Sprite")]
    public GameObject rangeObject; // �߻�ü

    [Header("Skill Stats")]
    public float damage = 1f;
    public float speed = 10f; // �߻�ü ���ǵ�
    public int rangeCount = 1; // �߻�ü ����
    public float rangeAngel = 1f; // �߻�ü ����
    public float angelSpread = 1f; // �߻�ü ����


    public override void AwakeningAction()
    {
        // ��Ÿ���� ����� Ȯ��
        if (!CheckCoolDown()) return;

        // �߻�
        CreateRangeObject();
    }

    public override void NormalAction()
    {
        // ��Ÿ���� ����� Ȯ��
        if (!CheckCoolDown()) return;

        // �߻�
        CreateRangeObject();
    }


    public void CreateRangeObject()
    {
        float angleSpace = rangeAngel;
        int rangeCount = this.rangeCount;

        float minAngle = -(rangeCount / 2f) * angleSpace + 0.5f * rangeAngel;

        for (int i = 0; i < rangeCount; i++)
        {
            float angle = minAngle + angleSpace * i;
            float randomSpread = Random.Range(-angelSpread, angelSpread);
            angle += randomSpread;

            GameObject go = Instantiate(rangeObject, SkillManager.Instance.player.position, Quaternion.identity);
            RangeSkillObject rangeSkillobject = go.GetComponent<RangeSkillObject>();

            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            var direction = RotateVector2(worldPos - (Vector2)SkillManager.Instance.player.position, angle);
            direction = direction.normalized;
            rangeSkillobject.Init(direction, Color.white, duration, speed);
        }
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
