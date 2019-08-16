using System.Collections.Generic;
using UnityEngine;

namespace Polymorphism
{
	public class Foot : PolymorphicLimb
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

			Vector3 localPosRelToCenter = agent.pivotRgb.transform.InverseTransformPoint(rgb.position);
			AddObservation(observations, localPosRelToCenter);
			AddObservation(observations, rgb.velocity);
			AddObservation(observations, rgb.angularVelocity);
		}

		public override void FeedActions(float[] actions, int startIndex)
		{

		}
	}
}
