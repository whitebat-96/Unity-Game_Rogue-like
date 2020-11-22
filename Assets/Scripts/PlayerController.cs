using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArm;

    private Camera theCam;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();      //this line ensures that the character moves the same distance with same force in all directions.

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

        theRB.velocity = moveInput * moveSpeed;         //basically setting the velocity of rigidbody '0' when stopped(no movespeed)

        Vector3 mousePos = Input.mousePosition;         // This will give us the position of the mouse arrow

        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);
        //This gives us the position of the player from the viewpoint of main camera(localposition gives us the 
        //transform relative to the parent transform)

        //Now, we adjust the player body ,such that it faces the direction where it shoots. Otherwise facing right while shooting left is
        //just wierd.

        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);  // Done to correct wierd positions of the gun.
        }
        else
        {
            transform.localScale = Vector3.one ;
            gunArm.localScale = Vector3.one ;
        }




        // Now we will learn ,how to rotate the arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);       //Note that here we took Vector2

        //Now we need to calculate the angle which is between these two points i guess
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        gunArm.rotation = Quaternion.Euler(0, 0, angle);    //Note that rotation does not follow vector maths , but quaterion maths.
                                                            //So, we use quaternion.euler if we want to give it vector inputs.


        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
