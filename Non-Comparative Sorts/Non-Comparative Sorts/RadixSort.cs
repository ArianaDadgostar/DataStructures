using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Non_Comparative_Sorts
{
    public class RadixSort
    {
        private int largest = 0;
        private int[] buckets = new int[10];
        private int[] sorted;
        public void IdentifyLargestDigit(int[] array)
        {
            int test = array[0];

            foreach(int digit in array)
            {
                if (digit < test) continue;
                test = digit;
            }

            while(test > 0)
            {
                test /= 10;
                largest++;
            }
        }

        public int[] Sort(int[] array)
        {
            IdentifyLargestDigit(array);
            int digit = 0;
            
            sorted = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                sorted[i] = array[i];
            }

            for (int i = 0; i < largest; i++)
            {
                buckets = new int[10];
                for(int j = 0; j < sorted.Length; j++)
                {
                    Math.DivRem(sorted[j], (int)Math.Pow(10.0, i + 1), out digit);
                    digit /= (int)Math.Pow(10.0, i);
                    buckets[digit]++;
                }

                for (int j = 1; j < buckets.Length; j++)
                {
                    buckets[j] += buckets[j - 1];
                }

                int[] temp = new int[sorted.Length];

                for(int j = sorted.Length - 1; j >= 0; j--)
                {
                    Math.DivRem(sorted[j], (int)Math.Pow(10.0, i+1), out digit);
                    digit /= (int)Math.Pow(10.0, i);

                    buckets[digit]--;
                    temp[buckets[digit]] = sorted[j];
                }

                sorted = temp;
            }

            return sorted;
        }
    }
}
