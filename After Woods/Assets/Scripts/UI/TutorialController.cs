using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    public void StartTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void StopTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}