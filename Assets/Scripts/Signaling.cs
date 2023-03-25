using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _maxDelta;

    private Animator _animator;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    //Комменст

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            StopAllCoroutines();
            _audioSource.Play();
            _animator.SetBool("Signal", true);
            StartCoroutine(Volume(_minVolume, _maxVolume, _maxVolume, 1));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            StopAllCoroutines();
            _animator.SetBool("Signal", false);
            StartCoroutine(Volume(_minVolume, _audioSource.volume, _minVolume, -1));

            if (_audioSource.volume == _minVolume)
                _audioSource.Stop();
        }
    }

    private IEnumerator Volume(float minVolume, float maxVolume, float volume, int number)
    {
        while (_audioSource.volume != volume)
        {
            _audioSource.volume += (Mathf.MoveTowards(minVolume, maxVolume, Time.deltaTime / _maxDelta)) * number;
            yield return null;
        }
    }
}
