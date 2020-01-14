using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    private IBallCollision _listener;

    public void AddListener(IBallCollision listener)
    {
        _listener = listener;
    }
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _listener?.BallContactWithPlatform(other.gameObject.GetComponent<Platform>());
        }
    }
}
