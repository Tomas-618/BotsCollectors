using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BaseWalletAudio : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _volume;

    [SerializeField] private InterfaceReference<IReadOnlyBaseWalletEvents, ResourcesBaseWallet> _events;
    [SerializeField] private AudioClip _onAddClip;
    [SerializeField] private AudioClip _onRemoveClip;

    private AudioSource _source;

    private void Awake() =>
        _source = GetComponent<AudioSource>();

    private void OnEnable()
    {
        _events.Value.Added += OnAdded;
        _events.Value.Removed += OnRemove;
    }

    private void OnDisable()
    {
        _events.Value.Added -= OnAdded;
        _events.Value.Removed -= OnRemove;
    }

    private void OnAdded() =>
        Play(_onAddClip, _volume);

    private void OnRemove() =>
        Play(_onRemoveClip, _volume);

    private void Play(AudioClip clip, in float volume) =>
        _source.PlayOneShot(clip, volume);
}
