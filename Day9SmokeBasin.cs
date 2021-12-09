using UnityEngine;
using System;
using System.Linq;
using Sirenix.OdinInspector;

[Button]
void Start()
{
    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    stopwatch.Start();

    print($"method {nameof(Day09)} returns {Day09()} as answer");

    stopwatch.Stop();

    // runs in ~10 ms
    print($"operation took {stopwatch.ElapsedMilliseconds} milliseconds"); 
}

string Day09()
{
    var path = Application.dataPath + "/AdventOfCode/09.txt";
    string[] data = System.IO.File.ReadAllLines(path);
    int sizeX, sizeY;
    sizeX = data[0].Length;
    sizeY = data.Length;

    int[,] iMultiDim = new int[sizeY, sizeX];
    // int[][] iJagged = new int[sizeY][];

    // depressing name :D
    var retValTotalOfLowPoints = 0;

    // init
    for (int i = 0; i < sizeY; i++)
    {
        for (int j = 0; j < sizeX; j++)
        {
            iMultiDim[i, j] = int.Parse(data[i][j].ToString());
        }
    }


    // calculate and add low points
    for (int y = 0; y < sizeY; y++)
    {
        for (int x = 0; x < sizeX; x++)
        {
            var cellVal = iMultiDim[y, x];

            // value 9 can't be lowest number
            if (cellVal < 9)
            {
                if (LowestOfFourNeighbors(y, x, cellVal))
                {
                    retValTotalOfLowPoints += 1 + cellVal;
                }
            }
        }
    }

    // helper to avoid bloat in for loop
    bool LowestOfFourNeighbors(int y, int x, int cellValue)
    {
        // left
        if (x > 0)
        {
            if (iMultiDim[y, x -1] <= cellValue)
                return false;
        }
        // right
        if (x < sizeX -1)
        {
            if (iMultiDim[y, x + 1] <= cellValue)
                return false;
        }
        // up
        if (y > 0)
        {
            if (iMultiDim[y - 1, x] <= cellValue)
                return false;
        }
        // down
        if (y < sizeY -1)
        {
            if (iMultiDim[y + 1, x] <= cellValue)
                return false;
        }

        return true;
    }

    return retValTotalOfLowPoints.ToString();
}
