using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    private float _speed;

    void Start()
    {
        _speed = GameManager.Instance.noteSpeed;
    }

    void Update()
    {
        // Y축 아래로 이동
        transform.position += Vector3.down * _speed * Time.deltaTime;

        // 화면 아래로 지나가면 MISS 처리
        if (transform.position.y < GameManager.JUDGEMENT_Y_POS - 0.5f)
        {
            GameManager.Instance.RemoveNote(gameObject);
            ScoreManager.Instance.ProcessJudgement(JudgementType.MISS);
        }
    }
}
