using System;
using System.Collections.Generic;
using UnityEngine;

namespace Polymorphism
{
	public class StankyLeg : PolymorphicJointedLimb
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
			AddObservation(observations, rgb.position);
			AddObservation(observations, rgb.velocity);
			AddObservation(observations, rgb.angularVelocity);
			AddObservation(observations, currentNormalizedRotation);
			observations.Add(currentJointForce);
		}

		public override void FeedActions(float[] actions, int startIndex)
		{
			SetJointProperties(actions[startIndex], 0f, 0f, actions[startIndex + 1]);
		}

		public override void OnAgentDone()
		{
			base.OnAgentDone();
		}
	}
}
