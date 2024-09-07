using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BotAudio : BasicBotEventsHandler
{
    [SerializeField, Range(0, 1)] private float _volume;
    
    [SerializeField] private AudioClip _collectedClip;

    private AudioSource _source;

    private void Awake() =>
        _source = GetComponent<AudioSource>();

    protected override void OnCollect() =>
        Play(_collectedClip, _volume);

    private void Play(AudioClip clip, in float volume) =>
        _source.PlayOneShot(clip, volume);
}
