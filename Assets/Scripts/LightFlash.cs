using UnityEngine;

public class LightFlash : MonoBehaviour
{
    public float FlashDuration = 0.4f;
    public float InitialFlashIntensity = 5f;
    private Light explosionLight;
    // Start is called before the first frame update
    void Start()
    {
        explosionLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        FlashDuration -= Time.deltaTime;
        if (FlashDuration < 0.2)
        {
            explosionLight.intensity = FlashDuration * 5f * InitialFlashIntensity;
        }
        if (FlashDuration < 0)
        {
            explosionLight.enabled = false;
        }
    }
}
