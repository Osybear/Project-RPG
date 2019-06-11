using UnityEngine;

public class SetCameraParentNull : MonoBehaviour, ISetParent
{
    public void Set(Transform camera) {
        camera.SetParent(null);
    }
}
