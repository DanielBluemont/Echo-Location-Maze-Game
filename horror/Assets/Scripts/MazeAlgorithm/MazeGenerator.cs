using System.Collections.Generic;
using UnityEngine;

namespace MazeGame.MazeAlgorithm
{
    public class MazeGenerator
    {
        private int Width;
        private int Height;
        public Maze GenerateMaze(int width, int height)
        {
            Width = width;
            Height = height;
            MazeGeneratorCell[,] cells = new MazeGeneratorCell[Width, Height];

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y] = new MazeGeneratorCell {X = x, Y = y};
                }
            }
            /*
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x, Height - 1].WallLeft = false;
            cells[x, Height - 1].Floor = false;
            cells[x, Height - 1].Celling = false;
        }

        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[Width - 1, y].WallBottom = false;
            cells[Width - 1, y].Floor = false;
            cells[Width - 1, y].Celling = false;
        }*/

            RemoveWallsWithBacktracker(cells);

            Maze maze = new Maze();

            maze.cells = cells;
            maze.finishPosition = PlaceMazeExit(cells);
            maze.cells = RemoveRandomWalls(cells);
            return maze;
        }
        private MazeGeneratorCell[,] RemoveRandomWalls(MazeGeneratorCell[,] cells)
        {
            for (int i = 0; i< (Height * Width)*0.7; i++)
            {
                int ranX = UnityEngine.Random.Range(1, Width-1);
                int ranY = UnityEngine.Random.Range(1, Height-1);
                int side = UnityEngine.Random.Range(0, 2);
                if (side == 0)
                {
                    cells[ranX, ranY].WallLeft = false;
                } 
                else
                {
                    cells[ranX, ranY].WallBottom = false;
                }
            }
            return cells;
        }
        private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
        {
            MazeGeneratorCell current = maze[0, 0];
            current.Visited = true;
            current.DistanceFromStart = 0;

            Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
            do
            {
                List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

                int x = current.X;
                int y = current.Y;

                if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
                if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
                if (x < Width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
                if (y < Height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

                if (unvisitedNeighbours.Count > 0)
                {
                    MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                    RemoveWall(current, chosen);

                    chosen.Visited = true;
                    stack.Push(chosen);
                    chosen.DistanceFromStart = current.DistanceFromStart + 1;
                    current = chosen;
                }
                else
                {
                    current = stack.Pop();
                }
            } while (stack.Count > 0);
        }

        private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
        {
            if (a.X == b.X)
            {
                if (a.Y > b.Y) a.WallBottom = false;
                else b.WallBottom = false;
            }
            else
            {
                if (a.X > b.X) a.WallLeft = false;
                else b.WallLeft = false;
            }
        }

        private Vector2Int PlaceMazeExit(MazeGeneratorCell[,] maze)
        {
            /*
        MazeGeneratorCell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, Height - 1];
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[Width - 1, y];
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
        }

        if (furthest.X == 0)
        {
            furthest.WallLeft = false;
        }
        else if (furthest.Y == 0) 
        {
            furthest.WallBottom = false;
        }
        else if (furthest.X == Width - 2) 
        {
            maze[furthest.X + 1, furthest.Y].WallLeft = false;
        }
        else if (furthest.Y == Height - 2) 
        {
            maze[furthest.X, furthest.Y + 1].WallBottom = false;
        }
        return new Vector2Int(furthest.X, furthest.Y);
        */
            System.Random random = new System.Random();
            int x = random.Next(0,Width-1);
            int y = random.Next(0,Height-1);
            if (x > y)
            {
                y = 0;
                maze[x, y].WallBottom = false;
            }
            else
            {
                x = 0;
                maze[x, y].WallLeft = false;
            }

            return new Vector2Int(x, y);
        }

    }
}