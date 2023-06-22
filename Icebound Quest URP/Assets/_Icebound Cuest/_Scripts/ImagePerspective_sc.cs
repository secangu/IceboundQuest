using UnityEngine;
public class ImagePerspective_sc : MonoBehaviour
{

    [SerializeField] Transform _ray1;
    [SerializeField] Renderer _rendererTarget1;
    [SerializeField] LayerMask _layerTarget1;
    [SerializeField] bool _checkImage1;

    [SerializeField] Transform _ray2;
    [SerializeField] Renderer _rendererTarget2;
    [SerializeField] LayerMask _layerTarget2;
    [SerializeField] bool _checkImage2;

    [SerializeField] Transform _ray3;
    [SerializeField] Renderer _rendererTarget3;
    [SerializeField] LayerMask _layerTarget3;
    [SerializeField] bool _checkImage3;

    void Start()
    {

    }

    void Update()
    {
        HitImage();
        ChangeColor();
    }
    public void HitImage()
    {
        _checkImage1 = false; _checkImage2 = false; _checkImage3 = false;

        //Ray Target1
        RaycastHit hitImage1;
        Ray rayImage1 = new Ray(_ray1.position, _ray1.forward);

        if (_checkImage1 = Physics.Raycast(rayImage1, out hitImage1, 30f, _layerTarget1))
        {
            if (hitImage1.collider.tag == "Image1")
            {
                _checkImage1 = true;
            }
        }
        //Ray Target2
        RaycastHit hitImage2;
        Ray rayImage2 = new Ray(_ray2.position, _ray2.forward);

        if (Physics.Raycast(rayImage2, out hitImage2, 30f, _layerTarget2))
        {
            if (hitImage2.collider.tag == "Image2")
            {
                _checkImage2 = true;
            }
        }

        //Ray Target3
        RaycastHit hitImage3;
        Ray rayImage3 = new Ray(_ray3.position, _ray3.forward);

        if (Physics.Raycast(rayImage3, out hitImage3, 30f, _layerTarget3))
        {
            if (hitImage3.collider.tag == "Image3")
            {
                _checkImage3 = true;
            }
        }
    }
    public void ChangeColor()
    {

        if (_checkImage1) _rendererTarget1.material.color = Color.red; else _rendererTarget1.material.color = Color.white;
        if (_checkImage2) _rendererTarget2.material.color = Color.red; else _rendererTarget2.material.color = Color.white;
        if (_checkImage3) _rendererTarget3.material.color = Color.red; else _rendererTarget3.material.color = Color.white;


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
