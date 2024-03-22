using UnityEngine;

public class UITrigger : MonoBehaviour
{
    [SerializeField] private GameObject ui;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            ui.SetActive(true);
            collider.gameObject.GetComponent<PlayerLogicController>().baitEnabled = false;
        }
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerLogicController>().baitEnabled = true;
        }
    }
}