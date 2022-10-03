using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Helsinki2017 {
    class Program
    {
        static string[,] adatRendez(string[] nyers)
        {
            string[,] tiszta = new string[5, nyers.Length];
            for (int i = 0; i < nyers.Length; i++) {
                string[] split = nyers[i].Split(';');
                for (int j = 0; j < 5; j++) {
                    tiszta[j, i] = split[j];
                }
            }
            return tiszta;
        }

        static class Pontok {
            public static string[,] rovidprogram;
            public static string[,] donto;
        }

        static void ÖsszPontszám(string teljesNev) {
            double sum = 0;
            for (int i = 1; i < Pontok.rovidprogram.GetLength(0); i++) {
                if (Pontok.rovidprogram[0, i] == teljesNev) {
                    sum += double.Parse(Pontok.rovidprogram[2, i], CultureInfo.InvariantCulture) + double.Parse(Pontok.rovidprogram[3, i], CultureInfo.InvariantCulture) - double.Parse(Pontok.rovidprogram[4, i], CultureInfo.InvariantCulture);
                }
            }
            for (int i = 1; i < Pontok.donto.GetLength(0); i++) {
                if (Pontok.donto[0, i] == teljesNev) {
                    sum += double.Parse(Pontok.donto[2, i], CultureInfo.InvariantCulture) + double.Parse(Pontok.donto[3, i], CultureInfo.InvariantCulture) - double.Parse(Pontok.donto[4, i], CultureInfo.InvariantCulture);
                }
            }
            if (sum != 0) {
                Console.WriteLine($"Összpontszám: {sum}");
            }
            else {
                Console.WriteLine("Nincs ilyen nevü versenyző.");
            }
        }

        static void Main(string[] args)
        {
            Pontok.rovidprogram = adatRendez(File.ReadAllLines(@"rovidprogram.csv"));
            Pontok.donto = adatRendez(File.ReadAllLines(@"donto.csv"));
            Console.WriteLine(Pontok.rovidprogram.GetLength(1) - 1);

            bool magyar = false;
            for (int i = 1; i < Pontok.donto.GetLength(1); i++) {
                magyar = Pontok.donto[1, i] == "HUN";
                if (magyar) {
                    i = Pontok.donto.GetLength(1);
                }
            }
            if (magyar) {
                Console.WriteLine("van magyar");
            }
            else {
                Console.WriteLine("nincs magyar");
            }

            ÖsszPontszám(Console.ReadLine());

            List<string> orszagok = new List<string>();

            for (int i = 1; i < Pontok.rovidprogram.GetLength(1); i++) {
                bool volt = false;
                for (int j = 0; j < orszagok.Count; j++) {
                    if (orszagok[j] == Pontok.rovidprogram[1, i]) {
                        volt = true;
                    }
                }
                if (!volt) {
                    orszagok.Add(Pontok.rovidprogram[1, i]);
                }
            }

            int[] orszagCount = new int[orszagok.Count];

            for (int i = 1; i < Pontok.donto.GetLength(1); i++) {
                for (int j = 0; j < orszagok.Count; j++) {
                    if (Pontok.donto[1, i] == orszagok[j]) {
                        orszagCount[j]++;
                    }
                }
            }

            for (int i = 0; i < orszagCount.Length; i++) {
                if (orszagCount[i] != 0) {
                    Console.WriteLine($"{orszagok[i]} {orszagCount[i]}");
                }
            }

            string[,] dontoEredmeny = new string[3, Pontok.donto.GetLength(1)];
            for (int i = 0; i < Pontok.donto.GetLength(1); i++) {
                dontoEredmeny[0, i] = Pontok.donto[0, i];
                dontoEredmeny[1, i] = Pontok.donto[1, i];
                //rushed
                dontoEredmeny[2, i] = Convert.ToString(double.Parse(Pontok.rovidprogram[2, i], CultureInfo.InvariantCulture) + double.Parse(Pontok.rovidprogram[3, i], CultureInfo.InvariantCulture) - double.Parse(Pontok.rovidprogram[4, i], CultureInfo.InvariantCulture) + double.Parse(Pontok.donto[2, i], CultureInfo.InvariantCulture) + double.Parse(Pontok.donto[3, i], CultureInfo.InvariantCulture) - double.Parse(Pontok.donto[4, i], CultureInfo.InvariantCulture));
            }

            using (StreamWriter sw = new StreamWriter("vegeredmeny.csv")) {
                for (int i = 0; i < dontoEredmeny.GetLength(1); i++) {
                    for (int j = 0; j < dontoEredmeny.GetLength(0); j++) {
                        sw.Write(dontoEredmeny[i,j]);
                    }
                    sw.WriteLine();
                }
                sw.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
