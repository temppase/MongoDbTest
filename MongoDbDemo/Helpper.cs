using System;

namespace MongoDbDemo
{
    partial class Program
    {
        public class Helpper
        {
            public void PrintSelect()
            {
                Console.WriteLine();
                Console.WriteLine(" ------------");
                Console.WriteLine("   Mongo UI");
                Console.WriteLine(" ------------");
                Console.WriteLine();
                Console.WriteLine("  Select what to do");
                Console.WriteLine("   0: Break");
                Console.WriteLine("   1: Add record");
                Console.WriteLine("   2: Print records");
                Console.WriteLine("   3: Search by label");
                Console.WriteLine("   4: Search by id (Upsert & Delete)");
                Console.WriteLine("   5: Search content");
                Console.WriteLine(" ---------------------");
                Console.Write("  Your selection: ");
            }
            public void BacKToMenu()
            {
                Console.WriteLine("    Press any key to menu...");
                Console.ReadKey();
            }
            public void DrawLine()
            {
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}
