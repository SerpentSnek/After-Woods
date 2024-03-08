using UnityEngine;
public class BeastBehavior : MonoBehaviour
{
    // Use GameManager.Instance.TimerObject.IsTimesUp to trigger Beast's action
    private void Start()
    {
        Debug.Log(GameManager.Instance.Timer.IsTimesUp);
    }
    public BeastBehavior()
    {
    }
}
