using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] SpriteRenderer bossSR;

    [SerializeField] Transform player;
    [SerializeField] public WeaponHandler WeaponPrefab;
    [SerializeField] private Transform weaponPivot;
    protected WeaponHandler weaponHandler;

    private bool flipX = false;
    private bool onSkill = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!onSkill)
            RandomSkill();
    }
    

    protected void RandomSkill()
    {
        //int idxSkill = Random.Range(0, 5);
        int idxSkill = 0;

        switch (idxSkill)
        {
            case 0:
                StartCoroutine(RushAttack());
                break;
            case 1:           
                StartCoroutine(SoundWaveAttack());
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    protected IEnumerator RushAttack()
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

    protected IEnumerator SoundWaveAttack()
    {
        onSkill = true;        
        onSkill = false;
        yield return new WaitForSeconds(3f);
    }
}
