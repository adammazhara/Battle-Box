using System.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerMovement2 : Player {
    protected bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;   
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private void Update() {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && IsGrounded()) rb.velocity = new Vector2(rb.velocity.x, jumpingPower); 
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) && rb.velocity.y > 0f) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) StartCoroutine(Dash());
        Flip();
    }

    private void FixedUpdate() {
        if (isDashing) return;
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    
    private void Flip() {
        if(Input.GetKey(KeyCode.D)) transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if(Input.GetKey(KeyCode.A)) transform.rotation = Quaternion.Euler(0f, 180f, 0f);  

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;  
        }
    }
 
    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void SlowDownMovement(float slowFactor) {
        speed = (slowFactor + 3); // reduce player's movement speed by the given slow factor
        jumpingPower = (slowFactor + 3); // reduce player's jumping power by the given slow factor
    }

    public void RestoreMovement() {
        speed = 16f;
        jumpingPower = 32f;
    }

    //private void OnCollisonEnter2D(Collision2D col) {
    //    if (col.gameObject.tag == "Ground") {
    //        IsGrounded = true;
    //    }
    //}
//
    //private void OnCollisonExit2D(Collision2D col) {
    //    if (col.gameObject.tag == "Ground") {
    //        IsGrounded = false;
    //    }
    //}
}