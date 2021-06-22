using UnityEngine;

public class ObjectAutoDestroy : MonoBehaviour
{
    public float Duration;

    public void Start()
    {
        Invoke("DestroySelf", Duration);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
