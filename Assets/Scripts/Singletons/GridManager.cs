using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonBrickStudios
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Grid grid;
        public static GridManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
            SceneManager.sceneLoaded += RefreshGrid;
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

        private void RefreshGrid(Scene scene, LoadSceneMode loadSceneMode)
        {
            grid.RefreshGrid();
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= RefreshGrid;
        }
    }
}
