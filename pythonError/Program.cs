using System;
using System.Numerics;
using System.Text;

namespace pythonError
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your text: ");
            string text = Console.ReadLine();

            byte[] asciiBytes = Encoding.ASCII.GetBytes(text);

            for (int i = 0; i < text.Length; i++)
            {
                Console.WriteLine(text[i].ToString() + ": " + asciiBytes[i].ToString());
            }

            Console.Write("Enter the p: ");
            int.TryParse(Console.ReadLine(), out int p);
            Console.Write("Enter the q: ");
            int.TryParse(Console.ReadLine(), out int q);
            Console.Write("Enter the d: ");
            int.TryParse(Console.ReadLine(), out int d);
            Console.Write("Enter the H0: ");
            int.TryParse(Console.ReadLine(), out int H0);


            int n = p * q;
            int fn = (p - 1) * (q - 1);

            int k = K_checker(fn, d);

            int e = (fn * k + 1) / d;

            Console.WriteLine("(e, n) : " + "(" + e.ToString() + ", " + n.ToString() + ") --> Открытый ключ.");
            Console.WriteLine("(d, n) : " + "(" + d.ToString() + ", " + n.ToString() + ") --> Секретный ключ.");

            double[] step_one = new double[asciiBytes.Length];

            for (int i = 0; i < asciiBytes.Length; i++)
            {
                step_one[i] = Math.Pow(asciiBytes[i], e) % n;
            }

            Console.Write("Наш начальный значения:  ");
            foreach (var item in asciiBytes)
            {
                Console.Write(' ' + item.ToString());
            }
            Console.WriteLine();

            Console.Write("Получилось этот шифр:  ");
            foreach (var item in step_one)
            {
                Console.Write(' ' + item.ToString());
            }

            double[] step_two = new double[step_one.Length];
            step_two[0] = Math.Pow(H0 + step_one[0], 2) % n;

            for (int i = 1; i < step_one.Length; i++)
            {
                step_two[i] = Math.Pow(step_two[i - 1] + step_one[i], 2) % n;
            }
            Console.WriteLine();
            Console.Write("Берем хэш-код наш текст:  ");
            foreach (var item in step_two)
            {
                Console.Write(' ' + item.ToString());
            }


            BigInteger s = BigInteger.Pow((BigInteger)step_two[step_two.Length - 1], d) % n;
            Console.WriteLine("\nChecker s: " + s);

            BigInteger[] step_three = new BigInteger[step_two.Length];
            for (int i = 0; i < step_two.Length; i++)
            {
                step_three[i] = BigInteger.Pow((BigInteger)step_one[i], d) % n;
            }

            Console.Write("\nБерем хэш-код наш текст:  ");
            foreach (var item in step_three)
            {
                Console.Write(' ' + item.ToString());
            }

            BigInteger[] step_four = new BigInteger[step_three.Length];
            step_four[0] = BigInteger.Pow(step_three[0] + H0, 2) % n;

            for (int i = 1; i < step_three.Length; i++)
            {
                step_four[i] = BigInteger.Pow(step_three[i] + step_four[i - 1], 2) % n;
            }

            Console.Write("\nБерем проверенный хэш-код наш текст:  ");
            foreach (var item in step_four)
            {
                Console.Write(' ' + item.ToString());
            }

            Console.Write("\nНаш начальный значения:  ");
            foreach (var item in asciiBytes)
            {
                Console.Write(' ' + ((char)item).ToString());
            }
            Console.WriteLine();
            Console.ReadKey();


        }
        static int K_checker(int fn, int d)
        {
            int k = 1;
            for (int i = 0; i <= 100; i++)
            {
                k = i;
                if ((fn * k + 1) % d == 0)
                {
                    break;
                }
            }
            return k;
        }
    }
}
