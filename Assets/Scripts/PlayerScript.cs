using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    //Vars
    public Camera playerCamera;
    public GameObject holdParent;
    public float walkSpeed = 4;
    public float jumpStrength = 25;
    public float mouseSensitivity = 1;
    public float interactRange = 7;
    float rotX = 0;

    private InputSystem_Actions Inputs;

    public Rigidbody heldObj;

    CharacterController characterController;

    private void Awake()
    {
        Inputs = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        Inputs.Enable();
        Inputs.Player.Interact.started += Interact;
        Inputs.Player.Throw.started += ThrowItem;
        Inputs.Player.Back.started += ReturnToMenu;

    }

    private void OnDisable()
    {
        Inputs.Disable();
        Inputs.Player.Interact.started -= Interact;
        Inputs.Player.Throw.started -= ThrowItem;
        Inputs.Player.Back.started -= ReturnToMenu;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //Lock Cursor to game and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MouseLook();
        Locomotion();
        if(heldObj != null)
        {
            heldObj.transform.position = Vector3.Lerp(heldObj.transform.position, holdParent.transform.position, 7.0f * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        
    }

    private void ReturnToMenu(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * -1000, Color.red, 1, false);
        //Debug.Log("Interacted");
        if(heldObj == null)
        {
            RaycastHit hit;
            if(Physics.SphereCast(playerCamera.transform.position, 0.25f, playerCamera.transform.forward, out hit, interactRange, 8))
            {
                Debug.Log("Sphere trace hit " + hit.transform.gameObject);
                //Setup selected object for hold if it's trash
                if (hit.transform.gameObject.GetComponent<Rigidbody>())
                {
                    heldObj = hit.transform.gameObject.GetComponent<Rigidbody>();
                    heldObj.useGravity = false;
                    heldObj.linearVelocity = new Vector3(0,0,0);
                    heldObj.transform.parent = holdParent.transform;
                }
            }
        }
        else
        {
            // Disable all hold effects when releasing trash, and add a bit of force based on mouse movement so they don't just fall to the floor in an unsatisfying manner
            if (heldObj != null)
            {
                heldObj.useGravity = true;
                heldObj.transform.parent = null;
                Vector2 MouseLookVal = Inputs.Player.Look.ReadValue<Vector2>();
                Vector3 ReleaseVel = playerCamera.transform.right * MouseLookVal.x * 50;
                ReleaseVel.y = (MouseLookVal.y * 20);
                Debug.Log(MouseLookVal);
                heldObj.AddForce(ReleaseVel);
                heldObj = null;
            }
        }
    }

    void ThrowItem(InputAction.CallbackContext obj)
    {
        if (heldObj != null)
        {
            heldObj.useGravity = true;
            heldObj.transform.parent = null;
            heldObj.AddForce(playerCamera.transform.forward * 350f);
            heldObj = null;
        }
    }

    //Player movement, should be updated to use Unity's newer input system like the grab and throw at some point
    void Locomotion()
    {
        Vector3 moveDir = (transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical")) + (transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal"));
        if(!characterController.isGrounded)
        {
            moveDir.y -= 9.8f * Time.deltaTime;
        }
        characterController.Move(moveDir * walkSpeed * Time.deltaTime);
    }

    void MouseLook()
    {
        rotX += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotX = Mathf.Clamp(rotX, -90, 90);
        playerCamera.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
    }
}