using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ImagePerspective_sc : MonoBehaviour
{
    [SerializeField] Transform _ray1;
    [SerializeField] Transform _ray2;
    [SerializeField] Transform _ray3;

    [Header("CameraSetting")]

    [SerializeField] float _speedX;
    [SerializeField] float _speedY;

    [SerializeField] float _maxRotationX;
    [SerializeField] float _maxRotationY;

    [SerializeField] float _maxPositionX;
    [SerializeField] float _maxPositionY;
    [SerializeField] float _minPositionY;

    float _initialPositionX;
    float _initialPositionY;
    float _initialPositionZ;

    float currentPositionX;
    float currentPositionY;

    Vector3 _rotationX;
    Vector3 _rotationY;
    void Start()
    {
        _initialPositionX = transform.position.x;
        _initialPositionY = transform.position.y;
        _initialPositionZ = transform.position.z;
        currentPositionX = _initialPositionX;
        currentPositionY = _initialPositionY;
    }

    void Update()
    {
        TransformCamera();
        HitImage();
    }
    public void HitImage()
    {
        RaycastHit hitImage1;
        Ray rayImage1 = new Ray(_ray1.position, _ray1.forward);

        RaycastHit hitImage2;
        Ray rayImage2 = new Ray(_ray2.position, _ray2.forward);

        RaycastHit hitImage3;
        Ray rayImage3 = new Ray(_ray3.position, _ray3.forward);

        if (Physics.Raycast(rayImage1, out hitImage1))
        {
            if (hitImage1.collider.tag == "Image1")
            {
                Debug.Log("Imagen1");
                Debug.DrawRay(rayImage1.origin, rayImage1.direction * 30f, Color.red);
            }
        }
        if (Physics.Raycast(rayImage2, out hitImage2))
        {
            if (hitImage2.collider.tag == "Image2")
            {
                Debug.Log("Imagen2");
                Debug.DrawRay(rayImage2.origin, rayImage2.direction * 30f, Color.red);
            }
        }
        if (Physics.Raycast(rayImage3, out hitImage3))
        {
            if (hitImage3.collider.tag == "Image3")
            {
                Debug.Log("Imagen3");
                Debug.DrawRay(rayImage3.origin, rayImage3.direction * 30f, Color.red);
            }
        }
    }
    public void TransformCamera()
    {
        /* Movimiento Camara*/

        float moveX = 0.03f * Input.GetAxis("Horizontal");
        float moveY = 0.03f * Input.GetAxis("Vertical");

        transform.position = new Vector3(currentPositionX, currentPositionY, _initialPositionZ);

        transform.Translate(new Vector3(moveX, moveY, 0) * Time.deltaTime); //No borrar si se borra la camara se pega al limite establecido

        currentPositionX += moveX;

        /* establece el limite de movimiento en X*/
        if (transform.position.x >= _initialPositionX + _maxPositionX) 
            currentPositionX = _initialPositionX + _maxPositionX;

        else if (transform.position.x <= _initialPositionX - _maxPositionX)        
            currentPositionX = _initialPositionX - _maxPositionX;        
        currentPositionY += moveY;
        
        /* establece el limite de movimiento en Y*/
        if (transform.position.y >= _initialPositionY + _maxPositionY)        
            currentPositionY = _initialPositionY + _maxPositionY;
        
        else if (transform.position.y <= _initialPositionY + _minPositionY)        
            currentPositionY = _initialPositionY + _minPositionY;

        /* Rotacion de la camara*/

        float X = _speedX * Input.GetAxis("Mouse X");
        float Y = _speedY * Input.GetAxis("Mouse Y");

        transform.localEulerAngles = new Vector3(_rotationX.x, _rotationY.y, 0);
        
        /* Establece un limite de rotacion*/
        _rotationX.x -= Y;
        if (_rotationX.x >= _maxRotationY) _rotationX.x = _maxRotationY; else if (_rotationX.x <= -_maxRotationY) _rotationX.x = -_maxRotationY;

        _rotationY.y += X;
        if (_rotationY.y >= _maxRotationX) _rotationY.y = _maxRotationX; else if (_rotationY.y <= -_maxRotationX) _rotationY.y = -_maxRotationX;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_ray1.position, _ray1.TransformDirection(Vector3.forward) * 30f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_ray2.position, _ray2.TransformDirection(Vector3.forward) * 30f);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_ray3.position, _ray3.TransformDirection(Vector3.forward) * 30f);
    }
}
