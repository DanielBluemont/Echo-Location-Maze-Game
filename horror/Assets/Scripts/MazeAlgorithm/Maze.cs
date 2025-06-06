using UnityEngine;

namespace MazeGame.MazeAlgorithm
{
    public class Maze
    {
        public MazeGeneratorCell[,] cells;
        public Vector2Int finishPosition;
    }

    public class MazeGeneratorCell
    {
        public int X;
        public int Y;

        public bool WallLeft = true;
        public bool WallBottom = true;
        public bool Floor = true;
        public bool Celling = true;

        public bool Visited = false;
        public int DistanceFromStart;
    }
}