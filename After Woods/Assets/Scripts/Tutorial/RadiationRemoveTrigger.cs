using UnityEngine;

public class RadiationRemoveTrigger : MonoBehaviour
{
    // [SerializeField] private GameObject ui;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerLogicController>().CurrentRadiation = 0;
        }
    }

}