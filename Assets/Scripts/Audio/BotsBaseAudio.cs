using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BotsBaseAudio : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _volume;

    [SerializeField] private InterfaceReference<IReadOnlyBotsBaseEvents, BotsBase> _events;
    [SerializeField] private AudioClip _onResourcesCountChangedClip;

    private AudioSource _source;

    private void Awake() =>
        _source = GetComponent<AudioSource>();

    private void OnEnable() =>
        _events.Value.ResourcesCountChanged += OnResourcesCountChanged;

    private void OnDisable() =>
        _events.Value.ResourcesCountChanged -= OnResourcesCountChanged;

    private void OnResourcesCountChanged(int count, bool isLess)
    {
        if (isLess)
            Play(_onResourcesCountChangedClip, _volume);
    }

    private void Play(AudioClip clip, in float volume) =>
        _source.PlayOneShot(clip, volume);
}
