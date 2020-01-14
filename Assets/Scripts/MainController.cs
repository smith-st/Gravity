using System.Linq;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public PlanetSettings[] planets;
    public Ball ball;
    public EdgeCollider2D _border;

    private const float Force = 7f;

    private bool _isTouch;
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
        var topLeft = (Vector2)_camera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f));
        var topRight = (Vector2)_camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        var bottomRight = (Vector2)_camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));
        var bottomLeft = (Vector2)_camera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        var points = new[]
        {
            topLeft,
            topRight,
            bottomRight,
            bottomLeft,
            topLeft
        };

        _border.points = points;
        ChangePlanetType(PlanetType.Moon);
    }

    private void FixedUpdate()
    {
        if (!_isTouch) return;
        var angle = MathUtil.AngleBeetwenTwoPoints(ball.transform.position, _camera.ScreenToWorldPoint(Input.mousePosition));
        var direction = MathUtil.rotateVector(new Vector2(Force, 0f), angle);
        ball.Rigidbody.AddForce(direction,ForceMode2D.Force);
    }

    private void Update()
    {
        _isTouch = Input.GetMouseButton(0);
    }

    private void ChangePlanetType(PlanetType type)
    {
        var planetSettings = planets.First(p => p.type == type);
        _camera.backgroundColor = planetSettings.color;
        Physics2D.gravity = new Vector2(0f, -planetSettings.gravity);
    }
}
