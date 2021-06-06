using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implements the IPlayerInput.
/// The logic here is that we put an interface to listen to inputs, so if the input requirement
/// changes, all you need to do is change either the interface or the implementation and your input
/// will change globally everywhere on the project
/// </summary>
public class PlayerInputImpl : IPlayerInput
{
    public float Horizontal => Input.GetAxis("Horizontal");
}