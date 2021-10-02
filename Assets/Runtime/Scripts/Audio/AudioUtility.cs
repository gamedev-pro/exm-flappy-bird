using UnityEngine;

public static class AudioUtility
{
    public static AudioService AudioService { private get; set; }
    //Tocar um SFX de Audio
    public static void PlayAudioCue(AudioClip clip)
    {
        AudioService.PlayAudioCue(clip);
    }

    public static void PlayMusic(AudioSource source, AudioClip clip)
    {
        AudioService.PlayMusic(clip);
    }
}
