using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace RiseOfStrongholds.Classes
{
    public class RandomClass
    {
        public RandomClass()
        {

        }

        public int produceInt(int min, int max) //code taken from http://stackoverflow.com/questions/4892588/rngcryptoserviceprovider-random-number-review
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];

            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);

            return new Random(result).Next(min, max);
        }
    }
}
