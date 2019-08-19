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
			RaycastHit hit;
			if(Physics.Raycast(transform.position, Vector3.down, out hit, 5f))
			{
				observations.Add(Vector3.Distance(transform.position, hit.point));
			}
			else
			{
				observations.Add(5f);
			}

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
