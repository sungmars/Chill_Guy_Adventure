using UnityEngine;

// MonoSingle ��� �� ������ Scene�� ��ġ�ϴ� ���� ����
public class MonoSingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            // GameObject�� ������ �ʾƼ� Awake�� ���� �ʾ��� �� �������ִ� ����
            if (instance == null)
            {
                GameObject go = new GameObject(typeof(T).Name);
                instance = go.AddComponent<T>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    // Scene�� ��ġ�ϸ� Awake�� ���� �Ǿ� �ϳ��� ����� �������� Destory()
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
    }
}
