// Task 1
Console.WriteLine("Task 1");

static int SumEvenNum(int start, int end)
{
    int sum = 0;
    for (int i = start; i <= end; i++)
    {
        if (i % 2 == 0)
        {
            sum += i;
        }
    }
    return sum;
}
Console.WriteLine($"The sum using for loop is: {SumEvenNum(1, 100)}");

static int SumEvenNumWhile(int start, int end)
{
    int sum = 0;
    int i = start;
    while (i <= end)
    {
        if (i % 2 == 0)
        {
            sum += i;
        }
        i++;
    }
    return sum;
}
Console.WriteLine($"The sum using while loop is: {SumEvenNumWhile(1, 100)}");

static int SumForeachNum(int start, int end)
{
    int sum = 0;
    foreach (int i in Enumerable.Range(start, end - start + 1))
    {
        if (i % 2 == 0)
        {
            sum += i;
        }
    }
    return sum;
}
Console.WriteLine($"The sum using foreach loop is: {SumForeachNum(1, 100)}");

// Task 2
Console.WriteLine("\nTask 2");
// Using if/else
Console.Write("if/else method: ");
static char GetLetterGrade(int score)
{
    if (score >= 90) return 'A';
    else if (score >= 80) return 'B';
    else if (score >= 70) return 'C';
    else if (score >= 60) return 'D';
    else return 'F';
}
Console.Write("Enter your score (0-100): ");
char grade = GetLetterGrade(int.Parse(Console.ReadLine()));
Console.WriteLine($"Your letter grade is: {grade}");

// Using switch expression
Console.Write("switch method: ");
static char GetLetterGradeSwitch(int score)
{
    return score switch
    {
        >= 90 => 'A',
        >= 80 => 'B',
        >= 70 => 'C',
        >= 60 => 'D',
        _ => 'F',
    };
}
Console.Write("Enter your score (0-100): ");
char gradeSwitch = GetLetterGradeSwitch(int.Parse(Console.ReadLine()));
Console.WriteLine($"Your letter grade is: {gradeSwitch}");

// Task 3
Console.WriteLine("\nTask 3\n");

static int SumEvenNumPlus(int start, int end)
{
    int sum = 0;
    for (int i = start; i <= end; i++)
    {
        if (i % 2 == 0)
        {
            sum += i;
        }
    }
    if (sum > 2000)
    {
        Console.WriteLine("That’s a big number!");
    }
    else
    {
        switch (sum)
        {
            case <= 127:
                Console.WriteLine("Wow, that's less than signed binary 8-bit can hold!");
                break;
            case <= 255:
                Console.WriteLine("That's less than unsigned binary 8-bit can hold!");
                break;
        }
    }
    return sum;
}
int result100 = SumEvenNumPlus(1, 100);
Console.WriteLine($"The sum of 100 using for loop is: {result100}\n");
int result10 = SumEvenNumPlus(1, 10);
Console.WriteLine($"The sum of 10 using for loop is: {result10}\n");
int result30 = SumEvenNumPlus(1, 30);
Console.WriteLine($"The sum of 30 using for loop is: {result30}\n");