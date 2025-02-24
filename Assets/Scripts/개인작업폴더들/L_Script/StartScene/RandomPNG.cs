using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomPNG : MonoBehaviour
{
    public Sprite[] sprites;
    public Image displayImage;
    public Button reloadButton;
    void Start()
    {
        if (sprites.Length > 0 && displayImage != null)
        {
            int randomIndex = Random.Range(0, sprites.Length);//랜덤 인덱스 선택하기
            displayImage.sprite = sprites[randomIndex];//선택한 이미지 생성
        }
        else
            Debug.LogWarning("할당된 이미지 없음");
        
    }
    private void ReloadScene() //재로딩 버튼에 사용할 예정
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ButtonPress()
    {
        ReloadScene();
    }

}
