using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VR_Movement : MonoBehaviour
{
    private Valve.VR.InteractionSystem.Player MyPlayer;

    public SteamVR_Action_Vector2 MoveValue;
    public Rigidbody RBody;

    public float Distance = 0.5f;
    public float Sensitivity;
    public float MaxSpeed;

    private float Speed = 0.0f;

    private void Start()
    {
        MyPlayer = GetComponent<Valve.VR.InteractionSystem.Player>();
    }

    void Update()
    {
        RaycastHit hit;
        if (RBody.SweepTest(MyPlayer.hmdTransform.TransformDirection(Vector3.forward), out hit, Distance))
        {
        }
        else
        {
            if (MoveValue.axis.y > 0)
            {
                // create a new vector that gets the direction the HMD is facing
                Vector3 direction = MyPlayer.hmdTransform.TransformDirection(new Vector3(0, 0, MoveValue.axis.y));

                // multipies the joystick value by the senstivity value
                Speed = MoveValue.axis.y * Sensitivity;

                // ensure the speed doesn't go above the max speed
                Speed = Mathf.Clamp(Speed, 0, MaxSpeed);

                // move the player prefab along the horizontal plane in the direction specified earlier
                transform.position += Speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
            }

            if (MoveValue.axis.y < 0)
            {
                // create a new vector that gets the direction the HMD is facing
                Vector3 direction = MyPlayer.hmdTransform.TransformDirection(new Vector3(0, 0, MoveValue.axis.y));

                // multipies the joystick value by the senstivity value
                Speed = MoveValue.axis.y * Sensitivity;

                // ensure the speed doesn't go above the max speed
                Speed = Mathf.Clamp(Speed, -MaxSpeed, 0);

                // move the player prefab along the horizontal plane in the direction specified earlier
                transform.position -= Speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
            }

            if (MoveValue.axis.x > 0)
            {
                // create a new vector that gets the direction the HMD is facing
                Vector3 direction = MyPlayer.hmdTransform.TransformDirection(new Vector3(MoveValue.axis.x, 0, 0));

                // multipies the joystick value by the senstivity value
                Speed = MoveValue.axis.x * Sensitivity;

                // ensure the speed doesn't go above the max speed
                Speed = Mathf.Clamp(Speed, 0, MaxSpeed);

                // move the player prefab along the horizontal plane in the direction specified earlier
                transform.position += Speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
            }

            if (MoveValue.axis.x < 0)
            {
                // create a new vector that gets the direction the HMD is facing
                Vector3 direction = MyPlayer.hmdTransform.TransformDirection(new Vector3(MoveValue.axis.x, 0, 0));

                // multipies the joystick value by the senstivity value
                Speed = MoveValue.axis.x * Sensitivity;

                // ensure the speed doesn't go above the max speed
                Speed = Mathf.Clamp(Speed, -MaxSpeed, 0);

                // move the player prefab along the horizontal plane in the direction specified earlier
                transform.position -= Speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
            }
        }
    }
}