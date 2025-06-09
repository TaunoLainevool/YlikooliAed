using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator; 
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * moveSpeed;
    }
   
   public void Move(InputAction.CallbackContext context){
    animator.SetBool("isWalking", true);
    if(context.canceled){
        animator.SetBool("isWalking", false);
        animator.SetFloat("lastInputX", moveInput.x);
        animator.SetFloat("lastInputY", moveInput.y);
    }

    moveInput = context.ReadValue<Vector2>();
    animator.SetFloat("inputX", moveInput.x);
    animator.SetFloat("inputY", moveInput.y);
   }
}
