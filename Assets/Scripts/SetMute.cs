using UnityEngine;
using UnityEngine.UI;
using YG;

public class SetMute : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private AudioSource[] playOnAwake;

    private void Awake()
    {
        ManageSound.SetMute(YandexGame.savesData.mute);
        foreach(var t in playOnAwake)
        {
            t.Play();
        }

        toggle.SetIsOnWithoutNotify(YandexGame.savesData.mute);
    }

    public void Mute(bool state)
    {
        ManageSound.SetMute(state);
        YandexGame.savesData.mute = state;
    }
}
