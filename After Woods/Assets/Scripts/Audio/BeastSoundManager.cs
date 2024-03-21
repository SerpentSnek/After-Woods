using UnityEngine;

public class BeastSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource roarSound;
    [SerializeField] private AudioSource stompSound;
    [SerializeField] private GameObject stageBGM;
    [SerializeField] private AudioSource beastSound;

    public void PlaySound(string soundName)
    {
        var sound = roarSound;
        switch (soundName)
        {
            case "roar":
                sound = roarSound;
                break;
            case "stomp":
                sound = stompSound;
                break;
            default:
                Debug.LogError("Sound name not found");
                break;
        }

        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }

    public void StopSound(string soundName)
    {
        var sound = roarSound;
        switch (soundName)
        {
            case "roar":
                sound = roarSound;
                break;
            case "stomp":
                sound = stompSound;
                break;
            default:
                Debug.LogError("Sound name not found");
                break;
        }

        sound.Stop();
    }

    public void StartChaseBGM()
    {
        var sound = beastSound;
        if (!sound.isPlaying)
        {
            sound.Play();
            GameObject.FindWithTag("StageBGM").SetActive(false);
        }
    }

    public void StopChaseBGM()
    {
        var sound = beastSound;
        sound.Stop();
    }
}