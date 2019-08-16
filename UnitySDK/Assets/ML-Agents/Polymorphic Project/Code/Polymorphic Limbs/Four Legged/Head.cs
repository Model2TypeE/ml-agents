﻿using Polymorphism;
using System.Collections.Generic;

public class Head : PolymorphicLimb
{
	public override int ObsSize
	{
		get
		{
			return 16;
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
	}

	public override void FeedActions(float[] actions, int startIndex)
	{

	}
}
