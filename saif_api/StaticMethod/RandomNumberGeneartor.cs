﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;


namespace saif_api.StaticMethod
{
    public static class RandomNumberGeneartor
    {
        // Instantiate random number generator.  
        private static readonly Random _random = new Random();

        // Generates a random number within a range.      
        public static int Generate(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
