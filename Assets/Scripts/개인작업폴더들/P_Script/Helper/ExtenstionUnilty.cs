using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtenstionUnilty
{
    public static bool Contain(this LayerMask layerMask, int layer) // Layer�� �����ϴ��� Ȯ��
    {
        return (layerMask & (1 << layer)) != 0;
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component // ������Ʈ ������ Get ������ Add �� ��ȯ
    {
        var component = gameObject.GetComponent<T>();
        if (component == null) gameObject.AddComponent<T>();
        return component;
    }

    public static T GetRandomItem<T>(this IList<T> list) // ����Ʈ���� ������ ������ �ϳ� ���
    {
        return list[Random.Range(0, list.Count)];
    }



    public static void Shuffle<T>(this IList<T> list) // ����Ʈ ����
    {
        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public static Vector2 SetX(this Vector2 vector, float x)
    {
        return new Vector2(x, vector.y);
    }
    public static bool HasComponent<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent<T>() != null;
    }


    public static void ChangeDirection(this Rigidbody rb, Vector3 direction)
    {
        rb.velocity = direction.normalized * rb.velocity.magnitude;
    }
}
