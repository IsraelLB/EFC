using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips; // Array de archivos de audio
    private AudioSource audioSource;
    private float timer = 0f;
    private float interval = 15f; // Intervalo de 15 segundos

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned to the RandomAudioPlayer script.");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            PlayRandomAudio();
            timer = 0f;
        }
    }

    void PlayRandomAudio()
    {
        if (audioClips.Length > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();
        }
    }
}
