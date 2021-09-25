using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public float rotateSpeed;
    public float bounceForce;

    private Vector3 moveDirection;

    public CharacterController charController;
    private Camera theCam;
    public GameObject playerModel;
    public Animator anim;

    public bool isKnocking;
    public float knockBackLength;
    private float knockBackCounter;
    public Vector2 knockBackPower;

    public GameObject[] playerPieces;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocking) {
            float yStore = moveDirection.y;

            moveDirection = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
            moveDirection.Normalize();
            moveDirection *= moveSpeed;
            moveDirection.y = yStore;

            if (charController.isGrounded) {
                if (Input.GetButtonDown("Jump")) {
                    moveDirection.y = jumpForce;
                } else {
                    moveDirection.y = -1f;
                }
            } else {
                moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            }

            charController.Move(moveDirection * Time.deltaTime);

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Vector3 inPlaneMoveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
                Quaternion newRotation = Quaternion.LookRotation(inPlaneMoveDirection);
                Quaternion oldRotation = playerModel.transform.rotation;
                float t = rotateSpeed * Time.deltaTime;
                playerModel.transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t);
            }
        } else {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;

            moveDirection = -playerModel.transform.forward * knockBackPower.x;
            moveDirection.y = yStore;

            if (charController.isGrounded) {
                moveDirection.y = -1f;
            } else {
                moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            }

            charController.Move(moveDirection * Time.deltaTime);

            if (knockBackCounter <= 0) {
                isKnocking = false;
            }
        }

        Vector3 inPlaneVelocity = new Vector3(charController.velocity.x, 0f, charController.velocity.z);
        anim.SetFloat("Speed", inPlaneVelocity.magnitude);
        anim.SetBool("isGrounded", charController.isGrounded);
    }

    public void KnockBack()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;

        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }
}
