using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TILab2
{
    class Keys
    {
        static long[] FermaNum = new long[3] { 17, 257, 65537 };

        private long q;

        private long p;

        private long Module;

        private long SecretE;

        private long e;

        public long[] PrivateKey { get; }

        public long[] PublicKey { get; }

        public Keys()
        {
            PrivateKey = new long[2];
            PublicKey = new long[2];
        }

        public void GenerateKeys()
        {
            bool IsSimple = false;
            Random random = new Random();

            while (!IsSimple)
            {
                p = random.Next(10000, 100000);
                if (CheckNumber(p))
                {
                    IsSimple = true;
                    for (long i = 2; i < Math.Sqrt(p) + 1; i++)
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
                q = random.Next(10000, 100000);
                if (CheckNumber(q))
                {
                    IsSimple = true;
                    for (long i = 2; i < Math.Sqrt(q) + 1; i++)
                    {
                        if (q % i == 0)
                        {
                            IsSimple = false;
                            break;
                        }
                    }
                }
            }

            Module = p * q;

            long EulerFunc = (p - 1) * (q - 1);
            e = FermaNum[random.Next(0, 2)];

            PublicKey[0] = e;
            PublicKey[1] = Module;

            SecretE = Reverse(e, EulerFunc);
            if (SecretE < 0) 
                SecretE = EulerFunc + SecretE;
            if (SecretE == 0)
                GenerateKeys();
			else
			{
				PrivateKey[0] = SecretE;
				PrivateKey[1] = Module;
			}
        }

        private bool CheckNumber(long Num)
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

        private bool MillerRabinTest(long Num, long a)
        {

            if (Num % 2 == 0)
                return false;
            long s = 0, d = Num - 1;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            long r = 1;
            long x = (long)BigInteger.ModPow(a, d, Num);
            if (x == 1 || x == Num - 1)
                return true;

            x = (long)BigInteger.ModPow((long)Math.Pow(a, d), (long)Math.Pow(2, r), Num);

            if (x == 1)
                return false;

            if (x != Num - 1)
                return false;

            return true;
        }


        private static void EuclidEx(long a, long b, out long x, out long y, out long d)
        {
            long q, r, x1, x2, y1, y2;

            if (b == 0)
            {
                d = a;
                x = 1;
                y = 0;
                return;
            }

            x2 = 1;
            x1 = 0;
            y2 = 0;
            y1 = 1;

            while (b > 0)
            {
                q = a / b;
                r = a - q * b;
                x = x2 - q * x1;
                y = y2 - q * y1;
                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }

            d = a;
            x = x2;
            y = y2;
        }

        static long Reverse(long a, long n)
        {
            long x, y, d;
            EuclidEx(a, n, out x, out y, out d);

            if (d == 1) 
                return x;

            return 0;

        }

    }
}
