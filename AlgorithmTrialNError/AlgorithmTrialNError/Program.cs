using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmTrialNError
{
    class Program
    {
        static void Main(string[] args)
        {

            double total1 = 0;
            double total2 = 0;
            int howMany = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("howMany is: " + howMany);
            for (int i = 0; i < howMany; i++)
            {

            
                double num = Convert.ToInt32(Console.Read());

                if (num % 2 == 1)
                {
                    double newnumber = Math.Pow(2, num + 1);
                    total1 = total1 + newnumber;
                }
                else
                {
                    double newnumber2 = Math.Sqrt(num);
                    total2 = total2 + newnumber2;
                }
                

            }
            
            Console.WriteLine("total1 is: "+total1);
            Console.WriteLine("total2 is: "+total2);

            Console.ReadKey();
        }

        void DigitFinding()
        {

            Console.WriteLine("Please enter a number...");

            int num = Convert.ToInt32(Console.ReadLine());
            int i = 0;

            while (num >= 1)
            {
                num = num / 10;
                i++;
            }

            Console.WriteLine("Your number has " + i + " digits.");

            Console.ReadKey();
        }
        void ModSevThreeToTheNumber()
        {
            int number = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= number; i++)
            {
                if (i % 3 == 0 && i % 7 == 0)
                {
                    Console.WriteLine(i);
                }
            }

            Console.ReadKey();
        }
        void ConvertToAbsoluteValue()
        {
            Console.WriteLine("Write the number you want the absolute value of: ");
            int number = Convert.ToInt32(Console.ReadLine());

            if (number >= 0)
            {
                Console.WriteLine(number);
            }
            else
            {
                int newnumber = number * (-1);
                Console.WriteLine(newnumber);
            }

            Console.ReadKey();
        }
        void HowManyPosZeroNeg()
        {
            Console.WriteLine("Please enter how many numbers you want to enter: ");
          

            int howMany = Convert.ToInt32(Console.ReadLine());
            int zero = 0;
            int positive = 0;
            int negative = 0;
            for (int i = 1; i <= howMany; i++)
            {
                int number = Convert.ToInt32(Console.ReadLine());
                if (number == 0)
                {
                    zero++;
                }
                else if (number > 0)
                {
                    positive++;
                }
                else
                {
                    negative++;
                }
            }

            Console.WriteLine("You entered " + zero + " 0 values");
            Console.WriteLine("You entered " + positive + " + values");
            Console.WriteLine("You entered " + negative + " - values");

            Console.ReadKey();
        }
        void BasicFor()
        {

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }

    }
}
