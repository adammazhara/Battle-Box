using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 16f;
    private float jumpingPower = 32f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] private GameObject clonePrefab;
    private GameObject clone;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private GameObject shieldPrefab;
    private GameObject shield;
    private bool shieldActive = false;
    private float shieldCooldown = 10f;
    private int shieldCount = 0;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.R) && shield == null)
            ActivateShield();

        if (Input.GetKeyDown(KeyCode.C) && clone == null)
            Clone();

        if (Input.GetKeyDown(KeyCode.A))
            isFacingRight = false;

        else if (Input.GetKeyDown(KeyCode.D))
            isFacingRight = true;

        if (isDashing) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash());

        Flip();
    }
    
    private void Clone() {
        if (clone == null) {
            Vector3 randomOffset = new Vector3(transform.position.x - 30f, transform.position.y + 1f, transform.position.z);
            clone = Instantiate(clonePrefab, randomOffset, Quaternion.identity);
            
            clone.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        } else {
            Vector3 randomOffset = new Vector3(transform.position.x - 30f, transform.position.y + 1f, transform.position.z);
            clone.transform.position = randomOffset;
            clone.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        }
    }
    private void FixedUpdate() {
        if (isDashing) return;

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (clone != null)
            clone.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        
        if (shield != null){
            // calculate the offset based on facing direction
            float offset = isFacingRight ? 1f : -1f;

            // set the position of the shield just in front of the player
            shield.transform.position = transform.position + new Vector3(offset, 0f, 0f);
        }
}

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() {
        if(Input.GetKey(KeyCode.D))
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);  

        if(Input.GetKey(KeyCode.A))
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);  

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void ActivateShield() {
        if (!shieldActive && shieldCount < 3) {
            shieldCount++;
            
            if (!shieldActive) {
                // calculate the offset based on facing direction
                float offset = isFacingRight ? 1f : -1f;

                // spawn the shield just in front of the player
                Vector3 spawnPosition = transform.position + new Vector3(offset, 0f, 0f);

                // instantiate the shield
                shield = Instantiate(shieldPrefab, spawnPosition, Quaternion.Euler(0f, offset > 0f ? 0f : 180f, 0f));

                // set shield active flag to true and start coroutine to disable shield
                shieldActive = true;
                StartCoroutine(DisableShield());
            }
        }
    }
    private IEnumerator DisableShield() {
        yield return new WaitForSeconds(3f);
        shieldActive = false;
        Destroy(shield);

        yield return new WaitForSeconds(shieldCooldown);
    }

    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}