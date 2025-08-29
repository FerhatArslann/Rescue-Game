using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool hasWeight = true; // Player's weight
    public float moveSpeed = 5f; // Adjust the move speed as needed
    public float sprintSpeed = 10f; // Speed while sprinting
    public float weight = 0;
    public float weightThreshold;
    public float slowPerWeight;
    private float weightMovementMult;
    public Rigidbody2D rb;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
		// Player components
		rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
		// Get input from arrow or WASD keys
		float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
		// Normalize the movement vector to ensure constant speed in all directions
		Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
		// Update Animator parameters for animation transitions
		animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (weightThreshold - weight < 0)
        {
            weightMovementMult = 1f - (0.01f * (weight - weightThreshold) / slowPerWeight);
        }
        else
        {
            weightMovementMult = 1f;
        }
		// Check if the player is holding the sprint key (Left Shift or Right Shift)
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Sprinting
            rb.velocity = movement * sprintSpeed * weightMovementMult; 
        }
        else
        {
            // Regular movement
            rb.velocity = movement * moveSpeed * weightMovementMult;
        }
		// Check if there is any movement in the horizontal or vertical direction
		if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
			// Update Animator parameters for last movement direction
			animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
			animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
		}
	}
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Tree"))
        {
            SpriteRenderer spriteRenderer = collider2D.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Tree"))
        {
            SpriteRenderer spriteRenderer = collider2D.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
