using System;
using System.Collections.Generic;
using UnityEngine;

namespace Polymorphism
{
	[RequireComponent(typeof(Collider))]
	public class ContactRewarder : MonoBehaviour
	{
		[SerializeField] private PolymorphicAgent agent;
		[SerializeField] private RewardTuple[] contactRewards; // for editor accessability
		private Dictionary<string, RewardTuple> contactRewardTable; // for runtime use

		protected virtual void Awake()
		{
			// retrieve agent reference if not set in editor
			if (agent == null) agent = GetComponentInParent<PolymorphicAgent>();

			// build dictionary out of contact rewards
			contactRewardTable = new Dictionary<string, RewardTuple>();
			for (int i = 0; i < contactRewards.Length; i++)
			{
				contactRewardTable.Add
					(contactRewards[i].CollisionTag,
					contactRewards[i]);
			}
		}

		protected virtual void OnCollisionEnter(Collision collision)
		{
			RewardTuple reward; // cant inline in C# 6.0
			if (contactRewardTable.TryGetValue(collision.gameObject.tag, out reward))
			{
				// set or rewards on agent and toggle isDone flag to true if requested
				if (reward.ResetAgentOnContact)
				{
					agent.SetReward(reward.RewardValue);
					agent.Done();
				}
				else
				{
					agent.AddReward(reward.RewardValue);
				}
			}
		}
	}

	[Serializable]
	public struct RewardTuple
	{
		public string CollisionTag;
		[Range(-1, 1)] public float RewardValue;
		public bool ResetAgentOnContact;
	}
}
