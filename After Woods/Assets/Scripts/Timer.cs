using UnityEngine;

// Timer class's sole purpose is to provide methods for the Beast class
public class Timer : MonoBehaviour
{
    [SerializeField] private float totalTime;

    public float TotalTime
    {
        get => totalTime;
    }

    public Timer(float totalTime)
    {
        this.totalTime = totalTime;
    }
    public void AddTime()
    {
        this.totalTime += 1f;
    }
    public void ResetTimer()
    {
        this.totalTime = 20f;
    }
    public void CountdownTimer()
    {
        if (this.totalTime > 0f)
        {
            this.totalTime -= Time.deltaTime;
        }
    }
}
