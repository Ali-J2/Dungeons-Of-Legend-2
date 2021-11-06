using System;
using UnityEngine;

namespace DungeonBrickStudios
{
	public class Grid : MonoBehaviour
	{

		[SerializeField] private LayerMask unwalkableMask;
		[SerializeField] private LayerMask environmentMask;
		[SerializeField] private Vector2 gridWorldSize;
		[SerializeField] private float nodeRadius;
		[SerializeField] private Node[,] grid;

		private float nodeDiameter;
		private int gridSizeX, gridSizeY;

		private LayerMask layerMask;

		// Use this for initialization
		private void Start()
		{
			nodeDiameter = nodeRadius * 2f;
			gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
			gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
			layerMask = LayerMask.GetMask("Floor");
			CreateGrid();
		}

		private void CreateGrid()
		{
			grid = new Node[gridSizeX, gridSizeY];
			Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
					Vector3 gridPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
					Vector3 floorPoint = GetWorldPointAtFloorHeight(gridPoint);
					bool walkable = !(Physics.CheckSphere(floorPoint, nodeRadius - 0.1f, unwalkableMask));
					grid[x, y] = new Node(walkable, gridPoint, floorPoint);
				}
			}
		}

		public Node NodeFromWorldPoint(Vector3 worldPostion)
		{
			float percentX = (worldPostion.x + transform.position.x + gridWorldSize.x / 2) / gridWorldSize.x;
			float percentY = (worldPostion.z + transform.position.z + gridWorldSize.y / 2) / gridWorldSize.y;
			percentX = Mathf.Clamp01(percentX);
			percentY = Mathf.Clamp01(percentY);

			int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
			int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
			return grid[x, y];
		}

		private Vector3 GetWorldPointAtFloorHeight (Vector3 worldPoint)
        {
			Vector3 raycastStartPosition = worldPoint + (Vector3.up * 1000);
			bool hit = Physics.Raycast(raycastStartPosition, Vector3.down, out RaycastHit hitInfo, 2000, layerMask, QueryTriggerInteraction.Collide);

			if (hit)
				return hitInfo.point;
			else
				return worldPoint;
        }

		public bool IsWalkable(int x, int y)
		{
			if (x < 0 || x >= gridSizeX)
				return false;
			else if (y < 0 || y >= gridSizeY)
				return false;
			else
				return grid[x, y].walkable;
		}

		public void RefreshGrid()
        {
			Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

			for (int x = 0; x < gridSizeX; x++)
			{
				for (int y = 0; y < gridSizeY; y++)
				{
					Vector3 gridPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
					Vector3 floorPoint = GetWorldPointAtFloorHeight(gridPoint);
					bool walkable = !(Physics.CheckSphere(floorPoint, nodeRadius - 0.1f, environmentMask));

					grid[x, y].flatGridPosition = gridPoint;
					grid[x, y].actualPositionOnFloor = floorPoint;

					// If Node is unwalkable for whatever reason already, do nothing. Otherwise update it with the new environment data
					if (!grid[x, y].walkable)
						return;

					grid[x, y].walkable = walkable;
				}
			}
		}

		void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

			if (grid != null)
			{
				foreach (Node n in grid)
				{
					Gizmos.color = (n.walkable) ? Color.white : Color.red;
					Gizmos.DrawCube(n.flatGridPosition, Vector3.one * (nodeDiameter - 0.1f));
				}
			}
		}
	}
}
