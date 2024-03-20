using UnityEngine;

public class BeastSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource roarSound;
    [SerializeField] private AudioSource stompSound;

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
}