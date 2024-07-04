using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class InteractableButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField, Range(0, 1)] private float _deltaColorValue;

    [SerializeField] private Color _onPointerEnterColor;
    [SerializeField] private Color _onPointerExitColor;
    [SerializeField] private Color _onPointerDownColor;

    private Coroutine _coroutine;
    private Image _image;

    public event Action Clicked;

    private void Reset()
    {
        _deltaColorValue = 0.05f;
        _onPointerEnterColor = Color.white;
        _onPointerExitColor = Color.white;
        _onPointerDownColor = Color.white;
    }

    private void Awake() =>
        _image = GetComponent<Image>();

    private void OnEnable() =>
        _image.color = _onPointerExitColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _image.DOColor(_onPointerEnterColor, _deltaColorValue);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _image.DOColor(_onPointerExitColor, _deltaColorValue);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _image.DOColor(_onPointerDownColor, _deltaColorValue);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
        OnPointerEnter(eventData);
    }
}
