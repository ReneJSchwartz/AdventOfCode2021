using UnityEngine;
using System;
using System.Linq;
using Sirenix.OdinInspector;

[Button]
void Start()
{
    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    stopwatch.Start();

    print($"method {nameof(AdventOfCode06Lanternfish)} returns {AdventOfCode06Lanternfish(days: 80)} as answer");

    stopwatch.Stop();

    print($"operation took {stopwatch.ElapsedMilliseconds} milliseconds"); 
}


// runtime for first problem is ~29 ms
// https://adventofcode.com/2021/day/6
string AdventOfCode06Lanternfish(bool firstPart = true, int days = 0)
{
    var path = Application.dataPath + "/AdventOfCode/06.txt";
    string data = System.IO.File.ReadAllText(path);

    string[] fishListStr = data.Split(',');

    // for use with odin inspector button etc
    var howManyDays = days == 0 ? 18 : days;

    sbyte[] fishArr = new sbyte[370000];
    var fishCount = fishListStr.Length;
    var newFishes = 0;

    for (int i = 0; i < fishArr.Length; i++)
    {
        fishArr[i] = 9;
    }

    for (int i = 0; i < fishListStr.Length; i++)
    {
        fishArr[i] = sbyte.Parse(fishListStr[i]);
    }

    for (int i = 0; i < howManyDays; i++)
    {
        fishCount += newFishes;
        newFishes = 0;

        for (int j = 0; j < fishCount; j++)
        {
            // reduce timer
            fishArr[j]--;
            
            // spawn fish
            if (fishArr[j] == 0)
            {
                // at next day the value will be 6 (7 - 1)
                fishArr[j] = 7;
                // do not spawn right away
                newFishes++;
            }
        }
    }

    return fishCount.ToString();
}
