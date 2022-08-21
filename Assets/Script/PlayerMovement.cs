using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isGround = true;
    public bool isRun = false;

    [SerializeField]
    private float walkSpeed = 15f;
    [SerializeField]
    private float runSpeed = 30f;

    public float jumpForce = 5.0f;

    [SerializeField]
    private float applySpeed;

    // ÄÄÆ÷³ÍÆ®
    private Animator animator;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();

        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        TryRun();
        Movement();
    }

    private void Movement()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX; // (1, 0, 0)
        Vector3 _moveVertical = transform.forward * _moveDirZ; // (0, 0, 1)

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        rigidbody.MovePosition(transform.position + _velocity * Time.deltaTime);

        animator.SetFloat("xDir", _moveDirX);
        animator.SetFloat("yDir", _moveDirZ);

        TryJump();
    }

    private void TryRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Running();
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            CancelRunning();
        }
    }

    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rigidbody.velocity = transform.up * jumpForce;
        animator.SetTrigger("isJump");
    }

    private void Running()
    {
        isRun = true;
        animator.SetBool("isRun", true);
        applySpeed = runSpeed;
    }

    private void CancelRunning()
    {
        isRun = false;
        animator.SetBool("isRun", false);
        applySpeed = walkSpeed;
    }

    private void CheckGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y - 0.1f);
        animator.SetBool("isJump", false);
    }
}
