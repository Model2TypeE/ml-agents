using UnityEngine;

public class JointTester : MonoBehaviour
{
	[SerializeField] ConfigurableJoint jointConfig;

	[SerializeField] private Vector3 currentRotation;

	private void Update()
	{
		float dt = Time.deltaTime;
		if (Input.GetKey(KeyCode.W))
		{
			currentRotation.x += 90f * dt;
			currentRotation.x = Mathf.Clamp
				(currentRotation.x, jointConfig.lowAngularXLimit.limit, 
				jointConfig.highAngularXLimit.limit);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			currentRotation.x -= 90f * dt;
			currentRotation.x = Mathf.Clamp
				(currentRotation.x, jointConfig.lowAngularXLimit.limit,
				jointConfig.highAngularXLimit.limit);
		}

		if (Input.GetKey(KeyCode.A))
		{
			currentRotation.z += 90f * dt;
			currentRotation.z = Mathf.Clamp
				(currentRotation.z, -jointConfig.angularZLimit.limit,
				jointConfig.angularZLimit.limit);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			currentRotation.z -= 90f * dt;
			currentRotation.z = Mathf.Clamp
				(currentRotation.z, -jointConfig.angularZLimit.limit,
				jointConfig.angularZLimit.limit);
		}


		jointConfig.targetRotation = Quaternion.Euler(currentRotation);
		//Debug.Log(jointConfig.targetRotation.eulerAngles);
	}
}
