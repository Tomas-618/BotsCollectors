using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BotAudio : BasicBotEventsHandler
{
    [Range(0, 1), SerializeField] private float _volume;
    
    [SerializeField] private AudioClip _collectedClip;
    [SerializeField] private AudioClip _putClip;

    private AudioSource _source;

    private void Awake() =>
        _source = GetComponent<AudioSource>();

    protected override void OnCollect(int count) =>
        Play(_collectedClip, _volume);

    protected override void OnPut(int count) =>
        Play(_putClip, _volume);

    private void Play(AudioClip clip, in float volume) =>
        _source.PlayOneShot(clip, volume);
}
