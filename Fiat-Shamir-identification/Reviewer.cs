using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TILab4
{
    class Reviewer
    {
        private static int NumOfRounds = 40;

        public bool Check(ref User user)
        {
            bool isPass = true;
            int bit;
            BigInteger x, y, y2, res;
            Random random = new Random();
            for (int i = 0; i < NumOfRounds && isPass; i++)
            {
                x = user.GenerateR();
                bit = random.Next(0, 2);
                y = user.GenerateY(bit);
                y2 = (y * y) % user.n;
                if (bit == 1)
                    res = (x * user.v) % user.n;
                else
                    res = x;

                if (y2 != res)
                {
                    isPass = false;
                }
            }

            return isPass;
        }
    }
}
