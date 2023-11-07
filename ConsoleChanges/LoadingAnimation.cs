using System;
using System.Threading;

class LoadingAnimation
{
    public static void Loading(string text)
    {
        Console.Write(text);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(".");
                Thread.Sleep(500); // Adjust the delay as needed (500 milliseconds = half a second)
            }

            Console.Write("\b\b\b   \b\b\b"); // Clear the three dots
            Thread.Sleep(500); // Wait before the next iterationc

        }
        Console.Clear();
    }
}