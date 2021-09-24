using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TILab4
{
    class TrustedCenter
    {
        private BigInteger p;
        private BigInteger q;
        public BigInteger n;

        public void GenerateN()
        {
            bool IsSimple = false;
            Random random = new Random();

            while (!IsSimple)
            {
                p = random.Next(1000000, 10000000);
                if (CheckNumber(p))
                {
                    IsSimple = true;
                    for (long i = 2; i < Math.Sqrt((ulong)p) + 1; i++)
                    {
                        if (p % i == 0)
                        {
                            IsSimple = false;
                            break;
                        }
                    }
                }
            }

            IsSimple = false;
            while (!IsSimple)
            {
                q = random.Next(1000000, 10000000);
                if (CheckNumber(q))
                {
                    IsSimple = true;
                    for (long i = 2; i < Math.Sqrt((ulong)q) + 1; i++)
                    {
                        if (q % i == 0)
                        {
                            IsSimple = false;
                            break;
                        }
                    }
                }
            }

            n = p * q;
        }

        private bool CheckNumber(BigInteger Num)
        {
            for (long i = 2; i < Num && i < 11; i++)
            {
                if (!MillerRabinTest(Num, i))
                {
                    return false;
                }
            }
            return true;
        }

        private bool MillerRabinTest(BigInteger Num, BigInteger a)
        {

            if (Num % 2 == 0)
                return false;
            BigInteger s = 0, d = Num - 1;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            BigInteger r = 1;
            BigInteger x = BigInteger.ModPow(a, d, Num);
            if (x == 1 || x == Num - 1)
                return true;

            x = BigInteger.ModPow((ulong)Math.Pow((ulong)a, (ulong)d), (long)Math.Pow(2, (ulong)r), Num);

            if (x == 1)
                return false;

            if (x != Num - 1)
                return false;

            return true;
        }
    }
}
