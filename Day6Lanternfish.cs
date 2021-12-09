using UnityEngine;
using System;
using System.Linq;
using Sirenix.OdinInspector;

[Button]
void Start(int paramDays)
{
    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    stopwatch.Start();


    print($"method {nameof(LanternfishByDayPooling)} returns {LanternfishByDayPooling(days: paramDays)} as answer");

    stopwatch.Stop();

    print($"operation took {stopwatch.Elapsed}"); 
}

// runs in about 1ms in unity and doesn't use mem much
string LanternfishByDayPooling(int days)
{
    var path = Application.dataPath + "/AdventOfCode/06.txt";
    string data = System.IO.File.ReadAllText(path);

    string[] fishListStr = data.Split(',');
    ulong[] fish = Array.ConvertAll(fishListStr, s => ulong.Parse(s));
    ulong[] fishArr = new ulong[10];

    ulong totalFishCount = 0;
    
    // populate starting fishes
    for (int i = 0; i < fish.Length; i++)
    {
        fishArr[fish[i]]++;
    }
    // get starting fishcount
    for (int i = 0; i < fishArr.Length; i++)
    {
        totalFishCount += fishArr[i];
    }

    for (int i = 0; i < days; i++)
    {
        // new fish addition p1
        var newFishesAmt = fishArr[0];
        totalFishCount += newFishesAmt;

        fishArr[0] = 0;
        fishArr[7] += newFishesAmt;

        // move array 
        for (int j = 1; j < 9 ; j++)
        {
            // all fishes are put 1 step closer to 0
            fishArr[j-1] += fishArr[j];
            fishArr[j] = 0;
        }

        // new fish addition p2
        fishArr[8] += newFishesAmt;
    }


    return totalFishCount.ToString();
}


// runtime for first problem is ~29 ms using arr slot for each fish ":D"
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
