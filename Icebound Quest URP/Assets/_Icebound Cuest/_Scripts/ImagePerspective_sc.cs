using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ImagePerspective_sc : MonoBehaviour
{
    
    [SerializeField] Transform _ray1;
    [SerializeField] LayerMask image1;
    [SerializeField] Transform _ray2;
    [SerializeField] LayerMask image2;
    [SerializeField] Transform _ray3;
    [SerializeField] LayerMask image3;
    
    void Start()
    {

    }

    void Update()
    {
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

        if (Physics.Raycast(rayImage1, out hitImage1,30f,image1))
        {
            if (hitImage1.collider.tag == "Image1")
            {
                hitImage1.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Debug.Log("Imagen1");
            }
        }else if(Physics.Raycast(rayImage1, out hitImage1, 30f, image1)) hitImage1.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;

        if (Physics.Raycast(rayImage2, out hitImage2,30f,image2))
        {          
            if (hitImage2.collider.tag == "Image2")
            {
                hitImage2.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Debug.Log("Imagen2");
            }
            else hitImage2.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        if (Physics.Raycast(rayImage3, out hitImage3, 30f, image3))
        {
            if (hitImage3.collider.tag == "Image3")
            {
                hitImage3.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Debug.Log("Imagen3");
            }
            else hitImage3.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
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
