﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Controller;
using UnityStandardAssets.Characters.FirstPerson;

public class AgentController : MonoBehaviour
{
    private IState state;
    public GameObject FPController;
    public GameObject FlyCamPrefab;

    // Possible states
    public IState WalkingState;
    public IState FlyingState;

    // Start is called before the first frame update
    void Start()
    {
        RigidbodyFirstPersonController controller = FPController.GetComponent<RigidbodyFirstPersonController>();
        Camera camera = controller.cam;
        WalkingState = new WalkingState(this, controller, camera);
        FlyingState = new FlyingState(this, FlyCamPrefab, FPController.transform);
        this.state = this.WalkingState;
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    public void ChangeState(IState newState)
    {
        state.Exit();
        state = newState;
        state.Enter();
    }
}