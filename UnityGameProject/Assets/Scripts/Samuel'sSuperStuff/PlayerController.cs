﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Entity This;

    private Vector3 MoveDir;

	// Use this for initialization
	void Start ()
    {
        This = GetComponent<Entity>();
	}
	
	// Update is called once per frame
	void Update ()
    {
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            MoveDir = transform.TransformDirection(MoveDir);            

            if (Input.GetButton("Jump"))
            {
                This.Jump();
            }     

        This.Move(MoveDir);
        //This.CallyMove(MoveDir);
        //This.StaticCrouch(MoveDir);
	}
}
