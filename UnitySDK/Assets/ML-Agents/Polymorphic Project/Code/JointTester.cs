using UnityEngine;

public class JointTester : MonoBehaviour
{
	private float targetVelocity = 0f;
	public float TargetVelocity
	{
		get { return targetVelocity; }
		set
		{
			targetVelocity = value;
			motor.targetVelocity = value;
			joint.motor = motor;
		}
	}

	[SerializeField] HingeJoint joint;
	[SerializeField] float velocityChange;

	private JointMotor motor;

	private void Awake()
	{
		motor = joint.motor;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.A)) TargetVelocity = -velocityChange;
		
		else if (Input.GetKey(KeyCode.D)) TargetVelocity = velocityChange;
		else TargetVelocity = 0f;
	}
}
