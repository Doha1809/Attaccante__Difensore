using System;
using System.Collections.Generic;
using System.Threading;

namespace Attaccante_e_Difensore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Giocatore> players = new List<Giocatore>();
            Arena arena = new Arena();
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 8; i++)
            {
                Giocatore giocatore = new Giocatore($"Player{i}");
                players.Add(giocatore);
                Thread combattente = new Thread(() => giocatore.Lotta(arena));
                threads.Add(combattente);
                combattente.Start();
            }

            
            foreach (var t in threads)
            {
                t.Join();
            }

            Console.WriteLine("Tutti i combattimenti sono terminati.");
            Console.ReadLine();
        }
    }
}
