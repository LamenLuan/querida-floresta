using UnityEngine;
public class MusicPlayer : MonoBehaviour
{
    private AudioSource musicSource;
    private static bool created = false;
    
    private void Awake()
    {
        if(!created) {
            DontDestroyOnLoad(transform.gameObject);
            musicSource = GetComponent<AudioSource>();
            musicSource.Play();
            created = true;
        }
        else Destroy(transform.gameObject);
    }

    public void PlayMusic()
    {
        if (musicSource.isPlaying) return;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void setMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
 }