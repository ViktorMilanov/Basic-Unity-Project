using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    private bool IsSpaceKeyPressed = false;
    private float HorizontalMovement;
    private Rigidbody rb;
    public static event Action OnPlayerWin;
    private int coinCount = 0;


    // Start is called before the first frame update

    private void OnEnable()
    {
        Player.OnPlayerWin += DisableMovement;
    }

    private void OnDisable()
    {
        Player.OnPlayerWin -= DisableMovement;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        EnableMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    //Check if Space Key is pressed down
        {
           IsSpaceKeyPressed = true;
        }

        HorizontalMovement = Input.GetAxis("Horizontal") * 1.8f;
    }

    void FixedUpdate()
    {
        if(coinCount != 0)
        {
            DisableMovement();
            OnPlayerWin?.Invoke();
        }

        rb.velocity = new Vector3(HorizontalMovement, rb.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (IsSpaceKeyPressed) 
        {
            rb.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            IsSpaceKeyPressed=false;
        }

        
    }

    private void DisableMovement()
    {
        rb.isKinematic = true;
    }

    private void EnableMovement()
    {
        rb.isKinematic = false;
    }

    

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            coinCount++;
        } 
    }
}
