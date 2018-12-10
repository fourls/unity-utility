using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Prefabs")]
    public GameObject deathPSPrefab;
    [Header("References")]
    public BoxCollider2D groundCheck;
    public LayerMask groundLayer;

    [Header("Values")]
    public float speed = 10f;
    public float jumpHeight = 4f;
    public float gravity = -12f;
    public float groundedGravity = -1f;
    public float maxTimeSpacePressed = 0.1f;

    private Rigidbody2D rb;

    private float timeSpacePressed = 0;
    private Vector2 overrideVelocity = Vector2.zero;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            timeSpacePressed = Time.time;
        }
    }

    void FixedUpdate() {
        Vector2 v = rb.velocity;
        
        float vert = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");

        v.x = horiz * speed;
        v.y += gravity * Time.deltaTime;

        if(IsGrounded()) {
            if(v.y < 0)
                v.y = groundedGravity;
            if(Input.GetKey(KeyCode.Space) && Time.time - timeSpacePressed < maxTimeSpacePressed) {
                v.y = CalculateJumpVelocity();
            }
        }

        if(overrideVelocity.x != 0) v.x = overrideVelocity.x;
        if(overrideVelocity.y != 0) v.y = overrideVelocity.y;
        overrideVelocity = Vector2.zero;

        rb.velocity = v;
    }

    public bool IsGrounded() {
        return Physics2D.OverlapBox(groundCheck.transform.position,groundCheck.size,0,groundLayer.value) != null;
    }

    float CalculateJumpVelocity() {
        float v = Mathf.Sqrt(-2 * gravity * jumpHeight);
        return v;
    }

    public void OverrideNextFrameVelocity(Vector2 amount) {
        overrideVelocity = amount;
    }

    public void Die() {
        Instantiate(deathPSPrefab,transform.position + Vector3.back,Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void UnDie() {
        gameObject.SetActive(true);
    }

    public void Teleport() {

    }
}
