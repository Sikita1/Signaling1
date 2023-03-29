using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour
{
    public UnityAction<bool> StayChangend;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            StayChangend?.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            StayChangend?.Invoke(false);
    }
}
