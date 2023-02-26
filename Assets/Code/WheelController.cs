using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;


public class WheelController:MonoBehaviour {
	public WheelCollider fLWheel, fRWheel, rLWheel, rRWheel;
	public Transform fLTransform, fRTransform, rLTransform, rRTransform;
	public float maxSteerAngle = 30;
	public float motorForce = 50;
	public Joystick joystick;

	float horizontalInput;
	float verticalInput;
	float steeringAngle;

	float angle_rotate;

	public void GetInput() {
		horizontalInput = joystick.Horizontal();
		verticalInput = joystick.Vertical();
	}

	public void Steer() {
		steeringAngle = maxSteerAngle*horizontalInput;
		fLWheel.steerAngle = steeringAngle;
		fRWheel.steerAngle = steeringAngle;
	}

	public void Accelerate() {
		rLWheel.motorTorque = verticalInput*motorForce;
		rRWheel.motorTorque = verticalInput*motorForce;
	}

	public void UpdateWheelPoses() {
		UpdateWheelPose(fLWheel, fLTransform);
		UpdateWheelPose(fRWheel, fRTransform);
		UpdateWheelPose(rLWheel, rLTransform);
		UpdateWheelPose(rRWheel, rRTransform);
	}

	public void UpdateWheelPose(WheelCollider wheelCollider, Transform transform) {
		Vector3 pos = transform.position;
		Quaternion quat = transform.rotation;
		wheelCollider.GetWorldPose(out pos, out quat);
		transform.position = pos;
		transform.rotation = quat;
	}

	private void FixedUpdate()
	 {
		if(Mathf.Abs(joystick.Vertical()) > 0.05f || Mathf.Abs(joystick.Horizontal()) > 0.05f)
		{
            GetInput();
			Steer();
			Accelerate();
			UpdateWheelPoses();
		}
		
		

		if(Mathf.Abs(joystick.Vertical()) > 0.1f)
		{
			angle_rotate += 1000 * joystick.Vertical() * Time.deltaTime;
		    fLTransform.Rotate (angle_rotate, 0.0f, 0.0f);
			fRTransform.Rotate (angle_rotate, 0.0f, 0.0f);
			rLTransform.Rotate (angle_rotate, 0.0f, 0.0f);
			rRTransform.Rotate (angle_rotate, 0.0f, 0.0f);
		}

        
	}
}
