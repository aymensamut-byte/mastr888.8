using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airDrag = 2f;
    
    [Header("Combat")]
    [SerializeField] private float punchDamage = 10f;
    [SerializeField] private float punchCooldown = 0.5f;
    [SerializeField] private float punchRange = 2f;
    [SerializeField] private float punchRadius = 0.5f;
    [SerializeField] private float maxHealth = 100f;
    
    [Header("Ground Check")]
    [SerializeField] private float groundDist = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody rb;
    private float currentHealth;
    private float punchCooldownTimer;
    private bool isGrounded;
    private Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }
    
    private void Update()
    {
        HandleInput();
        GroundCheck();
        UpdateDrag();
        punchCooldownTimer -= Time.deltaTime;
    }
    
    private void FixedUpdate()
    {
        Move();
    }
    
    private void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        if (Input.GetMouseButtonDown(0) && punchCooldownTimer <= 0)
        {
            Punch();
            punchCooldownTimer = punchCooldown;
        }
    }
    
    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDist, groundLayer);
    }
    
    private void Move()
    {
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }
    
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    
    private void UpdateDrag()
    {
        rb.drag = isGrounded ? groundDrag : airDrag;
    }
    
    private void Punch()
    {
        Vector3 punchPos = transform.position + transform.forward * punchRange;
        Collider[] hitColliders = Physics.OverlapSphere(punchPos, punchRadius);
        
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyController enemy = col.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(punchDamage);
                }
            }
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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
        Debug.Log("Player Died!");
        // يمكن إضافة رسوم متحركة للموت لاحقاً
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * punchRange, punchRadius);
    }
}
