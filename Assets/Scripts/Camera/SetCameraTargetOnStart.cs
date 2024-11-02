using Mirror;

public class SetCameraTargetOnStart : NetworkBehaviour
{
    private CameraFollow _cameraFollow;
    private void Start()
    {
        if (isLocalPlayer)
        {  
            _cameraFollow = FindObjectOfType<CameraFollow>();
            if (_cameraFollow != null)
            {
                _cameraFollow.SetTarget(transform);
            }
        }
    }

}
