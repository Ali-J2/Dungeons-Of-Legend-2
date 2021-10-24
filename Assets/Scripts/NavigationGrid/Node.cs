// Author: Ali Jiraki

using UnityEngine;

namespace DungeonBrickStudios
{
	public class Node
	{
		public bool walkable, traversed;
		public Vector3 flatGridPosition;
		public Vector3 actualPositionOnFloor;

		public Node(bool walkable, Vector3 worldPosition, Vector3 actualPositionOnFloor)
		{
			this.walkable = walkable;
			this.flatGridPosition = worldPosition;
			this.actualPositionOnFloor = actualPositionOnFloor;
		}

		public void SetWalkable(bool walkable)
		{
			this.walkable = walkable;
		}
	}
}