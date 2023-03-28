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

            wykonajProgram(położenieGłowicy, łańcuchOpisującyStanMaszyny, listing);
        }
        public static void wykonajProgram(int położenieGłowicy, char[] łańcuchOpisującyStanMaszyny, Czwórki listaKomend)
        {
            while (listaKomend.ContainsKey((łańcuchOpisującyStanMaszyny[położenieGłowicy], łańcuchOpisującyStanMaszyny[położenieGłowicy + 1])))
            {
                (char, char) wartośćZnalezionejKomendy = listaKomend.Values[listaKomend.IndexOfKey((łańcuchOpisującyStanMaszyny[położenieGłowicy], łańcuchOpisującyStanMaszyny[położenieGłowicy + 1]))];
                    
                switch (wartośćZnalezionejKomendy.Item2)
                {
                    case 'R':
                        łańcuchOpisującyStanMaszyny[położenieGłowicy] = łańcuchOpisującyStanMaszyny[++położenieGłowicy];
                        łańcuchOpisującyStanMaszyny[położenieGłowicy] = wartośćZnalezionejKomendy.Item1;
                        break;
                    case 'L':
                        łańcuchOpisującyStanMaszyny[położenieGłowicy] = łańcuchOpisującyStanMaszyny[--położenieGłowicy];
                        łańcuchOpisującyStanMaszyny[położenieGłowicy] = wartośćZnalezionejKomendy.Item1;
                        break;
                    default:
                        łańcuchOpisującyStanMaszyny[położenieGłowicy] = wartośćZnalezionejKomendy.Item1;
                        łańcuchOpisującyStanMaszyny[położenieGłowicy+1] = wartośćZnalezionejKomendy.Item2;
                        break;
                }
                Console.WriteLine();
                WyświetlStanMaszyny(łańcuchOpisującyStanMaszyny);
            }
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
