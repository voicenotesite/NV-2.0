using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NV2_Project
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "NV-2.0 CORE | COMMAND CENTER";
            
            // Inicjalizacja Frakcji
            var factions = new List<Faction>
            {
                new Faction { Name = "NEON ORDER" },
                new Faction { Name = "SILICON CULT" },
                new Faction { Name = "BIO-HACKERS" },
                new Faction { Name = "VOID RUNNERS" }
            };

            bool running = true;
            Random rng = new Random();

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("====================================================");
                Console.WriteLine("          NV-2.0: CENTRUM DOWODZENIA GILDII         ");
                Console.WriteLine("====================================================");
                Console.ResetColor();

                // Wyświetlanie Statusu Frakcji
                Console.WriteLine($"{"FRAKCJA",-15} | {"POZIOM",-7} | {"BOTY",-6} | {"SKARBIEC",-10}");
                Console.WriteLine(new string('-', 52));

                foreach (var f in factions)
                {
                    Console.WriteLine($"{f.Name,-15} | Lvl {f.PowerLevel,-3} | {f.BotCount,-6} | {f.Treasury,8:N2} NV");
                }

                Console.WriteLine("\n[KOMENDY]: 'recruit [nazwa]', 'bounty', 'exit'");
                Console.Write("\n> ");
                string input = Console.ReadLine()?.ToLower() ?? "";

                if (input == "exit") { running = false; }
                else if (input == "bounty")
                {
                    var winner = factions[rng.Next(factions.Count)];
                    decimal prize = (decimal)(rng.NextDouble() * 500 + 100);
                    winner.AddBounty(prize);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n[BOUNTY] {winner.Name} zgarnia {prize:N2} NV-Credits!");
                    Console.ResetColor();
                    await Task.Delay(1500);
                }
                else if (input.StartsWith("recruit "))
                {
                    string name = input.Replace("recruit ", "").ToUpper();
                    var f = factions.FirstOrDefault(x => x.Name.Contains(name));
                    if (f != null)
                    {
                        f.Recruit(5);
                        Console.WriteLine($"\n[INFO] {f.Name} zrekrutował 5 nowych botów!");
                        await Task.Delay(1000);
                    }
                }
            }
        }
    }
}