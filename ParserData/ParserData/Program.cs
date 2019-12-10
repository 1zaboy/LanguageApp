using System;

namespace ParserData
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseLan parseLan = new ParseLan();
            parseLan.SetDbWords("./LabFolder/Es/es.txt", 4);
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
