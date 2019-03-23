using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Camera mainCamera;
    public RectTransform image;
    public Transform player;

    private void Update() {
        image.position = mainCamera.WorldToScreenPoint(player.position);
    }
}
