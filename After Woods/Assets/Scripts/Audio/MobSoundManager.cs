using UnityEngine;

public class MobSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource aggroSound;

    public void PlayAggroSound()
    {
        if (!aggroSound.isPlaying)
        {
            aggroSound.Play();
        }
    }

    public void StopAggroSound()
    {
        aggroSound.Stop();
    }
}