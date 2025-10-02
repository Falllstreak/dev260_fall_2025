using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        // Title
        Console.WriteLine("Sum for Even Numbers, 1 through 100: \n -----------------------------------");

        // for loop even number sum
        int forLoopSum = SumForLoop();
        Console.WriteLine($"Sum when using the For loop:    \t{forLoopSum}");
            // Big Num Check if/else
        if (forLoopSum > 2000)
        {
            Console.WriteLine("If/Else check: That's a BIG number!\n");
        }
        else
        {
            Console.WriteLine("If/Else check: That's definitely one of the numbers of all time.\n");
        }


        // whle loop even number sum
        int whileLoopSum = SumWhileLoop();
        Console.WriteLine($"Sum when using the While loop:  \t{whileLoopSum}");
            // Big Num Check ternary operator
        string bignumwhile = whileLoopSum > 2000 ? "That's a BIG number!\n" : "That's definitely one of the numbers of all time.\n";
        Console.WriteLine($"Ternary check: {bignumwhile}");


        // for each even number sum
        int forEachSum = SumForEachLoop();
        Console.WriteLine($"Sum when using the For Each loop:  \t{forEachSum}\n");
            // Big Num Check ternary operator
        string bignumforeach = forEachSum > 2000 ? "That's a BIG number!\n" : "That's definitely one of the numbers of all time.\n";
        Console.WriteLine($"Ternary check: {bignumforeach}\n");


        // Letter Grades using array
        Console.WriteLine("\nLetter Grading:");
        int[] grades = { 23, 98, 76, 66, 81 };

        foreach (int grade in grades)
        {
            Console.WriteLine($"Grade {grade}\n--------\nIf/Else: {LetterGradingIfElse(grade)}\nSwitch: {LetterGradingSwitch(grade)}\n");
        }
    }

    // For Loop Calculation
    static int SumForLoop()
    {

        int sum = 0;
        for (int i = 1; i <= 100; i++)
        {
            if (i % 2 == 0)
                sum += i;
        }
        return sum;

    }


    // While Loop Calculation
    static int SumWhileLoop()
    {

        int sum = 0;
        int i = 1;
        while (i <= 100)
        {
            if (i % 2 == 0)
                sum += i;
            i++;

        }
        return sum;

    }


    // ForEach Loop Calculation
    static int SumForEachLoop()
    {

        int sum = 0;
        List<int> numbers = new List<int>();

        for (int i = 1; i <= 100; i++)
        {
            numbers.Add(i);
        }

        foreach (int total in numbers)
        {
            if (total % 2 == 0)
                sum += total;
        }

        return sum;

    }

    // If/Else Grading
    static string LetterGradingIfElse(int grade)
    {
        if (grade >= 90) return "A";
        else if (grade >= 80) return "B";
        else if (grade >= 70) return "C";
        else if (grade >= 60) return "D";
        else return "F";
    }


    // Switch Grading
    static string LetterGradingSwitch(int grade) =>
        grade switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",   
            _ => "F"
        };
}

