using UnityEngine;

// Timer class's sole purpose is to provide methods for the Beast class
public class Timer : MonoBehaviour, IReset
{
    [SerializeField] private float initialTime;
    private float currentTime;
    private bool isTimeUp = false;
    [SerializeField] private bool isActive = false;

    public float CurrentTime { get => currentTime; }
    public bool IsTimeUp { get => isTimeUp; }
    public bool IsActive { get => isActive; set => isActive = value; }

    public void AddTime(float timeRestored)
    {
        this.currentTime += timeRestored;
    }


    public void Reset()
    {
        isTimeUp = false;
        isActive = false;
        currentTime = initialTime;
    }

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (isActive)
        {
            if (this.currentTime > 0f)
            {
                this.currentTime -= Time.deltaTime;
            }
            else
            {
                isTimeUp = true;
            }
        }
    }
}
