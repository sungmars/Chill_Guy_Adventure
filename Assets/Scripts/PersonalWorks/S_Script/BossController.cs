using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] SpriteRenderer bossSR;

    [SerializeField] Transform player;
    [SerializeField] public WeaponHandler WeaponPrefab;
    [SerializeField] private Transform weaponPivot;
    public GameObject projectilePrefab;
    [SerializeField] private GameObject chillAttackPrefab;
    [SerializeField] private GameObject chillAttackBottomPrefab;
    protected WeaponHandler weaponHandler;

    Animator animator;
    private static readonly int isAttack = Animator.StringToHash("IsAttack");

    private bool flipX = false;
    private bool onSkill = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();

        animator = transform.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        InvokeRepeating("SkillRepeat", 2f, 5f);
    }

    private void Update()
    {
        if (!onSkill)
        {
            if (player.transform.position.x < transform.position.x)
            {
                flipX = false;
                bossSR.flipX = flipX;
            }
            else
            {
                flipX = true;
                bossSR.flipX = flipX;
            }
        }
    }

    private void SkillRepeat()
    {
        animator.SetTrigger(isAttack);
        Invoke("RandomSkill", 1.5f);
    }


    private void RandomSkill()
    {
        int idxSkill = Random.Range(0, 5);
        switch (1)
        {
            case 0:
                StartCoroutine(RushAttack());
                break;
            case 1:
                SoundWaveAttack();
                break;
            case 2:
                ChillAttack();
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    private IEnumerator RushAttack()
    {
        onSkill = true;
        Vector2 targetPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 curPos = transform.position;
        Vector2 backDir = (curPos - targetPos).normalized;
        Vector2 backPos = curPos + backDir * 1f;
        Vector2 movePos;
        while (Vector2.Distance(backPos, transform.position) > 0.1f)
        {
            movePos = Vector2.Lerp(transform.position, backPos, Time.deltaTime * 1.5f);
            transform.position = movePos;
            yield return new WaitForSeconds(0f);
        }

        while (Vector2.Distance(targetPos, transform.position) > 0.1f)
        {
            movePos = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * 2f);
            transform.position = movePos;
            yield return new WaitForSeconds(0f);
        }
        onSkill = false;
        yield return new WaitForSeconds(3f);
    }

    private void SoundWaveAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, weaponPivot.position, weaponPivot.rotation);
        // 플레이어 방향 계산
        Vector2 direction = (player.position - weaponPivot.position).normalized;
        // 발사체의 회전을 플레이어 방향에 맞게 조정
        projectile.transform.up = direction;
        // 속도 부여
        Rigidbody2D arrowRb = projectile.GetComponent<Rigidbody2D>();
        if (arrowRb != null)
        {
            arrowRb.velocity = direction * 3f;
        }
        StartCoroutine(SoundWaveSetSize(projectile));
    }

    IEnumerator SoundWaveSetSize(GameObject projectile)
    {
        BoxCollider2D boxCollider2D = projectile.GetComponent<BoxCollider2D>();
        float x = 10;
        Vector2 objSize = projectile.transform.localScale;
        Vector2 boxSize = boxCollider2D.size;
        while (projectile.transform.localScale.x < x)
        {
            objSize += objSize * Time.deltaTime;
            projectile.transform.localScale = new Vector2(objSize.x, objSize.y);

            Vector2 ratio = projectile.transform.localScale / objSize;
            boxCollider2D.size = boxSize * ratio;
            yield return null;
        }
        yield return null;
    }

    private void ChillAttack()
    {
        GameObject chillAttackParent;
        GameObject chillAttack;
        GameObject chillAttackBottom;

        for (int i = 0; i < 10; i++)
        {
            chillAttackParent = new GameObject();
            chillAttack = Instantiate(chillAttackPrefab, chillAttackParent.transform);
            chillAttackBottom = Instantiate(chillAttackBottomPrefab, chillAttackParent.transform);
            chillAttackParent.AddComponent<ChillAttackController>();
            chillAttackParent.name = $"ChillAttackParent {i}";
            chillAttack.name = $"ChillAttack {i}";
            chillAttackBottom.name = $"ChillAttackBottom {i}";
            if (i == 0)
            {
                chillAttack.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3f, 0f);
                chillAttackBottom.transform.position = new Vector3(player.transform.position.x + 0.06f, player.transform.position.y - 0.6f, 0f);
            }
            else
            {
                float x = Random.Range(-8f, 9f);
                float y = Random.Range(-4f, 5f);
                chillAttack.transform.position = new Vector3(x, y + 3f, 0f);
                chillAttackBottom.transform.position = new Vector3(x + 0.06f, y - 0.6f, 0f);
            }
        }
    }
}
