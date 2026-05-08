using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private LayerMask playerLayer;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float jumpCooldown = 2f;
    
    [Header("Combat")]
    [SerializeField] private float attackDamage = 15f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackRange2 = 2f;
    [SerializeField] private float attackRadius = 0.6f;
    [SerializeField] private float maxHealth = 80f;
    
    [Header("Ground Check")]
    [SerializeField] private float groundDist = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody rb;
    private PlayerController player;
    private float currentHealth;
    private bool isGrounded;
    private float attackCooldownTimer;
    private float jumpCooldownTimer;
    private bool hasDetectedPlayer = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        currentHealth = maxHealth;
    }
    
    private void Update()
    {
        GroundCheck();
        DetectPlayer();
        attackCooldownTimer -= Time.deltaTime;
        jumpCooldownTimer -= Time.deltaTime;
        
        if (hasDetectedPlayer && player != null)
        {
            ChasePlayer();
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                AttackPlayer();
            }
        }
    }
    
    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDist, groundLayer);
    }
    
    private void DetectPlayer()
    {
        if (player == null) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            hasDetectedPlayer = true;
        }
    }
    
    private void ChasePlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        
        // الحركة نحو اللاعب
        Vector3 moveDir = new Vector3(directionToPlayer.x, 0, directionToPlayer.z).normalized;
        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
        
        // الدوران نحو اللاعب
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        // قفز عشوائي أثناء المطاردة
        if (isGrounded && jumpCooldownTimer <= 0 && Random.value > 0.85f)
        {
            Jump();
            jumpCooldownTimer = jumpCooldown;
        }
    }
    
    private void AttackPlayer()
    {
        if (attackCooldownTimer <= 0)
        {
            Vector3 attackPos = transform.position + transform.forward * attackRange2;
            Collider[] hitColliders = Physics.OverlapSphere(attackPos, attackRadius);
            
            foreach (Collider col in hitColliders)
            {
                if (col.CompareTag("Player") && col.GetComponent<PlayerController>() == player)
                {
                    player.TakeDamage(attackDamage);
                    Debug.Log("Enemy dealt " + attackDamage + " damage to player!");
                }
            }
            
            attackCooldownTimer = attackCooldown;
        }
    }
    
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        hasDetectedPlayer = true;
        Debug.Log("Enemy Health: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public float GetHealth()
    {
        return currentHealth;
    }
    
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    
    private void Die()
    {
        Debug.Log("Enemy Defeated!");
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        // رسم نطاق الكشف
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // رسم نطاق الهجوم
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
