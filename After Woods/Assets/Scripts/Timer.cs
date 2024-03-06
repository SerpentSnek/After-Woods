using UnityEngine;

// Timer class's sole purpose is to provide methods for the Beast class
public class Timer : MonoBehaviour, Reset
{
    [SerializeField] private float totalTime;
    private bool isTimesUp;
    public float TotalTime
    {
        get => totalTime;
    }
    public bool IsTimesUp
    {
        get => isTimesUp;
        set => isTimesUp = value;
    }
    public Timer(float totalTime)
    {
        this.totalTime = totalTime;
        IsTimesUp = false;
    }
    public void AddTime(float timeRestored)
    {
        this.totalTime += timeRestored;
    }
    public void ResetStats(GameObject obj = null)
    {
        this.totalTime = 20f;
    }
    public void CountdownTimer()
    {
        if (this.totalTime > 0f)
        {
            this.totalTime -= Time.deltaTime;
        }
        else
        {
            IsTimesUp = true;
        }
    }
    void Update()
    {
        this.CountdownTimer();
    }
}
