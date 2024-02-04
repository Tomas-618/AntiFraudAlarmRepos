using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _changingArrivePointDelay;

    [SerializeField] private Transform[] _arrivePoints;

    private Transform _transform;
    private int _currentArrivePointIndex;

    private void Reset()
    {
        _speed = 2;
        _changingArrivePointDelay = 5;
    }

    private void Awake() =>
        _transform = transform;

    private void Start() =>
        StartCoroutine(Move(_changingArrivePointDelay));

    public IEnumerator Move(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Transform currentArrivePoint = _arrivePoints[_currentArrivePointIndex];

            _transform.position = Vector3.MoveTowards(_transform.position, currentArrivePoint.position, _speed * Time.deltaTime);

            if (_transform.position == currentArrivePoint.position)
            {
                yield return wait;

                ChangeArrivePoint();
            }

            yield return null;
        }
    }

    private void ChangeArrivePoint()
    {
        _currentArrivePointIndex++;

        if (_currentArrivePointIndex == _arrivePoints.Length)
        {
            _currentArrivePointIndex = 0;
            enabled = false;
        }

        Vector3 arrivePointPosition = _arrivePoints[_currentArrivePointIndex].position;

        LookAtArrivePoint(arrivePointPosition);
    }

    private void LookAtArrivePoint(in Vector3 arrivePointPosition) =>
        _transform.LookAt(arrivePointPosition);
}
