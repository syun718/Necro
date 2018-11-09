using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [HideInInspector]
    public float Laxis_x;
    [HideInInspector]
    public float Laxis_y;
    [HideInInspector]
    public float Raxis_x;
    [HideInInspector]
    public float Raxis_y;
    [HideInInspector]
    public bool button_RB;
    [HideInInspector]
    public bool button_A;
    [HideInInspector]
    public bool button_B;
    [HideInInspector]
    public bool button_X;
    [HideInInspector]
    public bool button_Y;

    public void EscapePlayerInput()
    {
        Laxis_x = Input.GetAxis(GamePadName.GameStick_Left + GamePadName.GameStick_X);
        button_A = Input.GetButtonDown(GamePadName.GamePad_A);
        button_B = Input.GetButtonDown(GamePadName.GamePad_B);
        button_X = Input.GetButton(GamePadName.GamePad_X) || Input.GetKey("x");
        button_Y = Input.GetButtonDown(GamePadName.GamePad_Y) || Input.GetKeyDown("z");
    }

}
