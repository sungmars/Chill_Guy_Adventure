using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject optionPanel;//설정패널

    private void Start()
    {
        optionPanel.SetActive(false);
    }
    //새로하기버튼
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();//데이터를 삭제함
        PlayerPrefs.Save();
        SceneManager.LoadScene("TutorialScene");//튜툐리얼 씬 로드
    }
    //이어하기버튼
    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string sceneToLoad = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(sceneToLoad);//저장된 씬을 로드
        }
        else
        {
            Debug.Log("저장데이터 없음");
        }
    }
    //설정패널띄우기 버튼
    public void OpenSetting()
    {
        if (optionPanel != null)
            optionPanel.SetActive(true);//설정패널 활성화
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
