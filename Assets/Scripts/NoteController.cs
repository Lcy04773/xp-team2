using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float noteSpeed = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int diff = GameManager1.Instance.GetCurrentDifficulty();

        float speedMultiplier = 1f + diff * 0.1f;

        transform.Translate(Vector3.down * noteSpeed *  speedMultiplier * Time.deltaTime);
    }
}
