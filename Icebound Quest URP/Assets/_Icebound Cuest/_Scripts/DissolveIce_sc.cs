using UnityEngine;

public class DissolveIce_sc : MonoBehaviour
{
    [SerializeField] float noiseStrength = 0.25f;

    [SerializeField] bool canDissolve;
    [SerializeField] bool freezed;
    [SerializeField] bool melted;

    float height;
    [SerializeField] float heightBack;
    [SerializeField] float _meltHeight;
    [SerializeField] float _freezedHeight;   
    
    Material material;
    Collider _collider;
    public bool Melt { get => melted; set => melted = value; }
    public bool Freez { get => freezed; set => freezed = value; }

    void Start()
    {        
        material = GetComponent<Renderer>().material;
        _collider = GetComponent<Collider>();
        height = transform.position.y;

        /* Para saber los valores de MeltHeight y freezedHeight dar play con estos valores en 0 y con el bool de freezed marcado
         Luego mover el objeto y sacar los valores*/

        _meltHeight = Mathf.Abs(height - _meltHeight);
        _freezedHeight = Mathf.Abs(height - _freezedHeight);

        if (Freez) Freezed();
        if (Melt) Melted();
    }

    private void Update()
    {
        Dissolve();
    }
    public void Dissolve()
    {
        if (canDissolve)
        {
            if (Freez)
            {
                if (heightBack > height - _meltHeight)
                {
                    heightBack -= Time.deltaTime;
                }
                else
                {
                    CancelDissolved(true, false);
                    _collider.isTrigger = true;
                }
            }
            else if (Melt)
            {
                if (heightBack < height + _freezedHeight)
                {
                    heightBack += Time.deltaTime;
                }
                else
                {
                    CancelDissolved(false, true);
                    _collider.isTrigger = false;
                }
            }
        }
        SetHeight(heightBack);
    }
    public void CanDissolve()
    {
        canDissolve = true;
    }
    public void CancelDissolved(bool melt, bool freez)
    {
        canDissolve = false;
        Melt = melt;
        Freez = freez;
    }
    public void Melted()
    {
        heightBack = transform.position.y - _meltHeight;
        _collider.isTrigger = true;
    }
    public void Freezed()
    {
        heightBack = transform.position.y + _freezedHeight;
        _collider.isTrigger = false;
    }
    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}
