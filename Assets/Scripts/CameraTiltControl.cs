using UnityEngine;

public class CameraTiltControl : MonoBehaviour
{
    public GameObject CameraLookAt;
    public float scrollSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        LookAtTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && Camera.main.transform.position.z < -1) // forward scroll (go to z = 0)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + (1f * scrollSpeed));
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && Camera.main.transform.position.z > -15) // backwards  (go to z = -15)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - (1f * scrollSpeed));
        }
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Camera.main.transform.LookAt(CameraLookAt.transform.position);
    }
}
