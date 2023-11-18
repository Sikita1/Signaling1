using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    private const string Signal = "Signal";

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _seconds;
    [SerializeField] private House _house;

    private Animator _animator;
    private Coroutine _coroutine;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    private void OnEnable()
    {
        _house.StayChangend += OnStayChangend;
    }

    private void OnDisable()
    {
        _house.StayChangend -= OnStayChangend;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    private void OnStayChangend(bool stay)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _animator.SetBool(Signal, stay);

        if (stay)
            _coroutine = StartCoroutine(AskVolume(_maxVolume));
        else
            _coroutine = StartCoroutine(AskVolume(_minVolume));
    }

    private IEnumerator AskVolume(float targetVolue)
    {
        while (_audioSource.volume != targetVolue)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolue, Time.deltaTime / _seconds);
            yield return null;
        }
    }
}
