void Start()
{
    // runtime 51 ms
    print(AdventOfCode07TheTreacheryOfWhales(true));
    // runtime 50 ms
    print(AdventOfCode07TheTreacheryOfWhales(false));
}

string AdventOfCode07TheTreacheryOfWhales(bool firstPart)
{
    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    stopwatch.Start();

    var path = Application.dataPath + "/AdventOfCode/07.txt";
    string data = System.IO.File.ReadAllText(path);
    string[] splitData = data.Split(',');
    int[] intData = Array.ConvertAll(splitData, s => int.Parse(s));
    var biggestInt = Mathf.Max(intData);
    int[] resultsArr = new int[biggestInt];


    if (firstPart)
    {
        var total = 0;
        var current = 0;
        for (int i = 0; i < biggestInt; i++)
        {
            for (int j = 0; j < splitData.Length; j++)
            {
                total += Mathf.Abs(intData[j] - current);
            }
            current++;
            resultsArr[i] += total;
            total = 0;
        }
    }
    else
    {
        int[] fuelCostArr = new int[biggestInt + 1];
        // make lookup table so this doesn't need to be recalculated
        fuelCostArr[0] = 0;
        var stepCost = 1;
        for (int i = 1; i < fuelCostArr.Length; i++)
        {

            fuelCostArr[i] = fuelCostArr[i - 1] + stepCost;
            stepCost++;
        }

        var total = 0;
        var current = 0;
        for (int i = 0; i < biggestInt; i++)
        {
            for (int j = 0; j < splitData.Length; j++)
            {
                total += fuelCostArr[Mathf.Abs(intData[j] - current)];
            }
            current++;
            resultsArr[i] += total;
            total = 0;
        }
    }

    stopwatch.Stop();
    print($"{ nameof(firstPart) } = {firstPart}: {stopwatch.ElapsedMilliseconds}");

    return (Mathf.Min(resultsArr).ToString());
}
