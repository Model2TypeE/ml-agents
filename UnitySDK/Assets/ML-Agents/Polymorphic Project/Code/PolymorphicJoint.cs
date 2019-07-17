using UnityEngine;

namespace Polymorphism
{
	public enum BodyPart { Standard, Head, Foot }
	public class PolymorphicJoint : MonoBehaviour
	{
		[SerializeField] internal BodyPart bodyPart;
		[SerializeField] internal bool canTouchGround;
		[SerializeField] internal bool xMobile, yMobile, zMobile;

		internal Quaternion currentRotation;
		internal bool grounded;

		private ConfigurableJoint joint;
		private Rigidbody rgb;
		private Transform connectedTransform;

		private Quaternion startRotation;
		private Vector3 startPos;
		private float startStrength;

		private void Awake()
		{
			joint = GetComponent<ConfigurableJoint>();
			rgb = GetComponent<Rigidbody>();
			connectedTransform = joint.connectedBody.transform;

			startPos = transform.position;
			startRotation = transform.rotation;
			startStrength = joint.slerpDrive.maximumForce;
		}

		private void Update()
		{
			currentRotation = transform.localRotation * Quaternion.Inverse(connectedTransform.localRotation);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (!canTouchGround && collision.gameObject.CompareTag("ground"))
			{
				grounded = true;
			}
		}

		private void OnCollisionExit(Collision collision)
		{
			if (!canTouchGround && collision.gameObject.CompareTag("ground"))
			{
				grounded = false;
			}
		}

		public void SetJointProperties(float xVal, float yVal, float zVal, float strength)
		{
			Vector3 targetRotation = joint.targetRotation.eulerAngles;
			if (xMobile) targetRotation.x = Mathf.Lerp
				 (joint.lowAngularXLimit.limit,
				 joint.highAngularXLimit.limit,
				 (xVal + 1f) * .5f); // DOCUMENT INTERPOLATION

			if (yMobile) targetRotation.y = Mathf.Lerp
				 (-joint.angularYLimit.limit,
				 joint.angularYLimit.limit,
				 (yVal + 1f) * .5f); // DOCUMENT INTERPOLATION

			if (zMobile) targetRotation.z = Mathf.Lerp
				 (-joint.angularZLimit.limit,
				 joint.angularZLimit.limit,
				 (zVal + 1f) * .5f); // DOCUMENT INTERPOLATION

			joint.targetRotation = Quaternion.Euler(targetRotation);
			var drive = joint.slerpDrive;

			drive.maximumForce = Mathf.Lerp(0, startStrength, (strength + 1) * .5f);
		}

		public void ResetJoint()
		{
			transform.position = startPos;
			transform.rotation = startRotation;

			joint.targetRotation = Quaternion.identity;

			rgb.velocity = Vector3.zero;
			rgb.angularVelocity = Vector3.zero;

			grounded = false;
		}
	}
}