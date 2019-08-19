using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGoalTrigger : MonoBehaviour
{
	[SerializeField] private Polymorphism.PolymorphicAgent agent;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("goal")) agent.OnGoalTouch();
	}
}
