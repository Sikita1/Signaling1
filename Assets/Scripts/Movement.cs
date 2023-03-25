using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _moveInput;

    private Animator _animator;
    private bool facingRight = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _moveInput = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.D))
        {
            RunPlayer(true);
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RunPlayer(true);
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }
        else
        {
            RunPlayer(false);
        }

        if (!facingRight && _moveInput > 0)
            Flip();
        else if (facingRight && _moveInput < 0)
            Flip();
    }

    private void RunPlayer(bool isRunning)
    {
        _animator.SetBool("Run", isRunning);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
