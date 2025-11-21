using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource sound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip audioClip, Transform spawnPos, float volume)
    {
        AudioSource audioSource = Instantiate(sound, spawnPos.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float soundLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, soundLength);
    }
}
