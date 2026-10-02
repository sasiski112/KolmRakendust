using System;

namespace MatemaatikaMang
{
    public class Ulesanne
    {
        public int A { get; private set; }
        public int B { get; private set; }
        public char Tehe { get; private set; }
        public int Vastus { get; private set; }

        public Ulesanne(char tehe, int raskus, Random juhuslik)
        {
            Tehe = tehe;
            int max = 10;
            if (raskus == 1) max = 50;
            if (raskus == 2) max = 100;
            int korrutaMax = 10 + raskus * 5;  

            if (tehe == '+')
            {
                A = juhuslik.Next(1, max + 1);
                B = juhuslik.Next(1, max + 1);
                Vastus = A + B;
            }
            else if (tehe == '-')
            {
                A = juhuslik.Next(2, max + 1);
                B = juhuslik.Next(1, A);      
                Vastus = A - B;
            }
            else if (tehe == '*')
            {
                A = juhuslik.Next(2, korrutaMax + 1);
                B = juhuslik.Next(2, korrutaMax + 1);
                Vastus = A * B;
            }
            else // jagamine
            {
                B = juhuslik.Next(2, korrutaMax + 1);
                Vastus = juhuslik.Next(2, korrutaMax + 1);
                A = B * Vastus;                 // jagub alati ilma jäägita
            }
        }

        // Tehte märk ekraanile
        public string Margi()
        {
            if (Tehe == '*') return "×";
            if (Tehe == '/') return "÷";
            if (Tehe == '-') return "−";
            return "+";
        }
    }
}
