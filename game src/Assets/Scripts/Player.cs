using System;
using UnityEngine;

public class Player : MonoBehaviour {
    protected Rigidbody2D rb;
    protected Transform transform;
    protected BoxCollider2D collider;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected TrailRenderer tr;
    //public HealthBar healthbar;
    
    private SpriteRenderer spriteRenderer;
    protected float health = 100f;

    protected float defaultGravityScale;
    protected float horizontal;
    protected float speed = 16f;
    protected float jumpingPower = 32f;

    public GravityWall gravityWall;
    protected float originalGravityScale = 1f;

    private bool isInvincible = false;
    private float invincibleTime = 3f;

    [SerializeField] private GameObject goldAuraPrefab;
    private GameObject goldAura;
    private bool goldAuraActive = false;

    protected bool shieldActive = false;
    //protected bool IsGrounded;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        //if (Input.GetKeyDown(KeyCode.B)) {
        //    ActivateGoldAura();
        //}
        //healthbar.SetHealth((int)health);

        if (Input.GetKeyDown(KeyCode.L) && !isInvincible) {
            isInvincible = true;
            //StartCoroutine(ActivateInvisibility());
        }

    }

    public void TakeDamage(float amount) {
        if (goldAuraActive) {
            Heal(amount);
        } else {
            health -= amount;
            if (health <= 0) {
                Destroy(gameObject);
            } else {
                // turn player red temporarily
                spriteRenderer.color = Color.red;
                Invoke("ResetColor", 0.5f);
            }
        }
        //healthbar.SetHealth((int)health);
    }

    private void ResetColor() {
        spriteRenderer.color = Color.white;
    }

    public void Heal(float amount) {
        health += amount;
        health = Mathf.Clamp(health, 0f, 100f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Spike")) {
            if (shieldActive) {
                return;
            }

            TakeDamage(10f);

            spriteRenderer.color = Color.red;
            Invoke("ResetColor", 0.1f);
        }
    }

    protected bool IsGrounded() {
        //return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (groundCheck.GetComponent<Collider2D>().IsTouching(collider)) {
            return true;
        }
        return false;   
    }
}