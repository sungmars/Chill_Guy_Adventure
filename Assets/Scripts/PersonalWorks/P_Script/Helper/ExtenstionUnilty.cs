using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtenstionUnilty
{
    public static bool Contain(this LayerMask layerMask, int layer) // Layer에 존재하는지 확인
    {
        return (layerMask & (1 << layer)) != 0;
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component // 컴포넌트 있으면 Get 없으면 Add 후 반환
    {
        var component = gameObject.GetComponent<T>();
        if (component == null) gameObject.AddComponent<T>();
        return component;
    }

    public static T GetRandomItem<T>(this IList<T> list) // 리스트에서 랜덤한 아이템 하나 얻기
    {
        return list[Random.Range(0, list.Count)];
    }

    public static List<T> GetRandomItems<T>(this IList<T> list, int count) // 리스트에서 랜덤한 아이템 여러개 얻기(중복 없이)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogError("리스트가 비어 있습니다.");
            return new List<T>();
        }

        if (count > list.Count)
        {
            Debug.LogError($"리스트 크기({list.Count})보다 많은 개수({count})를 뽑을 수 없습니다.");
            return new List<T>();
        }

        List<T> temp = new List<T>(list); // 리스트 복사
        List<T> result = new List<T>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, temp.Count); // 랜덤 인덱스 선택
            result.Add(temp[index]); // 결과 리스트에 추가
            temp.RemoveAt(index); // 중복 방지를 위해 제거
        }

        return result;
    }


    public static void Shuffle<T>(this IList<T> list) // 리스트 섞기
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
