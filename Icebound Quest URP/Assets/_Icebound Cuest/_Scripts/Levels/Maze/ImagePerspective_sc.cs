using System.Collections;
using UnityEngine;

public class ImagePerspective_sc : MonoBehaviour
{

    [System.Serializable]
    public struct ImageRay
    {
        public Transform ray;
        public Renderer rendererTarget;
        public LayerMask layerTarget;
        public bool checkImage;
    }

    [SerializeField] ImageRay[] imageRays;
    [SerializeField] PlayerButtonsController_sc cameraButtonScan;
    [SerializeField] float timerScan; // tiempo que debe escanear la imagen
    [SerializeField] AudioSource alignImageSound;
    [SerializeField] GameObject sucess;
    [SerializeField] int num;

    bool arrived; //Esta en la posicion correcta
    float time;

    CameraMovement_sc cameraMove;
    bool shouldPlaySound = false;

    void Start()
    {
        cameraMove = GetComponent<CameraMovement_sc>();
        time = timerScan;
    }

    void Update()
    {
        ChangeColor();

        if (arrived)
        {
            HitImages();
            if (CheckAllImagesChecked())
            {
                time -= Time.deltaTime;
                cameraButtonScan.DisabledSprites();
                cameraButtonScan.SliderValue(time);

                if (time <= 0 && !shouldPlaySound)
                {
                    shouldPlaySound = true;
                    alignImageSound.Play();
                    cameraButtonScan.ActiveSprites();
                    cameraMove.enabled = false;
                    StartCoroutine(ChangeScene());
                }
            }
            else
            {
                time = timerScan;
            }
        }
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
            Color targetEmissionColor = new Color(0.75f, 0.094f, 0.0f); // Por defecto es rojo  

            if (arrived)
            {
                targetEmissionColor = new Color(0.75f, 0.61f, 0.0f); // Si arrived es verdadero, el color de emisión es amarillo

                if (imageRay.checkImage)
                {
                    targetEmissionColor = new Color(0.0f, 0.75f, 0.004f); // si alinea la vista, el color de emisión es verde
                }
            }

            Material material = imageRay.rendererTarget.sharedMaterial;
            material.EnableKeyword("_EMISSION");

            if (material.HasProperty("_EmissionColor"))
            {
                targetEmissionColor *= 2.0f;
                material.SetColor("_EmissionColor", targetEmissionColor);
            }
        }
    }
    IEnumerator ChangeScene()
    {
        if (num > PlayerPrefs.GetInt("idLevel")) PlayerPrefs.SetInt("idLevel", num);
        yield return new WaitForSeconds(0.2f);
        sucess.SetActive(true);
        Time.timeScale = 0;
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
