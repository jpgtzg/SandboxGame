/* 
 * Copyright (c) 2022 AndromedaHelix
 * Copyrights licensed under the MIT License
 * See accompanying LICENSE file for terms
*/

/*
 * This script manages the input/output 
 * of the Input System class "Controls".
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Controls controls;

    #region Initializers
    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    #endregion

    public Vector2 MovementAction() //Returns Movement Values
    {
        return controls.Player.Movement.ReadValue<Vector2>();
    }

    public bool JumpAction() //Returns true when Jump pressed
    {
        return controls.Player.Jump.triggered;
    }

    public bool LeftMouseAction() //Returns true when Left Mouse clicked
    {
        return controls.Player.LeftMouseAction.triggered;
    }

    public bool RightMouseAction() //Returns true when Right Mouse clicked
    {
        return controls.Player.RightMouseAction.triggered;
    }

    public bool QAction1() //Returns true when Q pressed
    {
        return controls.Player.QAction1.triggered;
    }

    public bool EAction2() //Returns true when E pressed
    {
        return controls.Player.EAction2.triggered;
    }

    public bool FAction3() //Returns true when F pressed
    {
        return controls.Player.FAction3.triggered;
    }
}