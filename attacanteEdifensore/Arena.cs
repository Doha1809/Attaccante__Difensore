using System;
using System.Collections.Generic;
using System.Threading;

namespace Attaccante_e_Difensore
{
    internal class Arena
    {
        private  List<Giocatore> combattenti = new List<Giocatore>();
        private  Semaphore semaforo = new Semaphore(2, 2);
        private  Random riduzione = new Random();
        private  object lockObj = new object();

        public void Combattimento()
        {
            lock (lockObj)
            {
                if (combattenti.Count < 2)
                    return;

                Giocatore attaccante = combattenti[0];
                Giocatore difensore = combattenti[1];

                Console.WriteLine($"{attaccante.Nome} attacca {difensore.Nome}!");

                do
                {
                    Console.WriteLine($"L'attaccante {attaccante.Nome} ha un attacco di: {attaccante.Attacco}");
                    Console.WriteLine($"Il difensore {difensore.Nome} ha una difesa di: {difensore.Perc_difesa}");

                    if (attaccante.Attacco >= difensore.Perc_difesa)
                    {
                        double perc_ridotto = riduzione.Next(50, 100);
                        double danno_ridotto = ((100 - perc_ridotto) / 100) * attaccante.Attacco;
                        difensore.Punti_feritaAttuali -= danno_ridotto;

                        Console.WriteLine($"{difensore.Nome} ha subito {danno_ridotto} danni. Punti ferita rimasti: {difensore.Punti_feritaAttuali}");

                        if (difensore.Punti_feritaAttuali <= 0)
                        {
                            Console.WriteLine($"{difensore.Nome} è morto!");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"L'attacco di {attaccante.Nome} non ha avuto effetto!");
                    }

                  
                    int sceltaDifensore = riduzione.Next(0, 2);
                    int sceltaAttaccante = riduzione.Next(0, 2);

                    if (sceltaDifensore == 0)
                    {
                        Console.WriteLine($"{difensore.Nome} si è arreso!");
                        break;
                    }

                    if (sceltaAttaccante == 0)
                    {
                        Console.WriteLine($"{attaccante.Nome} si è arreso!");
                        break;
                    }

                } while (difensore.Punti_feritaAttuali > 0);

                Console.WriteLine("Combattimento terminato!");
                combattenti.Clear();
            }
        }

        public void EntraNellArena(Giocatore giocatore)
        {
            semaforo.WaitOne();
            lock (lockObj)
            {
                combattenti.Add(giocatore);
                Console.WriteLine($"{giocatore.Nome} è entrato nell'arena.");

                if (combattenti.Count == 2)
                {
                    Console.WriteLine($"Match: {combattenti[0].Nome} VS {combattenti[1].Nome}");
                    Combattimento();
                }
            }
            semaforo.Release();
        }
    }
}
