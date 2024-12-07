using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private bool Pause;
    protected AudioSource source;

    private void OnEnable()
    {
        source.mute = ManageSound.Mute;
        ManageSound.onMuteChange += Mute;
    }
    private void OnDisable()
    {
        source.mute = ManageSound.Mute;
        ManageSound.onMuteChange -= Mute;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public virtual void Play()
    {
        source.Play();
    }

    private void Mute(bool state)
    {
        if (Pause)
        {
            if (state)
                source.Stop();
            else
                source.Play();
        }
        else
        {
            source.Stop();
        }
        

        source.mute = state;
    }
}
