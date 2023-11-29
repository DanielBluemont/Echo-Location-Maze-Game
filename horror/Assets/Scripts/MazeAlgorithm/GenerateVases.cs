using System.Collections.Generic;
using System.Linq;
using MazeGame.GamePlayObjects;
using UnityEngine;

namespace MazeGame.MazeAlgorithm
{
    public class GenerateVases:MonoBehaviour
    {
        [SerializeField] private GameObject Locker;
        [Space]
        [SerializeField] private CodeInVase[] vases = new CodeInVase[4];
        string code;
        private int quantity;
     
        int Width, Height;
        int[] digits = new int[4];
        int[] order = {0, 1, 2, 3};
        CodeInVase currentVase;
        public void PlaceVases(Maze maze,List<Cell>[] cList, int w, int h)
        {

            int[] usedX = {};
            int[] usedY = {};
            Width = w;
            Height = h;
            quantity = (int)(Width*Height * 0.05);
            for (int j = 0; j < 4; j++)
            {
                int num = quantity;
                while (num > 0)
                {
                    int x = Random.Range(0, Width-1);
                    int y = Random.Range(0, Height-1);
                    if (usedX.Contains(x) && usedY.Contains(y)) continue;
                    int pos = Random.Range(0, 3);
                
                    for (int i = 0; i<=pos; i++)
                    {
                        currentVase = Instantiate(vases[j],cList[x][y].Floor.GetComponent<VasePos>().transforms[i].position, Quaternion.Euler(-90, 0,0));
                        num--; 
                    }
                }
                int digit = Random.Range(1,10);
                digits[j] = digit;
                currentVase.SetCode(digit);
            }
            ShuffleArraysInSync(digits, order);
            Instantiate(Locker, new Vector3(((Width/2 -1) *5)-2.5f, 1.14f, ((Height/2 -1) *5)-2.5f), Quaternion.identity).GetComponentInChildren<KeypadLock>().InitializeCode(digits, order);
            See();
        }
        private void See()
        {
            Debug.Log(string.Join(",", digits.Select(i => i.ToString())));
            Debug.Log(string.Join("", order));
        }
        private void ShuffleArraysInSync(int[] array1, int[] array2)
        {
            System.Random random = new System.Random();

            int n = array1.Length;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);

                int value1 = array1[k];
                array1[k] = array1[n];
                array1[n] = value1;

                int value2 = array2[k];
                array2[k] = array2[n];
                array2[n] = value2;
            }
        }
    }
}
