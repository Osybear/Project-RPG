using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public Image healthBar;

    [Server]
    public void TakeDamage(int amount) {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0) {
            currentHealth = 0;
        }

        RpcOnChangeHealth(currentHealth);
    }

    private void Update() {
        if(!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            CmdTakeHealth();
    }

    //This is a Network command, so the damage is done to the relevant GameObject
    [Command]
    private void CmdTakeHealth() {
        //Apply damage to the GameObject
        TakeDamage(2);
    }

    [ClientRpc]
    private void RpcOnChangeHealth(int health) {
        currentHealth = health;
        healthBar.fillAmount = (float)health / maxHealth;
        Debug.Log("Change Health Ammount" + (maxHealth - health));
    }
}