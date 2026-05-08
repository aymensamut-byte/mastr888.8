using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyController enemy;
    
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image enemyHealthBar;
    
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text enemyHealthText;
    
    [SerializeField] private Text gameStatusText;
    
    private bool gameEnded = false;
    
    private void Update()
    {
        if (gameEnded) return;
        
        UpdateHealthBars();
        CheckGameStatus();
    }
    
    private void UpdateHealthBars()
    {
        if (player != null && playerHealthBar != null)
        {
            float healthPercent = player.GetHealth() / player.GetMaxHealth();
            playerHealthBar.fillAmount = healthPercent;
            
            // تغيير اللون (أخضر -> أحمر)
            playerHealthBar.color = Color.Lerp(Color.red, Color.green, healthPercent);
            
            if (playerHealthText != null)
            {
                playerHealthText.text = $"Player: {player.GetHealth():F0} / {player.GetMaxHealth():F0}";
            }
        }
        
        if (enemy != null && enemyHealthBar != null)
        {
            float healthPercent = enemy.GetHealth() / enemy.GetMaxHealth();
            enemyHealthBar.fillAmount = healthPercent;
            
            // تغيير اللون (أخضر -> أحمر)
            enemyHealthBar.color = Color.Lerp(Color.red, Color.green, healthPercent);
            
            if (enemyHealthText != null)
            {
                enemyHealthText.text = $"Enemy: {enemy.GetHealth():F0} / {enemy.GetMaxHealth():F0}";
            }
        }
    }
    
    private void CheckGameStatus()
    {
        if (player.GetHealth() <= 0)
        {
            gameEnded = true;
            ShowGameOver("❌ You Lose! Enemy Wins! ❌");
        }
        else if (enemy.GetHealth() <= 0)
        {
            gameEnded = true;
            ShowGameOver("✅ You Win! Enemy Defeated! ✅");
        }
    }
    
    private void ShowGameOver(string message)
    {
        if (gameStatusText != null)
        {
            gameStatusText.text = message;
            gameStatusText.color = message.Contains("Win") ? Color.green : Color.red;
        }
    }
}
