using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TILab4
{
    class User
    {
        public BigInteger s;
        public BigInteger v;
        public BigInteger n;
        private BigInteger r;

        public void Generate(BigInteger n)
        {
            this.n = n;
            GenerateNewS();
            GenerateNewV();
            
        }

        public void GenerateNewS()
        {
            Random random = new Random();

            do
            {
                s = random.Next(1, Int32.MaxValue);
            }
            while (s > n || GCD(s, n) != 1);

        }

        public void GenerateNewV()
        {
            v = BigInteger.ModPow(s, 2, n);
        }

        public BigInteger GenerateR()
        {
            Random random = new Random();
            BigInteger x;

            do
            {
                r = random.Next(1, Int32.MaxValue);
            }
            while (r > n);

            x = BigInteger.ModPow(r, 2, n);

            return x;
        }

        public BigInteger GenerateY(int bit)
        {
            BigInteger y;

            if (bit == 1)
                y = (r * s) % n;
            else
                y = r % n;

            return y;
        }

        

        private BigInteger GCD(BigInteger a, BigInteger b)
        {
            if (a == 0)
            {
                return b;
            }
            else
            {
                while (b != 0)
                {
                    if (a > b)
                    {
                        a -= b;
                    }
                    else
                    {
                        b -= a;
                    }
                }

                return a;
            }
        }
    }


}
