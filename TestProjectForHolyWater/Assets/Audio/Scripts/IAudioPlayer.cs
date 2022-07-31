public interface IAudioPlayer
{
    public bool mute { get; }
    public void MuteAudio(bool state);
    public void PlayAudio(string name, object obj = null);
}
