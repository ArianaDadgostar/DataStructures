using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Non_Comparative_Sorts
{
    public class BucketSort
    {
        const int bucketNum = 4;

        private int largest;
        private int smallest;
        private List<int>[] buckets;

        public void EstablishLargestSmallest(int[] array)
        {
            float smallVal = float.PositiveInfinity;
            largest = 0;

            foreach (int val in array)
            {
                if (val < smallest)
                {
                    smallVal = val;
                }
                if (val < largest) continue;

                largest = val;
            }

            smallest = (int)smallVal;
        }

        public List<int>[] SetUpBuckets(int[] array)
        {
            buckets = new List<int>[bucketNum];
            for (int i = 0; i < bucketNum; i++)
            {
                buckets[i] = new List<int>();
            }
            EstablishLargestSmallest(array);

            double bucketRange = (double)largest / (bucketNum - 1);

            foreach (int val in array)
            {
                buckets[(int)(((float)val) / bucketRange)].Add(val);
            }


            foreach (List<int> bucket in buckets)
            {
                SortBuckets(bucket);
            }

            return buckets;
        }

        public List<int> Junk(List<int> bucket)
        {
            for(int i = 0; i < bucket.Count; i++)
            {
                int index = i - 1;
                if (bucket[i] > bucket[index]) continue;
                for (int j = 0; j < index; j++)
                {
                    if (bucket[j + 1] > bucket[i]) continue;
                    index = j;
                    break;
                }

                int replaced = 0;
                int wanted = bucket[i];
                for(int j = index;  j < i; j++)
                {
                    replaced = bucket[j];
                    bucket[j] = wanted;

                    wanted = replaced;
                }
            }

            return bucket;
        }
        List<int> SortBuckets(List<int> unsorted)
        {
            int num = 0;
            for (int i = 0; i < unsorted.Count; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (unsorted[j] >= unsorted[j - 1]) break;
                    num = unsorted[j];
                    unsorted[j] = unsorted[j - 1];
                    unsorted[j - 1] = num;
                }
            }

            return unsorted;
        }

        public int[] BucketSorting(int[] unsorted)
        {
            EstablishLargestSmallest(unsorted);
            List<int>[] buckets = SetUpBuckets(unsorted);

            int[] sorted = new int[unsorted.Length];

            int count = 0;

            for (int i = 0; i < buckets.Length; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    sorted[count] = buckets[i][j];
                    count++;
                }
            }

            return sorted;
        }
    }
}
