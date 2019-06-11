using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class UIManager : NetworkBehaviour {
    
    public GameObject worldCanvas;

    private void Start() {
        worldCanvas.transform.SetParent(null);
    }
}
 