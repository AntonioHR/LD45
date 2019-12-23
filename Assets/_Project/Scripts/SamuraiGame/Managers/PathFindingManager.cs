
using System;
using UnityEngine;
using Pathfinding;

namespace SamuraiGame.Managers
{
    public class PathFindingManager : MonoBehaviour {
        [SerializeField]
        private Vector2Int bgSize = Vector2Int.zero;
        [SerializeField]
        private float nodeSize = 0.2f;
        void Start() {
            // CreatePath();

            AstarData data = AstarPath.active.data;

            GridGraph gg = data.gridGraph;

            // Setup a grid graph with some values

            Vector2Int size = GetGridSize();

            gg.center = new Vector3(0, 0, 0);

            // Updates internal size from the above values
            gg.SetDimensions(size.x, size.y, nodeSize);

            // Scans all graphs
            AstarPath.active.Scan();

            gg.collision.collisionCheck = true;
            gg.collision.diameter = 0.2f;
        }

        private Vector2Int GetGridSize()
        {
            Vector2Int size = new Vector2Int(200, 100);
            if(bgSize.x > 0.01f) {
                size.x = bgSize.x;
            }
            if(bgSize.y > 0.01f) {
                size.y = bgSize.y;
            }
            return size;
        }

        private void CreatePath()
        {
            GameObject pathGraph = new GameObject("GraphPath");
            pathGraph.AddComponent<AstarPath>();
        }
    }
}