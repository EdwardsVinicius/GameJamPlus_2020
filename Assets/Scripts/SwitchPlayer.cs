﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SwitchPlayer : MonoBehaviour
{
    public GameObject player1, player2;
    private Animator animator;
    private Vector3 p1, p2, parent;
    private int onPlayer = 1;


    [SerializeField]
    private Transform follow = null;

    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;

    private void Awake()
    {
        follow = player1.transform;
        originalLocalPosition = follow.localPosition;
        originalLocalRotation = follow.localRotation;
    }


    void Start()
    {
        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(false);
        animator = GameObject.Find("PlayerPenguin").GetComponentInChildren<Animator>();
    }

    private void Update()
    {

        if (Input.GetKeyDown("x"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("dashing"))
                return;
               
            SwitchPlayers();
        }
        
        followActivePlayer();
    }

    void SwitchPlayers()
    {
        switch (onPlayer)
        {
            case 1:
                onPlayer = 2;
                p1 = player1.transform.position;
                player2.transform.position = new Vector3(p1.x, p1.y, p1.z);

                player1.gameObject.SetActive(false);
                player2.gameObject.SetActive(true);

                follow = player2.transform;
                break;

            case 2:
                onPlayer = 1;
                p2 = player2.transform.position;
                player1.transform.position = new Vector3(p2.x, p2.y , p2.z);

                player1.gameObject.SetActive(true);
                player2.gameObject.SetActive(false);

                follow = player1.transform;
                break;
        }
    }

    public void followActivePlayer()
    {
        transform.position = follow.position;

        //HAS TO BE IN THIS ORDER
        //sort of "reverses" the quaternion so that the local rotation is 0 if it is equal to the original local rotation
        follow.RotateAround(follow.position, follow.forward, -originalLocalRotation.eulerAngles.z);
        follow.RotateAround(follow.position, follow.right, -originalLocalRotation.eulerAngles.x);
        follow.RotateAround(follow.position, follow.up, -originalLocalRotation.eulerAngles.y);

        //rotate the parent
        transform.rotation = follow.rotation;

        //moves the parent by the child's original offset from the parent
        transform.position += -transform.right * originalLocalPosition.x;
        transform.position += -transform.up * originalLocalPosition.y;
        transform.position += -transform.forward * originalLocalPosition.z;

        //resets local rotation, undoing step 2
        follow.localRotation = originalLocalRotation;

        //reset local position
        follow.localPosition = originalLocalPosition;
    }

}
