using System.Collections;
using UnityEngine;

public class ImagePerspective_sc : MonoBehaviour
{
    [SerializeField] PlayerButtonsController_sc cameraButtonScan;

    [System.Serializable]
    public struct ImageRay
    {
        public Transform ray;
        public Renderer rendererTarget;
        public LayerMask layerTarget;
        public bool checkImage;
    }

    [SerializeField] ImageRay[] imageRays;

    [SerializeField] bool arrived;
    [SerializeField] float timerScan;
    float time;
    [SerializeField] AudioSource alignImageSound;

    private CameraMovement_sc cameraMove;
    private InterfaceController_sc interfaceController;

    void Start()
    {
        interfaceController = FindObjectOfType<InterfaceController_sc>();
        cameraMove = GetComponent<CameraMovement_sc>();
    }

    void Update()
    {
        if (arrived)
        {
            HitImages();
        }
        ChangeColor();


        if (CheckAllImagesChecked())
        {
            time -= Time.deltaTime;
            cameraButtonScan.DisabledSprites();
            cameraButtonScan.SliderValue(time);
            if (time <= 0)
            {
                cameraButtonScan.ActiveSprites();
                Sound();
                cameraMove.enabled = false;
                StartCoroutine(ChangeScene());
            }
        }
        else
        {
            time = timerScan;
        }
    }

    public void Sound()
    {
        alignImageSound.Play();
    }

    public void HitImages()
    {
        for (int i = 0; i < imageRays.Length; i++)
        {
            RaycastHit hitImage;
            Ray rayImage = new Ray(imageRays[i].ray.position, imageRays[i].ray.forward);

            if (Physics.Raycast(rayImage, out hitImage, 30f, imageRays[i].layerTarget))
            {
                imageRays[i].checkImage = hitImage.collider.CompareTag("Image" + (i + 1));
            }
            else
            {
                imageRays[i].checkImage = false;
            }
        }
    }

    public void ChangeColor()
    {
        foreach (var imageRay in imageRays)
        {
            Color targetColor = Color.green; // Por defecto es verde 

            if (arrived)
            {
                targetColor = new Color(1.0f, 0.5f, 0.0f); // Si arrived es verdadero, el color es naranja 
                if (imageRay.checkImage)
                {
                    targetColor = Color.red; // si alinea la vista, el color es rojo (RGB: 1, 0, 0)
                }
            }else targetColor = Color.green;

            imageRay.rendererTarget.sharedMaterial.color = targetColor;
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2.9f);
        interfaceController.ChangeScene(0);
    }

    private bool CheckAllImagesChecked()
    {
        foreach (var imageRay in imageRays)
        {
            if (!imageRay.checkImage)
            {
                return false; // Al menos una  no fue verificada
            }
        }
        return true;// Todas las imágenes fueron verificadas
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AlignCamera"))
        {
            arrived = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AlignCamera"))
        {
            arrived = false;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var imageRay in imageRays)
        {
            Gizmos.color = imageRay.rendererTarget.sharedMaterial.color;
            Gizmos.DrawRay(imageRay.ray.position, imageRay.ray.TransformDirection(Vector3.forward) * 30f);
        }
    }

}
