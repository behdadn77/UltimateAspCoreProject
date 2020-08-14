using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Random
{
    public class RandomDigits
    {
        public string CreateRandomCode(int Count = 6)
        {
            try
            {
                string min = "1", max = "9";
                if (Count > 10 || Count < 1)
                {
                    return "";
                }
                if (Count == 10)
                {
                    System.Random randomm = new System.Random();
                    double MyCodee = randomm.Next(Convert.ToInt32(min), 2_147_483_647);

                    return MyCodee.ToString();
                }
                //int MaxInt32Value = 2_147_483_647;


                for (int i = 1; i < Count; i++)
                {
                    min += "0";
                    max += "9";
                }


                System.Random random = new System.Random();
                double MyCode = random.Next(Convert.ToInt32(min), Convert.ToInt32(max));

                return MyCode.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
