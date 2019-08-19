using UnityEngine;

public class DirectionalGoal : MonoBehaviour
{
	[SerializeField] private Transform[] goalPositions;
	private int currentPosIndex;

	public void ResetGoal()
	{
		return;

		Debug.Assert(goalPositions.Length > 1);

		currentPosIndex = Random.Range(0, goalPositions.Length);

		// ensure that new pos isnt the old pos
		int num = currentPosIndex;
		while (num == currentPosIndex)
		{
			num = Random.Range(0, goalPositions.Length);
		}

		transform.position = goalPositions[currentPosIndex].position;
	}
}
