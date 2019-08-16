using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polymorphism
{
	public class CenterTorso : PolymorphicLimb
	{
		public override int ObsSize
		{
			get
			{
				return 19;
			}
		}

		public override int ActSize
		{
			get
			{
				return 0;
			}
		}

		public override void CollectLimbObs(List<float> observations)
		{
			AddObservation(observations, grounded);
			AddObservation(observations, rgb.position);
			AddObservation(observations, transform.forward);
			AddObservation(observations, transform.up);
			AddObservation(observations, rgb.velocity);
			AddObservation(observations, rgb.angularVelocity);


			AddObservation(observations, agent.goalDir.normalized);
		}

		public override void FeedActions(float[] actions, int startIndex)
		{

		}
	}
}
