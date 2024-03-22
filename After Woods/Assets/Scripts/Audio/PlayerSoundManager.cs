using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource baitingSound;
    [SerializeField] private AudioSource climbingSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource eatingSound;
    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource pickupSound;
    [SerializeField] private AudioSource walkingSound;
    [SerializeField] private AudioSource takeDamageSound;

    public void PlaySound(string soundName)
    {
        var sound = baitingSound;
        switch (soundName)
        {
            case "baiting":
                sound = baitingSound;
                break;
            case "climbing":
                sound = climbingSound;
                break;
            case "death":
                sound = deathSound;
                break;
            case "eating":
                sound = eatingSound;
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
            case "damage":
                sound = takeDamageSound;
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
        var sound = baitingSound;
        switch (soundName)
        {
            case "baiting":
                sound = baitingSound;
                break;
            case "climbing":
                sound = climbingSound;
                break;
            case "death":
                sound = deathSound;
                break;
            case "eating":
                sound = eatingSound;
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
            case "damage":
                sound = takeDamageSound;
                break;
            default:
                Debug.LogError("Sound name not found");
                break;
        }

        sound.Stop();
    }

    public void StopAllSounds()
    {
        baitingSound.Stop();
        climbingSound.Stop();
        deathSound.Stop();
        eatingSound.Stop();
        jumpingSound.Stop();
        pickupSound.Stop();
        walkingSound.Stop();
        takeDamageSound.Stop();
    }
}