using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    private const string Flicker = "Signal";

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _maxDelta;

    private Animator _animator;
    private Coroutine _coroutine;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _audioSource.Play();
            _animator.SetBool(Flicker, true);
            _coroutine = StartCoroutine(AskVolume(_minVolume, _maxVolume, _maxVolume, 1));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _animator.SetBool(Flicker, false);
                _coroutine = StartCoroutine(AskVolume(_minVolume, _audioSource.volume, _minVolume, -1));

                if (_audioSource.volume == _minVolume)
                    _audioSource.Stop();
            }
        }
    }

    private IEnumerator AskVolume(float current, float target, float volume, int number)
    {
        while (_audioSource.volume != volume)
        {
            _audioSource.volume += (Mathf.MoveTowards(current, target, Time.deltaTime / _maxDelta)) * number;
            yield return null;
        }
    }
}
