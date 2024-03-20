using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource climbingSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource pickupSound;
    [SerializeField] private AudioSource walkingSound;

    public void PlaySound(string soundName)
    {
        var sound = climbingSound;
        switch (soundName)
        {
            case "climbing":
                sound = climbingSound;
                break;
            case "death":
                sound = deathSound;
                break;
            case "jumping":
                sound = jumpingSound;
                break;
            case "pickup":
                sound = pickupSound;
                break;
            case "walking":
                sound = walkingSound;
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
        var sound = climbingSound;
        switch (soundName)
        {
            case "climbing":
                sound = climbingSound;
                break;
            case "death":
                sound = deathSound;
                break;
            case "jumping":
                sound = jumpingSound;
                break;
            case "pickup":
                sound = pickupSound;
                break;
            case "walking":
                sound = walkingSound;
                break;
            default:
                Debug.LogError("Sound name not found");
                break;
        }

        sound.Stop();
    }
}