using UnityEngine;

public class RigidCameraFollowPlayer : MonoBehaviour, IFollowTarget 
{
    private Vector3 offset;

    public void SetTransformOffset(Transform camera, Transform player) {
        offset = camera.position - player.position;
    }

    public void SetTransformPosition(Transform camera, Transform player) {
        camera.position = player.position + offset;
    }
}
