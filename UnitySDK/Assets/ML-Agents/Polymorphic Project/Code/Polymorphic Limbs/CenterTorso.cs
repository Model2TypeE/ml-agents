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
				return 10;
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
			AddObservation(observations, rgb.velocity);
			AddObservation(observations, rgb.angularVelocity);
		}

		public override void FeedActions(float[] actions, int startIndex)
		{ }

		public override void OnAgentDone()
		{
			base.OnAgentDone();
		}
	}
}
