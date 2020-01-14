using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    private IBallCollision _listener;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Freeze();
    }
    public void AddListener(IBallCollision listener)
    {
        _listener = listener;
    }

    public void Freeze()
    {
        Rigidbody.simulated = false;
    }

    public void Unfreeze()
    {
        Rigidbody.simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _listener?.BallContactWithPlatform(other.gameObject.GetComponent<Platform>());
        }
    }
}