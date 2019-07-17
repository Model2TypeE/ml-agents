using UnityEngine;

namespace Polymorphism
{
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


			jointConfig.targetRotation = Quaternion.Euler(currentRotation);
			//Debug.Log(jointConfig.targetRotation.eulerAngles);

			//Debug.Log((transOne.localRotation * Quaternion.Inverse(transTwo.localRotation)));
		}
	}
}
