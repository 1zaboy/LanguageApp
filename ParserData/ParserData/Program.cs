using System;

namespace ParserData
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseLan parseLan = new ParseLan();
            parseLan.SetDbWords("./LabFolder/Ru/ru.txt", 1);
            parseLan.SetDbWords("./LabFolder/En/en.txt", 2);
            parseLan.SetDbWords("./LabFolder/Pt/pt.txt", 3);
            parseLan.SetDbWords("./LabFolder/Es/es.txt", 4);
            parseLan.SetDbWords("./LabFolder/Bg/bg.txt", 5);
            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
