using Mirror;

public class SetCameraTargetOnStart : NetworkBehaviour
{

    private void Start()
    {
        if (isLocalPlayer)
        {
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            PlayerCells playerCells = GetComponent<PlayerCells>();

            if (cameraFollow != null && playerCells)
            {
                cameraFollow.SetTarget(playerCells);
            }
        }
    }

}
