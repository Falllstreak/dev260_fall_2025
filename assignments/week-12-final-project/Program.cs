using System;

namespace MediaLibraryApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nMedia Library App");
            Console.WriteLine("============================");
            Console.WriteLine();

            // Starts the text UI handler
            MediaLibraryNavigator navigator = new MediaLibraryNavigator();
            navigator.Start();
        }
    }
}