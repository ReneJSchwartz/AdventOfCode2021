void Start()
{
    // First question of day 1 runs in 4 ms probably because of excessive int parsing
    print(AdventOfCode1(true)); 
    // 2nd question runs in 2 ms
    print(AdventOfCode1(false)); 
}

string AdventOfCode1(bool firstHalf)
{
    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    stopwatch.Start();

    var path = Application.dataPath + "/AdventOfCode/01.txt";
    string[] data = System.IO.File.ReadAllLines(path);
    int increases = 0;

    if (firstHalf)
    {
        int currentNumber = int.MaxValue;

        foreach (var item in data)
        {
            if (int.Parse(item) > currentNumber)
            {
                increases++;
            }
            currentNumber = int.Parse(item);
        }
    }
    else
    {
        int[] intArr = Array.ConvertAll(data, s => int.Parse(s));

        var firstSum = (intArr[0] + intArr[1] + intArr[2]);

        var sum = firstSum;
        
        // sliding 3
        for (int i = 1; i < intArr.Length - 2; i++)
        {
            var currentSum = 0;
            currentSum += (intArr[i] + intArr[i + 1] + intArr[i + 2]);
            if (currentSum > sum)
            {
                increases++;
            }
            sum = currentSum;
        }
    }
    
    stopwatch.Stop();
    print(stopwatch.ElapsedMilliseconds);

    return (increases.ToString());
}
