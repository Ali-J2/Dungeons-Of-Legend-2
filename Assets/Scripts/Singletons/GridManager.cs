using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonBrickStudios
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Grid grid;
        public static GridManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        public Vector3 GetFloorPosition(Vector3 objectPosition)
        {
            return grid.NodeFromWorldPoint(objectPosition).actualPositionOnFloor;
        }
        public bool PositionIsWalkable(Vector3 pos)
        {
            return grid.NodeFromWorldPoint(pos).walkable;
        }
        public void SetGridPositionWalkable(Vector3 pos, bool walkable)
        {
            grid.NodeFromWorldPoint(pos).SetWalkable(walkable);
        }
    }
}
