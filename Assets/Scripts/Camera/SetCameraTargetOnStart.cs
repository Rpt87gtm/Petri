using UnityEngine;

public class SetCameraTargetOnStart : MonoBehaviour
{
    private void Start()
    {
        Camera camera = Camera.main;
        camera.gameObject.GetComponent<CameraFollow>()?.SetTarget(transform);
    }

}
