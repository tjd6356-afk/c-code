using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject settingPanel;

    // [기존 코드] 타이틀 -> 로비 이동
    public void GameStartButtonAction()
    {
        SceneManager.LoadScene("loby");
    }

    // [기존 코드] 게임 종료
    public void GameQuitButtonAction()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ⭐ [새로 추가된 기능] 범용 레벨 선택 시스템
    // 버튼을 누를 때 인스펙터 창에서 가고 싶은 씬 이름을 적어주면 거기로 이동합니다.
    public void LoadLevelScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("이동할 레벨(씬)의 이름이 비어있습니다! 버튼의 인자값을 확인하세요.");
        }
    }

    public void OpenSettingWindow()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(true); // 세팅 창을 화면에 보이게 켭니다.
        }
    }

    // 2. 창 닫기(X) 버튼에 연결할 함수
    public void CloseSettingWindow()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false); // 세팅 창을 화면에서 안 보이게 끕니다.
        }
    }

}
