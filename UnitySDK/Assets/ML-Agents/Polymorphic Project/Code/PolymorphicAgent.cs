using MLAgents;
using System.Collections.Generic;
using UnityEngine;

namespace Polymorphism
{
	public class PolymorphicAgent : Agent
	{
		public Rigidbody pivotRgb;
		public Transform goal;

		private List<PolymorphicLimb> limbs;
		private List<float> observations;
		internal Vector3 goalDir;

		private bool isNewDecisionStep;
		private int currentDecisionStep;

		public override void InitializeAgent()
		{
			limbs = new List<PolymorphicLimb>();
			GetComponentsInChildren(false, limbs);
			int obsSize = 0, actSize = 0;
			for (int i = 0; i < limbs.Count; i++)
			{
				obsSize += limbs[i].ObsSize;
				actSize += limbs[i].ActSize;
				limbs[i].agent = this;
			}

			Debug.Assert(obsSize == brain.brainParameters.vectorObservationSize,
				"Total limb observation size of " + obsSize + " is unequal to brain parameters: " + brain.brainParameters.vectorObservationSize);
			Debug.Assert(actSize == brain.brainParameters.vectorActionSize[0],
				"Total limb action size of " + actSize + " is unequal to brain parameters: " + brain.brainParameters.vectorActionSize[0]);

			observations = new List<float>(obsSize);
		}

		public override void CollectObservations()
		{
			observations.Clear();
			for(int i = 0; i < limbs.Count; i++)
			{
				limbs[i].CollectLimbObs(observations);
			}

			AddVectorObs(observations);
		}

		public override void AgentAction(float[] vectorAction, string textAction)
		{
			if (isNewDecisionStep)
			{
				int currentIndex = 0;
				for (int i = 0; i < limbs.Count; i++)
				{
					limbs[i].FeedActions(vectorAction, currentIndex);
					currentIndex += limbs[i].ActSize;
				}
			}
			IncrementDecisionTimer();

			goalDir = goal.position - pivotRgb.position;
			AddReward(
				+ 0.03f * Vector3.Dot(goalDir.normalized, pivotRgb.velocity) // velocity alignment
				+ 0.05f * Vector3.Dot(goalDir.normalized, pivotRgb.transform.forward) // rotational alignment
			);
		}

		public override void AgentReset()
		{
			for (int i = 0; i < limbs.Count; i++)
			{
				limbs[i].OnAgentDone();
			}
		}

		public void IncrementDecisionTimer()
		{
			if (currentDecisionStep == agentParameters.numberOfActionsBetweenDecisions ||
				agentParameters.numberOfActionsBetweenDecisions == 1)
			{
				currentDecisionStep = 1;
				isNewDecisionStep = true;
			}
			else
			{
				currentDecisionStep++;
				isNewDecisionStep = false;
			}
		}
	}
}
