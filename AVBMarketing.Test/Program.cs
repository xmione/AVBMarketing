using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVBMarketing.Test
{
    public class Program
    {
        private static Tests _tests;
        public static void Main(string[] args )
        {
            _tests = new Tests();
            _tests.Test1();
        }
    }
}
