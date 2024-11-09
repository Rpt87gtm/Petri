using Mirror;

public class SetCameraTargetOnStart : NetworkBehaviour
{

    private void Start()
    {
        if (isLocalPlayer)
        {
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            PlayerCellsGroup playerCells = GetComponent<PlayerCellsGroup>();

            if (cameraFollow != null && playerCells)
            {
                cameraFollow.SetTarget(playerCells);
            }
        }
    }

}
