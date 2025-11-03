using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Note Prefab")]
    public GameObject notePrefab; // 프리팹을 Inspector에서 연결

    [Header("Spawn Settings")]
    public float spawnInterval = 1.0f; // 몇 초마다 생성할지 (테스트용)
    private float _timer = 0f;

    void Update()
    {
        if (!GameManager.Instance) return;

        // 게임이 시작된 경우에만 동작하도록 할 수도 있음
        _timer += Time.deltaTime;

        // 일정 간격으로 노트를 생성 (테스트용)
        if (_timer >= spawnInterval)
        {
            _timer = 0f;

            // 0~5 랜덤 레인 선택
            int lane = Random.Range(0, GameManager.LANE_COUNT);
            SpawnNoteAtLane(lane);
        }
    }

    private void SpawnNoteAtLane(int laneIndex)
    {
        if (notePrefab == null) return;

        float spawnY = GameManager.JUDGEMENT_Y_POS + GameManager.Instance.noteSpeed * GameManager.Instance.approachTime;
        float spawnX = GameManager.Instance.LANE_POSITIONS[laneIndex];

        // 노트 생성
        GameObject note = Instantiate(notePrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);

        // GameManager의 activeNotes 리스트에 등록
        GameManager.Instance.AddNote(note);
    }
}
