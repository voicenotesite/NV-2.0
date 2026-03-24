using System;
using System.Collections.Generic;

class Program
{
    // Definicja 4 głównych potęg świata NV-2.0
    static string[] Factions = { "Neon Order", "Rust Marauders", "Silicon Cult", "Void Seekers" };
    static List<string> ActiveNPCs = new List<string>();

    static void Main()
    {
        Console.Title = "NV-2.0 | ENGINE LOGIC";
        Console.WriteLine("--- NV-2.0: SYSTEM FRAKCJI AKTYWNY ---");
        
        Random rng = new Random();

        // Symulujemy rozwój świata przez pierwsze 60 dni
        for (int day = 1; day <= 60; day++) 
        {
            // Mechanika Bartosza: Nowy bot pojawia się co 10 dni
            if (day % 10 == 0)
            {
                int botNumber = day / 10;
                string assignedFaction;

                if (botNumber <= 4)
                {
                    // Pierwsza czwórka to LIDERZY (każdy dostaje swoją gildię)
                    assignedFaction = Factions[botNumber - 1];
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[DZIEŃ {day}] POWSTAŁ LIDER: Bot #{botNumber} zakłada {assignedFaction}!");
                }
                else
                {
                    // Każdy kolejny to REKRUT - Totalny Random dołącza do jednej z gildii
                    assignedFaction = Factions[rng.Next(0, 4)];
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[DZIEŃ {day}] REKRUT: Bot #{botNumber} dołącza do {assignedFaction} (Total Random)");
                }
                
                ActiveNPCs.Add($"Bot_{botNumber}@{assignedFaction}");
                Console.ResetColor();
            }
        }

        Console.WriteLine("\n--- AKTUALNY STAN ŚWIATA NV-2.0 ---");
        foreach(var npc in ActiveNPCs) Console.WriteLine(npc);
    }
}