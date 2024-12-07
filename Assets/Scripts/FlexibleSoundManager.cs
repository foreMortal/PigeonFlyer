using UnityEngine;

public class FlexibleSoundManager : SoundManager
{
    [SerializeField] private AudioClip[] clips;

    public override void Play()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }

}
