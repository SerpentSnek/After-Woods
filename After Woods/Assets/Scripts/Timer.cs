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
        totalTime += 1f;
    }
    public void ResetTimer()
    {
        totalTime = 20f;
    }
    public void CountdownTimer()
    {
        if (totalTime > 0f)
        {
            totalTime -= Time.deltaTime;
        }
    }
}
