using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    // [SerializeField] private GameObject ui;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameManager.Instance.Timer.IsActive = true;
        }
    }

}