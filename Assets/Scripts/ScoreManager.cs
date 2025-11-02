using UnityEngine;
using TMPro; // TextMeshProUGUI를 사용하기 위해 필요합니다.

/// <summary>
/// 스코어, 콤보, 판정 카운트를 관리하는 싱글톤 클래스입니다.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI References (Inspector 연결)")]
    // UI 텍스트 컴포넌트와 연결될 변수입니다.
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI perfectCountText;
    public TextMeshProUGUI missCountText;

    // 내부 스코어 상태 변수 (Private 변수는 _로 시작)
    private int _currentCombo = 0;
    private int _perfectCount = 0;
    private int _greatCount = 0;
    private int _goodCount = 0;
    private int _missCount = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // 씬 전환이 있다면 활성화
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// JudgementController에서 호출되어 판정 결과를 처리합니다.
    /// </summary>
    public void ProcessJudgement(JudgementType judgement)
    {
        switch (judgement)
        {
            case JudgementType.PERFECT:
                _perfectCount++;
                _currentCombo++;
                // TODO: SFX 재생 함수 호출
                break;
            case JudgementType.GREAT:
                _greatCount++;
                _currentCombo++;
                // TODO: SFX 재생 함수 호출
                break;
            case JudgementType.GOOD:
                _goodCount++;
                _currentCombo++;
                // TODO: SFX 재생 함수 호출
                break;
            case JudgementType.MISS:
                _missCount++;
                _currentCombo = 0; // MISS 시 콤보 초기화
                // TODO: 콤보 브레이크 피드백 호출 (UI/SFX)
                break;
        }

        UpdateUI();
    }

    /// <summary>
    /// 콤보와 카운트 텍스트를 업데이트합니다.
    /// </summary>
    private void UpdateUI()
    {
        // 콤보 텍스트 업데이트
        if (comboText != null)
        {
            // 콤보가 0 이상일 때만 표시
            comboText.text = (_currentCombo > 0) ? _currentCombo.ToString() + "\nCOMBO" : "";
        }
        
        // 판정 카운트 업데이트
        if (perfectCountText != null) perfectCountText.text = $"PERFECT: {_perfectCount}";
        if (missCountText != null) missCountText.text = $"MISS: {_missCount}";
        // 나머지 GREAT, GOOD 카운트도 필요시 여기에 추가
    }
}