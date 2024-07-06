using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BotAudio : BasicBotEventsHandler
{
    [SerializeField, Range(0, 1)] private float _volume;
    
    [SerializeField] private AudioClip _collectedClip;
    [SerializeField] private AudioClip _putClip;

    private AudioSource _source;

    private void Awake() =>
        _source = GetComponent<AudioSource>();

    protected override void OnCollect() =>
        Play(_collectedClip, _volume);

    protected override void OnPut() =>
        Play(_putClip, _volume);

    private void Play(AudioClip clip, in float volume) =>
        _source.PlayOneShot(clip, volume);
}
