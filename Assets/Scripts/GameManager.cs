using UnityEngine;
using System.Collections.Generic;
using System.Linq; // LINQ를 사용하여 활성 노트 관리를 쉽게 합니다.

/// <summary>
/// 게임의 중앙 허브 역할을 하며, 오디오 시간, 노트 속도, 활성 노트 리스트 등을 관리합니다.
/// 모든 시스템(Judgement, Spawner, Movement)이 이 인스턴스를 사용합니다.
/// </summary>
public class GameManager : MonoBehaviour
{
    // 1. 싱글톤 인스턴스: 다른 스크립트에서 GameManager.Instance로 쉽게 접근 가능
    public static GameManager Instance { get; private set; }

    [Header("Component References")]
    public AudioSource audioSource; 

    [Header("Active Notes Data")]
    // 활성 노트 리스트: 판정 로직이 현재 화면에 있는 노트를 찾을 때 사용합니다.
    public List<GameObject> activeNotes = new List<GameObject>(); 

    [Header("Game Parameters (Movement/Timing)")]
    // 노트가 내려오는 속도
    public float noteSpeed = 8.0f;         
    // 노트가 스폰되어 판정선에 도달하는 데 걸리는 시간 (2.0초)
    public float approachTime = 2.0f;    
    
    [Header("Lane Constants")]
    // 6개 레인의 X 좌표
    public const int LANE_COUNT = 6;
    // Lane 0의 X 좌표는 -2.5, Lane 5의 X 좌표는 2.5.
    public readonly float[] LANE_POSITIONS = { -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f };
    // 판정선 (Judgement Line)의 Y 좌표 기준. (Y=0)
    public const float JUDGEMENT_Y_POS = 0f;

    private bool _gameStarted = false;

    private void Awake()
    {
        // 싱글톤 초기화 로직
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
    
    void Start()
    {
        // BGM 재생 및 게임 시작
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            _gameStarted = true;
        }
    }
    
    /// <summary>
    /// --- 판정 시스템 (JudgementController) 지원 함수 ---
    /// </summary>
    
    // 고정밀 오디오 시간을 밀리초(ms) 단위로 반환. (판정 로직의 핵심)
    public float GetAudioTimeMs()
    {
        if (audioSource == null || audioSource.clip == null) return 0f;

        // AudioSource.timeSamples를 사용해 프레임에 독립적인 고정밀 시간을 계산.
        // 공식: (샘플 위치 / 클립 주파수) * 1000
        return (float)(audioSource.timeSamples / (double)audioSource.clip.frequency * 1000.0);
    }
    
    /// <summary>
    /// 활성 노트 리스트 관리 함수 (Spawner와 JudgementController가 사용)
    /// </summary>
    
    // 노트가 생성될 때 리스트에 추가합니다. (Spawner가 호출)
    public void AddNote(GameObject note)
    {
        if (note != null)
        {
            activeNotes.Add(note);
        }
    }

    // 노트가 제거되거나 MISS 처리될 때 리스트에서 제거하고 오브젝트를 파괴합니다.
    public void RemoveNote(GameObject note)
    {
        if (note != null)
        {
            activeNotes.Remove(note);
            Destroy(note);
        }
    }
    
    // TODO: 여기에 게임 일시 정지, 게임 오버 처리 등의 공용 상태 관리 함수가 추가될 예정입니다.
}
