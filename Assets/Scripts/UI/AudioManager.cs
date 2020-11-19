using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //singleton that can be called from any script
    //Add to a game object with two audio sources. BMG audio source should be set to loop if desired

    public static AudioManager current;

    [Header("Parameters")]
    public float BGMVolume;
    public float SFXVolume;

    [Header("Clips")]
    public AudioClip[] AudioClips;
    private AudioClip tempClip;
    
    [Header("Background Music")]
    public AudioClip[] BGMs;
    private AudioClip tempBGM;

    [Header("Sources")]
    public AudioSource BGMSource;
    public AudioSource SFXSource;

    void Awake()
    {
        current = this;
        //I usually put this in a buffer scene that is never visited again and keep the AudioManager as a persistent object
        //Game Start >> buffer scene >> main menu
        //I know yopu know, I'm adding comments just in case :D
        DontDestroyOnLoad(this.gameObject);
        ChangeBGMVolume();
        ChangeSFXVolume();
    }
    public void ChangeSFXVolume()
    {
        SFXSource.volume = SFXVolume;
    }
    public void ChangeBGMVolume()
    {
        BGMSource.volume = BGMVolume;
    }
    public void PlaySoundEffect(string AudioClipName)
    {
        foreach (var Clip in AudioClips)
        {
            if (Clip.name == AudioClipName)
                tempClip = Clip;
        }
        SFXSource.PlayOneShot(tempClip);
    }

    public void PlayBGM(string BGMName)
    {
        BGMSource.Stop();
        foreach (var Clip in AudioClips)
        {
            if (Clip.name == BGMName)
                tempBGM = Clip;
        }
        BGMSource.PlayOneShot(tempBGM);
    }
}
