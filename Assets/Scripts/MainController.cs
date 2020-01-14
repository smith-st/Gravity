using System.Collections;
using System.Linq;
using Interfaces;
using UnityEngine;

public class MainController : MonoBehaviour, IBallCollision, IPlanetChanger
{
    public PlanetSettings[] planets;
    public Ball ball;
    public EdgeCollider2D border;
    public MainMenu menu;
    public GameObject exitText;

    private const float Force = 7f;
    private const float ExitDelay = 3f;

    private bool _isTouch;
    private Camera _camera;
    private Coroutine _exitCoroutine;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        _camera = Camera.main;
        GenerateBorder();
        ball.AddListener(this);
        SearchMenuButtons();
        exitText.SetActive(false);
    }

    public void ChangeCurrentPlanet(PlanetType type)
    {
        ChangePlanetType(type);
        ShowMenu(false);
    }

    public void BackPressed()
    {
        if (!menu.IsShowed)
        {
            ShowMenu();
        }
        else
        {
            if (_exitCoroutine == null)
            {
                _exitCoroutine = StartCoroutine(HideExitText());
            }
            else
            {
                StopCoroutine(_exitCoroutine);
                Application.Quit();
            }
        }
    }

    private IEnumerator HideExitText()
    {
        exitText.SetActive(true);
        yield return new WaitForSeconds(ExitDelay);
        exitText.SetActive(false);
        _exitCoroutine = null;
    }

    private void GenerateBorder()
    {
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

        border.points = points;
    }

    private void SearchMenuButtons()
    {
        var buttons = FindObjectsOfType<PlanetSelectButton>();
        foreach (var button in buttons)
        {
            button.AddListener(this);
        }
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackPressed();
        }
    }

    private void ChangePlanetType(PlanetType type)
    {
        var planetSettings = planets.First(p => p.type == type);
        _camera.backgroundColor = planetSettings.color;
        Physics2D.gravity = new Vector2(0f, -planetSettings.gravity);
    }

    public void BallContactWithPlatform(Platform platform)
    {
        (platform as IColorChange)?.ChangeColor();
    }

    private void ShowMenu(bool show = true)
    {
        if (show)
        {
            menu.Show();
            ball.Freeze();
        }
        else
        {
            menu.Hide();
            ball.Unfreeze();
        }
    }
}
