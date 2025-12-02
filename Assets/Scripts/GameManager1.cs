using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;

    private int currentDifficulty = 0;

    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int GetCurrentDifficulty()
    {
        return currentDifficulty; 
    }

    public void IncreaseDifficulty()
    {
        currentDifficulty++;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
