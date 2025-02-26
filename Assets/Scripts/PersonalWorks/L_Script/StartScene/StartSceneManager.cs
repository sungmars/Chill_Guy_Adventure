using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject optionPanel;//설정패널
    public AudioClip bgm;
    private void Start()
    {
        optionPanel.SetActive(false);
        AudioManager.Instance.PlayBGM(bgm);
    }
    //새로하기버튼
    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");//게임 씬 로드
    }
    //이어하기버튼
    public void Custom()
    {
        SceneManager.LoadScene("CustomScene");//커스텀 씬 로드
    }
    //설정패널띄우기 버튼
    public void OpenSetting()
    {
        if (optionPanel != null)
            optionPanel.SetActive(true);//설정패널 활성화
    }
    public void CloseSetting()
    {
        if (optionPanel != null)
            optionPanel.SetActive(false);//설정패널 활성화
    }
    //게임종료버튼
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//에디터에서 실행중이면 종료
#endif
    }
}
