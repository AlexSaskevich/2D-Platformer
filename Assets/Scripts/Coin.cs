using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value = 1;

    private Collider2D _collider;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.AddCoin(_value);

            Destroy(gameObject);
        }
    }
}