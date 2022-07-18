using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    public bool sound = true;

    private void Awake()
    {
        makeSingleton();
        audioSource = GetComponent<AudioSource>();
    }
    private void makeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            GameObject obj = new GameObject();
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
    public void SoundOnOff()
    {
        sound = !sound;
        print("sound" + sound);
    }

    public void playSoundFX(AudioClip audioClip, float volume)
    {
        if (sound == true)
        {
            audioSource.PlayOneShot(audioClip, volume);
        }
    }

    // Update is called once per frame
}
