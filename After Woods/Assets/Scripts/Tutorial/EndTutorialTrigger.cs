using UnityEngine;

public class EndTutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject beast;
    [SerializeField] private Transform beastSpawn;
    [SerializeField] private GameObject invincibility;
    private bool activated = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !activated)
        {
            beast.transform.position = beastSpawn.transform.position;
            invincibility.SetActive(false);
            var lc = collider.gameObject.GetComponent<PlayerLogicController>();
            lc.FoodAmount = 0;
            GameManager.Instance.Timer.Reset();
            GameManager.Instance.fromTutorial = true;
            activated = true;
        }
    }

}