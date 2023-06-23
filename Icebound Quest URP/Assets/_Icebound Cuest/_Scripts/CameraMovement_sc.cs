using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement_sc : MonoBehaviour
{
    [Header("CameraSetting")]

    [SerializeField] float _speedX;
    [SerializeField] float _speedY;

    [SerializeField] float _maxRotationX;
    [SerializeField] float _maxRotationY;

    [SerializeField] float _maxPositionX;
    [SerializeField] float _maxPositionY;
    [SerializeField] float _minPositionY;

    Vector3 _initialPosition;
    Vector3 currentPosition;

    Vector3 _rotationX;
    Vector3 _rotationY;
    Vector3 _initiaRotation;
    void Start()
    {
        _initialPosition = transform.localPosition;
        currentPosition = _initialPosition;

        _initiaRotation = transform.localEulerAngles;
    }

    void Update()
    {
        TransformCamera();
    }
    public void TransformCamera()
    {
        /* Movimiento Camara*/

        float moveX = 0.03f * Input.GetAxis("Horizontal");
        float moveY = 0.03f * Input.GetAxis("Vertical");

        transform.localPosition = new Vector3(currentPosition.x, currentPosition.y, _initialPosition.z);
        transform.Translate(new Vector3(moveX, moveY, 0) * Time.deltaTime); //No borrar si se borra la camara se pega al limite establecido
        currentPosition.x += moveX;

        /* establece el limite de movimiento en X*/
        if (transform.localPosition.x >= _initialPosition.x + _maxPositionX)
            currentPosition.x = _initialPosition.x + _maxPositionX;
        else if (transform.localPosition.x <= _initialPosition.x - _maxPositionX)
            currentPosition.x = _initialPosition.x - _maxPositionX;

        currentPosition.y += moveY;
        /* establece el limite de movimiento en Y*/
        if (transform.localPosition.y >= _initialPosition.y + _maxPositionY)
            currentPosition.y = _initialPosition.y + _maxPositionY;
        else if (transform.localPosition.y <= _initialPosition.y + _minPositionY)
            currentPosition.y = _initialPosition.y + _minPositionY;

        /* Rotacion de la camara*/

        float X = _speedX * Input.GetAxis("Mouse X");
        float Y = _speedY * Input.GetAxis("Mouse Y");

        transform.localEulerAngles = new Vector3(_rotationX.x, _rotationY.y, 0);

        /* Establece un limite de rotacion*/
        _rotationX.x -= Y;
        if (_rotationX.x >= _initiaRotation.x + _maxRotationY) _rotationX.x = _initiaRotation.x + _maxRotationY;
        else if (_rotationX.x <= _initiaRotation.x - _maxRotationY) _rotationX.x = _initiaRotation.x - _maxRotationY;

        _rotationY.y += X;
        if (_rotationY.y >= _initiaRotation.y + _maxRotationX) _rotationY.y = _initiaRotation.y + _maxRotationX;
        else if (_rotationY.y <= _initiaRotation.y - _maxRotationX) _rotationY.y = _initiaRotation.y - _maxRotationX;
    }    
}
