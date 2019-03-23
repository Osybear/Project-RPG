using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Health : NetworkBehaviour {
    public const int maxHealth = 100;
    //Detects when a health change happens and calls the appropriate function
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    public Image healthBar;

    public void TakeDamage(int amount) {
        if (!isServer)
            return;

        //Decrease the "health" of the GameObject
        currentHealth -= amount;
        //Make sure the health doesn't go below 0
        if (currentHealth <= 0) {
            currentHealth = 0;
        }
    }

    private void Update() {
        //If the space key is pressed, decrease the GameObject's own "health"
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isLocalPlayer)
                CmdTakeHealth();
        }
    }

    private void OnChangeHealth(int health) {   
        healthBar.fillAmount = (float)health / maxHealth;
    }

    //This is a Network command, so the damage is done to the relevant GameObject
    [Command]
    private void CmdTakeHealth() {
        //Apply damage to the GameObject
        TakeDamage(2);
    }
}