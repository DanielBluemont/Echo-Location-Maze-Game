using System.Collections.Generic;
using MazeGame.EnemyAI;
using UnityEngine;
using UnityEngine.AI;

namespace MazeGame.MazeAlgorithm
{
    public class MazeSpawner : MonoBehaviour
    {
        [SerializeField] private Cell CellPrefab;
        [SerializeField] private GameObject Player, Monster,Exit, Ps;
        [SerializeField] private Vector3 CellSize = new Vector3(1,1,0);
        [SerializeField] private Transform Maze;
        [SerializeField] private int Width, Height;
        [Space]
        [SerializeField] private NavMeshSurface surface; 
        private List<Cell>[] cellList;
        private Maze maze;


        private void Start()
        {
            GenerateVases vases = this.GetComponent<GenerateVases>();
            cellList = new List<Cell>[Width];
            MazeGenerator generator = new MazeGenerator();
            maze = generator.GenerateMaze(Width, Height);
            for (int x = 0; x < maze.cells.GetLength(0); x++)
            {
                cellList[x] = new List<Cell>();
                for (int y = 0; y < maze.cells.GetLength(1); y++)
                {
                    Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);

                    if (!maze.cells[x, y].WallLeft)
                        Destroy(c.WallLeft);
                    if (!maze.cells[x, y].WallBottom)
                        Destroy(c.WallBottom);
                    if (!maze.cells[x, y].Floor)
                        Destroy(c.Floor);
                    if (!maze.cells[x, y].Celling)
                        Destroy(c.Celling);
                    c.transform.parent = Maze;
                    cellList[x].Add(c);
                }
            }
            vases.PlaceVases(maze,cellList, Width, Height);
            surface.BuildNavMesh();
            SpawnExitAndPlayer((new Vector3(maze.finishPosition.x, 0, maze.finishPosition.y)));
            Instantiate(Ps);
        }
    
    
        private void SpawnExitAndPlayer(Vector3 p)
        {
            GameObject m;
            if (p.x <= 0)
            {
                m = Instantiate(Monster, new Vector3(2.5f, 2.5f, p.z *5), Quaternion.identity);
                Instantiate(Player, new Vector3(((Width - 1) * 5) -2.5f, 1f, 2.5f), Quaternion.identity);
                GameObject cell = Instantiate(Exit, p*5, Quaternion.identity);
                Destroy(cell.GetComponent<Cell>().WallBottom);
            }
            else
            {
                m = Instantiate(Monster, new Vector3(p.x * 5, 2.5f, -2.5f), Quaternion.identity);
                Instantiate(Player, new Vector3(2.5f, 2f, ((Height - 1) * 5)-2.5f), Quaternion.identity);
                GameObject cell = Instantiate(Exit, p*5, Quaternion.identity);
                Destroy(cell.GetComponent<Cell>().WallLeft);
            }
            m.GetComponent<MonsterListener>().InformAboutMapSize(Width);
        }
    
    }
}