using UnityEngine;

public class LockedLevels_sc : MonoBehaviour
{
    [SerializeField] int scene;
    [SerializeField] int num;
    [SerializeField] float rotation;

    [SerializeField] GameObject platform;
    [SerializeField] GameObject colliderNextLevel;

    public float Rotation { get => rotation; set => rotation = value; }
    public int Scene { get => scene; set => scene = value; }

    void Start()
    {
        if (PlayerPrefs.HasKey("idLevel") == false) PlayerPrefs.SetInt("idLevel", 1);

        if (PlayerPrefs.GetInt("idLevel") >= num)
        {
            platform.SetActive(false);
            colliderNextLevel.SetActive(true);
        }
        else
        {
            platform.SetActive(true);
            colliderNextLevel.SetActive(false);
        }
    }
}
