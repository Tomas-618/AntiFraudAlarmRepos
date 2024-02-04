using UnityEngine;

public class FlyingCamera : MonoBehaviour, IMovable, IRotatable
{
    [SerializeField, Min(0)] private float _walkingSpeed;
    [SerializeField, Min(0)] private float _runningSpeed;
    [SerializeField, Min(0)] private float _rotationSpeedX;
    [SerializeField, Min(0)] private float _rotationSpeedY;

    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

    private Transform _transform;
    private float _speed;
    private float _pitch;
    private float _yaw;

    private void OnValidate()
    {
        if (_minAngle >= _maxAngle)
            _minAngle = _maxAngle - 1;
    }

    private void Reset()
    {
        _walkingSpeed = 10;
        _runningSpeed = 20;
        _minAngle = -90;
        _maxAngle = 90;
        _rotationSpeedX = 3;
        _rotationSpeedY = 3;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _speed = _walkingSpeed;
        _transform = transform;
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    public void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis(AxisNames.Horizontal), Input.GetAxis(AxisNames.Jump), Input.GetAxis(AxisNames.Vertical));

        _speed = Input.GetKey(KeyCode.LeftShift) ? _runningSpeed : _walkingSpeed;

        _transform.Translate(move * _speed * Time.deltaTime);
    }

    public void Rotate()
    {
        _yaw += _rotationSpeedX * Input.GetAxis(AxisNames.MouseX);
        _pitch -= _rotationSpeedY * Input.GetAxis(AxisNames.MouseY);

        _pitch = Mathf.Clamp(_pitch, _minAngle, _maxAngle);

        _transform.rotation = Quaternion.Euler(_pitch, _yaw, 0);
    }
}
