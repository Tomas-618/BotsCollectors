using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BotsSpawnerAudio : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _volume;

    [SerializeField] private AudioClip _clickedClip;
    [SerializeField] private InteractableButton _button;

    private AudioSource _source;

    private void Awake() =>
        _source = GetComponent<AudioSource>();

    private void OnEnable() =>
        _button.Clicked += OnClicked;

    private void OnDisable() =>
        _button.Clicked -= OnClicked;

    private void OnClicked() =>
        Play(_clickedClip, _volume);

    private void Play(AudioClip clip, in float volume) =>
        _source.PlayOneShot(clip, volume);
}
