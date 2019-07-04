using MLAgents;
using UnityEngine;

public class PolymorphicAcademy : Academy
{
	public override void InitializeAcademy()
	{
		Physics.defaultSolverIterations = 12;
		Physics.defaultSolverVelocityIterations = 12;
	}

	public override void AcademyReset()
	{

	}

	public override void AcademyStep()
	{

	}
}
