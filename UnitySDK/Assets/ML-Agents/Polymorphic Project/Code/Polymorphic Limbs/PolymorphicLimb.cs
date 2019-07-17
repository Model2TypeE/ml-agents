using System;
using System.Collections.Generic;
using UnityEngine;

namespace Polymorphism
{
	[RequireComponent(typeof(Rigidbody))]
	public abstract class PolymorphicLimb : MonoBehaviour
	{
		public abstract int ObsSize { get; }
		public abstract int ActSize { get; }

		public bool grounded;
		protected Rigidbody rgb;
		protected Vector3 startingPos;
		protected Quaternion startingRot;

		private const string ground = "ground";

		protected virtual void Awake()
		{
			rgb = GetComponent<Rigidbody>();
			startingPos = transform.position;
			startingRot = transform.rotation;
		}

		public abstract void CollectLimbObs(List<float> observations);

		public abstract void FeedActions(float[] actions, int startIndex);

		public virtual void OnAgentDone()
		{
			transform.position = startingPos;
			transform.rotation = startingRot;
		}

		protected virtual void OnCollisionEnter(Collision collision)
		{
			if (collision.collider.CompareTag(ground))
			{
				grounded = true;
			}
		}

		protected virtual void OnCollisionExit(Collision collision)
		{
			if (collision.collider.CompareTag(ground))
			{
				grounded = false;
			}
		}

		protected void AddObservation(List<float> observations, Quaternion quaternion)
		{
			observations.Add(quaternion.x);
			observations.Add(quaternion.y);
			observations.Add(quaternion.z);
			observations.Add(quaternion.w);
		}
		protected void AddObservation(List<float> observations, Vector3 vector)
		{
			observations.Add(vector.x);
			observations.Add(vector.y);
			observations.Add(vector.z);
		}
		protected void AddObservation(List<float> observations, bool boolean)
		{
			observations.Add(boolean ? 1f : 0f);
		}
	}

	[RequireComponent(typeof(ConfigurableJoint))]
	public abstract class PolymorphicJointedLimb : PolymorphicLimb
	{
		protected ConfigurableJoint joint;
		protected Vector3 currentNormalizedRotation;
		protected float currentJointForce;
		protected float maxJointForceLimit;

		protected override void Awake()
		{
			base.Awake();
			joint = GetComponent<ConfigurableJoint>();
			maxJointForceLimit = joint.slerpDrive.maximumForce;
			currentJointForce = maxJointForceLimit;
		}

		protected void SetJointProperties(float x, float y, float z, float force)
		{
			// convert -1 -> 1 value range to 0 -> 1
			x = (x + 1f) * .5f;
			y = (y + 1f) * .5f;
			z = (z + 1f) * .5f;
			currentNormalizedRotation = new Vector3(x, y, z);

			// normalized rotations applied to min and max rotation range
			// (rot = 0: low angular limit, rot = 1: high angular limit)
			var xRot = Mathf.Lerp(joint.lowAngularXLimit.limit, joint.highAngularXLimit.limit, x);
			var yRot = Mathf.Lerp(-joint.angularYLimit.limit, joint.angularYLimit.limit, y);
			var zRot = Mathf.Lerp(-joint.angularZLimit.limit, joint.angularZLimit.limit, z);

			joint.targetRotation = Quaternion.Euler(xRot, yRot, zRot);

			// set joint slerp drive force
			currentJointForce = (force + 1f) * 0.5f * maxJointForceLimit;

			var slerpDrive = joint.slerpDrive;
			slerpDrive.maximumForce = currentJointForce;
			joint.slerpDrive = slerpDrive;
		}

		public override void OnAgentDone()
		{
			base.OnAgentDone();

			// reset target rotation
			joint.targetRotation = Quaternion.identity;

			// reset joint strength
			currentJointForce = maxJointForceLimit;

			var slerpDrive = joint.slerpDrive;
			slerpDrive.maximumForce = maxJointForceLimit;
			joint.slerpDrive = slerpDrive;
		}
	}
}
