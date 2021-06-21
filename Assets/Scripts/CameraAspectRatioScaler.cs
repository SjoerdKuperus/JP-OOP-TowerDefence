using UnityEngine;

/// <summary>
/// Responsive Camera Scaler
/// </summary>
public class CameraAspectRatioScaler : MonoBehaviour
{
    private float horizontalResolution = 1920;
    private float aspectDesign = 54.54f; // is 1080 / 19.8 (The initial size with 1080 resolution height)
    private Camera thisCamera;

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    /// <summary>
    /// Update per Frame
    /// </summary>
    void Update()
    {
        float currentAspect = (float)Screen.width / (float)Screen.height;
        thisCamera.orthographicSize = horizontalResolution / currentAspect / aspectDesign;
    }
}