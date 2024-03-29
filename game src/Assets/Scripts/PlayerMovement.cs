using System.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour {
    private float defaultGravityScale;
    private float horizontal;
    private float speed = 16f;
    private float jumpingPower = 32f;
    //public HealthBar healthbar;
    public bool isFacingRight = true;


    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] private GameObject clonePrefab;
    private GameObject clone;
    public SpriteRenderer spriteRenderer;
    private float health = 100f;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private GameObject shieldPrefab;
    private GameObject shield;
    private bool shieldActive = false;
    private float shieldCooldown = 10f;
    private int shieldCount = 0;
    public GravityWall gravityWall;
    private float originalGravityScale;
    public float floatingForce = 10f;
    private bool isInvincible = false;
    private float invincibleTime = 3f;
    [SerializeField] private GameObject goldAuraPrefab;
    private GameObject goldAura;
    private bool goldAuraActive = false;


    void Start() {
        //healthbar.SetHealth((int)health);
        defaultGravityScale = rb.gravityScale;
        originalGravityScale = rb.gravityScale;
    }
    private void Update() {

        if (Input.GetKeyDown(KeyCode.B))
        {
        ActivateGoldAura();
        }
        //healthbar.SetHealth((int)health);
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


        //if(Input.GetKey(KeyCode.P))
        //    TestServerRPC();
        if (Input.GetKeyDown(KeyCode.L) && !isInvincible) {
            isInvincible = true;
            StartCoroutine(ActivateInvisibility());
    }
       


        Flip();
    }
   
    private void Clone() {
        if (clone == null) {
            Vector3 randomOffset = new Vector3(transform.position.x - 10f, transform.position.y + 1f, transform.position.z);
            clone = Instantiate(clonePrefab, randomOffset, Quaternion.identity);
           
            clone.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        } else {
            Vector3 randomOffset = new Vector3(transform.position.x - 10f, transform.position.y + 1f, transform.position.z);
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
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("GravityWall"))
    {
        rb.gravityScale = 1f;
        rb.AddForce(Vector2.up * floatingForce, ForceMode2D.Force);
    }




    // check if the player collided with a spike
    if (collision.CompareTag("Spike"))
    {
        // check if the player is currently invincible due to shield activation
        if (shieldActive) {
            return;
        }
       
        // call the TakeDamage function to reduce health
        TakeDamage(10f);


        // change the color of the sprite renderer to red
        spriteRenderer.color = Color.red;
        Invoke("ResetColor", 0.1f);
    }
}
void OnTriggerExit2D(Collider2D collision)
{
    if (collision.CompareTag("GravityWall"))
    {
        rb.gravityScale = originalGravityScale;
    }
}


// add a function to take damage and check if the player is dead
public void TakeDamage(float amount)
{
    if (goldAuraActive)
    {
        Heal(amount);
    }
    else
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            // turn player red temporarily
            spriteRenderer.color = Color.red;
            Invoke("ResetColor", 0.5f);
        }
    }
    //healthbar.SetHealth((int)health);
}


private void ResetColor()
{
    spriteRenderer.color = Color.white;
}
public void Heal(float amount) {
    health += amount;
    health = Mathf.Clamp(health, 0f, 100f);
}
//[ServerRpc]
//private void TestServerRPC(){
//    Debug.Log("TestServerRPC " + OwnerClientId);
//}


public float GetJumpingPower()
{
    return jumpingPower;
}


private IEnumerator ActivateInvisibility() {
    spriteRenderer.enabled = false;
    yield return new WaitForSeconds(invincibleTime);
    spriteRenderer.enabled = true;
    isInvincible = false;
}

private void ActivateGoldAura()
{
    if (!goldAuraActive)
    {
        goldAura = Instantiate(goldAuraPrefab, transform.position, Quaternion.identity, transform);
        goldAuraActive = true;
        StartCoroutine(DeactivateGoldAura());
    }
}

private IEnumerator DeactivateGoldAura()
{
    yield return new WaitForSeconds(5f); // You can change the duration of the gold aura here
    goldAuraActive = false;
    Destroy(goldAura);
}
public void SlowDownMovement(float slowFactor)
    {
        speed = (slowFactor + 3); // reduce player's movement speed by the given slow factor
        jumpingPower = (slowFactor + 3); // reduce player's jumping power by the given slow factor
    }
public void RestoreMovement()
{
    speed = 16f;
    jumpingPower = 32f;
}
public void SetVelocity(Vector2 velocity){
    rb.velocity = velocity;
}
public void SetVelocityZero(){
    rb.velocity = Vector2.zero;
}
public Vector2 GetVelocity(){
    return rb.velocity;
}
}
