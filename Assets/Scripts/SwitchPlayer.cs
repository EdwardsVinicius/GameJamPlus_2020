﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchPlayer : MonoBehaviour
{
    public GameObject player1, player2;
    private Animator animator;
    private Vector3 p1, p2;
    private int whichPlayerIsOn = 1;
    // Start is called before the first frame update
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
    }

    void SwitchPlayers()
    {
        switch (whichPlayerIsOn)
        {
            case 1:
                whichPlayerIsOn = 2;
                p1 = player1.transform.position;
                player2.transform.position = new Vector3(p1.x, p1.y, p1.z);

                player1.gameObject.SetActive(false);
                player2.gameObject.SetActive(true);
                break;

            case 2:
                whichPlayerIsOn = 1;
                p2 = player2.transform.position;
                player1.transform.position = new Vector3(p2.x, p2.y, p2.z);

                player1.gameObject.SetActive(true);
                player2.gameObject.SetActive(false);
                break;
        }
    }

}