using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroBossController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioClip lookAround;
    [SerializeField] private AudioClip bossEye;

    protected readonly int findbool = Animator.StringToHash("FindBool");

    public IEnumerator BossIn()
    {
        this.gameObject.SetActive(true);
        transform.position = new Vector3(0, 10f, 0);
        float x = transform.position.x;
        float y = transform.position.y;
        float z = 0;
        float speed = 0.1f;

        while ((int)transform.position.y != 0)
        {
            y = transform.position.y - speed;
            transform.position = new Vector3(x, y, z);
            speed += 0.005f;
            yield return new WaitForSeconds(0f);
        }
        AudioManager.Instance.PlayBossSound(dropClip);        

        animator.SetBool(findbool, true);
        yield return new WaitForSeconds(0.2f);
        AudioManager.Instance.PlayBossSound(lookAround);
        yield return new WaitForSeconds(0.4f);
        AudioManager.Instance.PlayBossSound(lookAround);
        yield return new WaitForSeconds(0.4f);
        AudioManager.Instance.PlayBossSound(lookAround);
        yield return new WaitForSeconds(0.4f);
        AudioManager.Instance.PlayBossSound(lookAround);
        yield return new WaitForSeconds(0.4f);
        AudioManager.Instance.PlayBossSound(lookAround);
        yield return new WaitForSeconds(0.4f);
        AudioManager.Instance.PlayBossSound(lookAround);
        yield return new WaitForSeconds(0.7f);
        AudioManager.Instance.PlayBossSound(bossEye);
        yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene("BossScene");
    }
}
