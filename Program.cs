using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgPOE
{
    internal class Program
    {
        static void Main(string[] args)
        {
           //creates a new instance of method1 called run
            method1 run = new method1();
            run.menu(); //this will run the menu method in method1
        }
    }
}
