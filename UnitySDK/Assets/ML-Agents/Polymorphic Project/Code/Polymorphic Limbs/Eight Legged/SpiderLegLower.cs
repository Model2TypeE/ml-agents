using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegLower : Polymorphism.PolymorphicJointedLimb
{
	public override int ObsSize
	{
		get
		{
			return 14;
		}
	}

	public override int ActSize
	{
		get
		{
			return 2;
		}
	}

	public override void CollectLimbObs(List<float> observations)
	{
		AddObservation(observations, grounded);

		Vector3 localPosRelToCenter = agent.pivotRgb.transform.InverseTransformPoint(rgb.position);
		AddObservation(observations, localPosRelToCenter);
		AddObservation(observations, rgb.velocity);
		AddObservation(observations, rgb.angularVelocity);
		AddObservation(observations, currentNormalizedRotation);
		observations.Add(currentJointForce);
	}

	public override void FeedActions(float[] actions, int startIndex)
	{
		SetJointProperties(actions[startIndex], 0f, 0f, actions[startIndex + 1]);
	}
}
