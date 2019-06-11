using UnityEngine;

public class AxisRawMovementInput : MonoBehaviour, IMovementInput
{
    public float GetHorizontal() {
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVertical() {
        return Input.GetAxisRaw("Vertical");
    }
}
