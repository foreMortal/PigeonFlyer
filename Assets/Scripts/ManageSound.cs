using System;

public static class ManageSound 
{
    private static bool mute;

    public static bool Mute { get { return mute; } }
    public static event Action<bool> onMuteChange;

    public static void SetMute(bool state)
    {
        mute = state;
        onMuteChange?.Invoke(state);
    }
}
