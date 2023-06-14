using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VR_Movement : MonoBehaviour
{
    private Valve.VR.InteractionSystem.Player MyPlayer;
    private Vector3 Movement;

    public SteamVR_Action_Vector2 MoveValue;
    public Rigidbody RBody;
    public float Speed = 70.0f;

    private void Start()
    {
        MyPlayer = GetComponent<Valve.VR.InteractionSystem.Player>();
    }

    void Update()
    {
        Movement.x = -MoveValue.axis.x * Speed * Time.deltaTime;
        Movement.z = -MoveValue.axis.y * Speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        RBody.velocity = Movement;
    }
}