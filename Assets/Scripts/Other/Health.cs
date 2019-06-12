using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public Image healthBar;

    private void Update() {
        if(!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            CmdTakeHealth();
    }

    [Server]
    public void OnDamage(int amount) {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0) {
            currentHealth = 0;
        }

        RpcOnChangeHealth(currentHealth);
    }

    [Command]
    private void CmdTakeHealth() {
        OnDamage(2);
    }

    [ClientRpc]
    private void RpcOnChangeHealth(int health) {
        currentHealth = health;
        SetHealthBar();
    }

    [Client]
    private void SetHealthBar() {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}