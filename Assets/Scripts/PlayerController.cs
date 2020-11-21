using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

        theRB.velocity = moveInput * moveSpeed;         //basically setting the velocity of rigidbody '0' when stopped(no movespeed)

        Vector3 mousePos = Input.mousePosition;         // This will give us the position of the mouse arrow

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        //This gives us the position of the player from the viewpoint of main camera(localposition gives us the 
        //transform relative to the parent transform)

        // Now we will learn ,how to rotate the arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);       //Note that here we took Vector2

        //Now we need to calculate the angle which is between these two points i guess
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        gunArm.rotation = Quaternion.Euler(0, 0, angle);    //Note that rotation does not follow vector maths , but quaterion maths.
                                                            //So, we use quaternion.euler if we want to give it vector inputs.



    }
}
