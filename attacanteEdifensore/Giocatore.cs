using System;
using System.Threading;

namespace Attaccante_e_Difensore
{
    internal class Giocatore
    {
        Random casuale = new Random();

        private string nome;
        public string Nome
        {
            get { return nome; }
        }
        private int punti_ferita;
        public int Punti_ferita
        {
            get { return punti_ferita; }
        }
        private int attacco;
        public int Attacco
        {
            get { return attacco; }
        }
        private int perc_difesa;
        public int Perc_difesa
        {
            get { return perc_difesa; }
        }
        private double punti_feritaAttuali;
        public double Punti_feritaAttuali
        {
            get { return punti_feritaAttuali; }
            set { punti_feritaAttuali = value; }
        }
        public Giocatore(string nome)
        {
            this.nome = nome;
            punti_ferita = casuale.Next(5000, 10000);
            attacco = casuale.Next(200, 500);
            perc_difesa = casuale.Next(0, 100);
            punti_feritaAttuali = punti_ferita;
        }

        public void Lotta(Arena arena)
        {
            Console.WriteLine($"{nome} è pronto a combattere!");
            arena.EntraNellArena(this);
        }
    }
}
