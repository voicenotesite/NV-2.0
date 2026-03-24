using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices; // KLUCZOWE: Do rozmowy z Rustem
using System.Threading.Tasks;

namespace NV2_Project
{
    class Program
    {
        // IMPORT Z RUSTA (Musi pasować do nazw w lib.rs)
        [DllImport("nv_core.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double calculate_combat_power(int bots, int power_level, double treasury);

        [DllImport("nv_core.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void check_core_status();

        static async Task Main(string[] args)
        {
            Console.Title = "NV-2.0 CORE HYBRID | C# + RUST";
            
            // Inicjalizacja Core
            try {
                check_core_status(); 
            } catch (Exception e) {
                Console.WriteLine($"[BŁĄD] Nie znaleziono nv_core.dll! {e.Message}");
            }

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
                Console.WriteLine("          NV-2.0: HYBRYDOWY SILNIK DECYZYJNY        ");
                Console.WriteLine("====================================================");
                Console.ResetColor();

                Console.WriteLine($"{"FRAKCJA",-15} | {"BOTY",-6} | {"SKARBIEC",-10} | {"MOC (RUST)"}");
                Console.WriteLine(new string('-', 60));

                foreach (var f in factions)
                {
                    // WYWOŁANIE RUSTA: Tu dzieje się magia!
                    double rustPower = calculate_combat_power(f.BotCount, f.PowerLevel, (double)f.Treasury);
                    
                    Console.WriteLine($"{f.Name,-15} | {f.BotCount,-6} | {f.Treasury,8:N2} | {rustPower,8:N2} CP");
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
                    await Task.Delay(1000);
                }
                else if (input.StartsWith("recruit "))
                {
                    string name = input.Replace("recruit ", "").ToUpper();
                    var f = factions.FirstOrDefault(x => x.Name.Contains(name));
                    if (f != null) { f.Recruit(5); }
                }
            }
        }
    }
}