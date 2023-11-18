using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    private const string Run = "Run";

    [SerializeField] private float _speed;
    [SerializeField] private float _moveInput;

    private Animator _animator;
    private bool _facingRight = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _moveInput = Input.GetAxis("Horizontal");

        if (_moveInput > 0)
        {
            RunPlayer(true);
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        else if (_moveInput < 0)
        {
            RunPlayer(true);
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }
        else
        {
            RunPlayer(false);
        }

        if (!_facingRight && _moveInput > 0)
            Flip();
        else if (_facingRight && _moveInput < 0)
            Flip();
    }

    private void RunPlayer(bool isRunning)
    {
        _animator.SetBool(Run, isRunning);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
