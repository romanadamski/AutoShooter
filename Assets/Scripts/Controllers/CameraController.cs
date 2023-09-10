using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 5;

    private Camera _camera;
    private bool _isMovementActive;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.GameplayStarted += GameplayStarted;
        EventsManager.Instance.GameplayEnded += GameplayEnded;
    }

    private void GameplayEnded()
    {
        _isMovementActive = false;
    }

    private void GameplayStarted()
    {
        SetDefaultCameraValues();
        _isMovementActive = true;
    }

    private void Update()
    {
        if (!_isMovementActive) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButton(0))
        {
            _camera.transform.Translate(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0);
        }
        else if (Input.GetMouseButton(1))
        {
            _camera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * movementSpeed, Input.GetAxis("Mouse X") * movementSpeed, 0));
            var X = _camera.transform.rotation.eulerAngles.x;
            var Y = _camera.transform.rotation.eulerAngles.y;
            _camera.transform.rotation = Quaternion.Euler(X, Y, 0);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            _camera.transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * movementSpeed);
        }

        if (Input.GetKey(KeyCode.Y))
        {
            SetDefaultCameraValues();
        }
    }

    private void SetDefaultCameraValues()
    {
        var gameBounds = GameLauncher.Instance.GamePlane.GameBounds;
        var yPosition = Mathf.Clamp(0.3f * gameBounds.size.z, 3, 0.3f * gameBounds.size.z);
        _camera.transform.LookAt(new Vector3(gameBounds.center.x, 0, gameBounds.center.z));
        _camera.transform.position = new Vector3(gameBounds.center.x, yPosition, gameBounds.min.z);
    }
}
