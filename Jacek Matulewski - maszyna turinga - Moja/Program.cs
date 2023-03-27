using System;
using System.Collections.Generic;
using System.IO;

namespace Jacek_Matulewski___maszyna_turinga___Moja
{
    using Czwórki = SortedList<(char stanGłowicy, char stanMaszyny), (char nowyStanGłowicy, char nowyStanMaszyny)>;
    class Program
    {

        static void Main(string[] args)
        {
            string[] kodProgramu = File.ReadAllLines("program.txt");
            char[] łańcuchOpisującyStanMaszyny = File.ReadAllText("taśma.txt").ToCharArray();
            Console.Write($"Początkowy stan maszyny: ");
            WyświetlStanMaszyny(łańcuchOpisującyStanMaszyny);
            Console.WriteLine("\nProgram:");
            foreach (string liniaKodu in kodProgramu) Console.WriteLine(liniaKodu);

            Czwórki listing = new Czwórki();
            listing = SprawdźIUtwórzListęZTaśmy(kodProgramu);

            int położenieGłowicy = SprawdźStanMaszynyIZnajdźPozycjęgłowicy(łańcuchOpisującyStanMaszyny);
            WyświetlStanMaszyny(łańcuchOpisującyStanMaszyny);


            //Console.WriteLine(pozycjaGłowicy);
            //char[] łańcuchZnaków = łańcuchOpisującyStanMaszyny.ToCharArray();

            //for (int i = 0; i < kodProgramu.Length; i++)
            //{
            //    if (kodProgramu[i].Substring(0, 1).Equals(łańcuchZnaków[pozycjaGłowicy])&& kodProgramu[i].Substring(1, 2).Equals(łańcuchZnaków[pozycjaGłowicy+1]))
            //    {

            //        if (kodProgramu[i].Substring(2).Equals("R"))
            //        {
            //            łańcuchZnaków[pozycjaGłowicy] = łańcuchZnaków[++pozycjaGłowicy];
            //            łańcuchZnaków[pozycjaGłowicy] = kodProgramu[i].ToCharArray()[3];
            //            i = 0;
            //        }else if (kodProgramu[i].Substring(2).Equals("L"))
            //        {
            //            łańcuchZnaków[pozycjaGłowicy] = łańcuchZnaków[--pozycjaGłowicy];
            //            łańcuchZnaków[pozycjaGłowicy] = kodProgramu[i].ToCharArray()[3];
            //            i = 0;
            //        }
            //        else
            //        {
            //            łańcuchZnaków[pozycjaGłowicy] = kodProgramu[i].ToCharArray()[3];
            //            łańcuchZnaków[pozycjaGłowicy+1] = kodProgramu[i].ToCharArray()[2];
            //            i = 0;
            //        }
            //        Console.WriteLine();
            //        foreach(char znak in łańcuchZnaków) Console.Write(znak);
            //        Console.WriteLine(pozycjaGłowicy);
            //    }
            //}

        }
        public static Czwórki SprawdźIUtwórzListęZTaśmy(string[] kodProgramu)
        {
            Czwórki listing = new Czwórki();
            foreach (string linia in kodProgramu)
            {
                if (!(linia[0] > 96 && linia[3] > 96 && linia[0] < 123 && linia[3] < 123)) throw new Exception("niepoprawne znaki stanu głowicy!");
                if (!(linia[1] > 64 && linia[2] > 64 && linia[1] < 91 && linia[2] < 91)) throw new Exception("niepoprawne znaki wartości taśmy!");
                if (listing.ContainsKey((linia[0], linia[1]))) throw new Exception("Dwie tak samo zaczynające się linie kodu!");
                listing.Add((linia[0], linia[1]), (linia[3], linia[2]));
            }
            return listing;
        }
        public static int SprawdźStanMaszynyIZnajdźPozycjęgłowicy(char[] łańcuchOpisującyStanMaszyny)
        {
            int położenieGłowicy = -1;
            for (int i = 0; i < łańcuchOpisującyStanMaszyny.Length; i++)
            {
                if (łańcuchOpisującyStanMaszyny[i] > 96 && łańcuchOpisującyStanMaszyny[i] < 123 && położenieGłowicy == -1) położenieGłowicy = i;
                else if (łańcuchOpisującyStanMaszyny[i] > 64 && łańcuchOpisującyStanMaszyny[i] < 91) continue;
                else throw new Exception("Brak znaku głowicy lub dwa znaki głowicy");
            }
            return położenieGłowicy;
        }
        public static void WyświetlStanMaszyny(char[] stanMaszyny)
        {
            foreach (char znak in stanMaszyny) Console.Write(znak);
        }
    }
}
