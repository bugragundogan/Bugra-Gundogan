using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDESA33
{
    class SDESA33
    {
        static void Main(string[] args)
        {
            // ax^2+bx+c=0, find x
            // disk.=b^2-4ac
            // x1,2= -b+(-) "Enter a number"
            //1x^2+3x+4=0

            //int a = 5;
            //int b = 4;
            //int c = -12;
            //double x;

            double a;
            double b;
            double c;

            Console.WriteLine("Welcome to Quadratic Equation Solver!");

            Console.WriteLine("Please enter the coeffcient values.");

            Console.WriteLine("Enter a");
            a = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter b");
            b = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter c");
            c = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Your equation is: "+a+"x^2+"+b+"x+"+c+" = 0");

            Console.WriteLine("");

            Console.WriteLine("");

            Console.WriteLine("");




            double diskriminant = (b * b) - 4 * a * c;

            double disksqrt = Math.Sqrt(diskriminant);

            double rootone = (-b + disksqrt) / (2 * a);

            double roottwo = (-b - disksqrt) / (2 * a);

            if (diskriminant < 0)
            {
                Console.WriteLine("EQUATION HAS NO REAL ROOT.");
            }
            else
            {
                Console.WriteLine("Root one = " +rootone);
                Console.WriteLine("Root two = " +roottwo);
            }
            

            
            Console.ReadKey();

        }
    }
}
