using System;
using System.Threading;
using System.Threading.Tasks;

namespace NV2_Project
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "NV-2.0 CORE ENGINE | Dysk G:";
            Console.Clear();

            // Nagłówek systemowy
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================================");
            Console.WriteLine("          NV-2.0: SYSTEM OPERACYJNY GILDII          ");
            Console.WriteLine("====================================================");
            Console.ResetColor();

            // Definicja 4 Głównych Frakcji
            string[] frakcje = { 
                "NEON ORDER", 
                "SILICON CULT", 
                "BIO-HACKERS", 
                "VOID RUNNERS" 
            };

            int totalCredits = 0;
            Random rng = new Random();

            Console.WriteLine($"\n[SYSTEM] Inicjalizacja na 12 rdzeniach Ryzena...");
            await Task.Delay(1000);

            // Aktywacja Liderów
            foreach (var f in frakcje)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[LIDER] Frakcja: {f} -> STATUS: AKTYWNA");
                await Task.Delay(300);
            }

            Console.ResetColor();
            Console.WriteLine("\n----------------------------------------------------");
            Console.WriteLine("[PROTOKÓŁ] Rozpoczynam generowanie zleceń (Bounties)...");
            Console.WriteLine("----------------------------------------------------\n");

            // Główna pętla symulacji Bounty
            for (int cykl = 1; cykl <= 10; cykl++)
            {
                int nagroda = rng.Next(100, 501);
                string łowca = frakcje[rng.Next(frakcje.Length)];
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"[CYKL {cykl:D2}] ");
                Console.ResetColor();
                Console.WriteLine($"{łowca} zlikwidował bota! Nagroda: +{nagroda} NV-Credits");

                totalCredits += nagroda;
                await Task.Delay(800); // Szybka symulacja akcji
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n----------------------------------------------------");
            Console.WriteLine($"[RAPORT FINALNY] Zebrano łącznie: {totalCredits} NV-Credits");
            Console.WriteLine("[INFO] Odliczanie do 10. dnia (Event: Bounty Hunt) TRWA.");
            Console.WriteLine("----------------------------------------------------");
            Console.ResetColor();
            
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć sesję...");
            Console.ReadKey();
        }
    }
}