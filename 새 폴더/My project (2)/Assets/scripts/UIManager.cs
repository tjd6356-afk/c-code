using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ⭐ 버튼 컴포넌트 제어를 위해 추가
using TMPro;           // ⭐ 점수 텍스트 제어를 위해 추가

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject settingPanel;
    public GameObject savePanel;

    [Header("Lobby Level Settings")]
    // ⭐ 로비의 레벨 선택 버튼들을 순서대로 드래그해서 넣는 배열 (0번칸 = 1스테이지 버튼)
    public Button[] levelButtons;
    // ⭐ 각 레벨의 점수를 표시해 줄 텍스트 컴포넌트 배열 (0번칸 = 1스테이지 점수 텍스트)
    public TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        // 씬이 시작될 때 현재 로비 씬이라면 레벨 잠금/해제 및 점수판을 업데이트합니다.
        if (levelButtons != null && levelButtons.Length > 0)
        {
            UpdateLobbyUI();
        }
    }

    // ⭐ [새로 추가] 로비의 버튼 잠금 상태와 점수 텍스트를 최신화하는 함수
    public void UpdateLobbyUI()
    {
        // 저장된 데이터가 없다면 기본값인 '1레벨'만 열어둡니다.
        int maxUnlocked = PlayerPrefs.GetInt("MaxUnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNum = i + 1; // 레벨 번호는 1부터 시작

            // 1. 잠금 및 해제 처리
            if (levelNum <= maxUnlocked)
            {
                levelButtons[i].interactable = true; // 버튼 활성화 (클리어 가능)
            }
            else
            {
                levelButtons[i].interactable = false; // 버튼 비활성화 (잠금 상태)
            }

            // 2. 점수 표시 처리 (텍스트 배열이 등록되어 있는 경우에만)
            if (scoreTexts != null && i < scoreTexts.Length && scoreTexts[i] != null)
            {
                int score = PlayerPrefs.GetInt($"LevelScore_{levelNum}", 0);
                if (score > 0)
                {
                    scoreTexts[i].text = $"Score: {score}";
                }
                else
                {
                    scoreTexts[i].text = "Locked"; // 깨지 못한 곳은 Locked 혹은 0점 표시
                }
            }
        }
    }

    // ⭐ [새로 추가] 타이틀 씬의 [이어하기] 버튼에 연결할 함수
    public void ContinueGameButtonAction()
    {
        int maxUnlocked = PlayerPrefs.GetInt("MaxUnlockedLevel", 1);

        // 예를 들어 실제 카드 인게임 씬 이름 규칙이 "level_1", "level_2" 일 경우 아래와 같이 이동합니다.
        string targetSceneName = "level_" + maxUnlocked;

        // 프로젝트에 존재하는 씬인지 검증 후 이동하는 것이 안전합니다.
        SceneManager.LoadScene(targetSceneName);
    }

    // ⭐ [새로 추가] 세팅 창의 [저장하기] 버튼에 연결할 함수
    public void SaveCurrentProgressInSettings()
    {
        // 사실 PlayerPrefs는 실시간 저장되지만, 유저가 안심할 수 있도록 물리 저장을 확정 짓습니다.
        PlayerPrefs.Save();
        Debug.Log("현재 진행 상황이 안전하게 저장되었습니다!");
    }

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
            settingPanel.SetActive(true);
        }
    }

    public void CloseSettingWindow()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
    }

    public void GoToTitleScene()
    {
        SceneManager.LoadScene("title");
    }

    public void OpenSavePanel()
    {
        if (savePanel != null)
        {
            savePanel.SetActive(true);
        }
    }
    public void CloseSavePanel()
    {
        if (savePanel != null)
        {
            savePanel.SetActive(false);
        }
    }

}