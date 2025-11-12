using Non_Comparative_Sorts;

namespace SortTest
{
    public class CountingSortTest
    {
        [Theory]
        [InlineData(new int[] { 3, 2, 1, 3, 4 }, new int[] { 1, 1, 2, 1 })]
        [InlineData(new int[] { 5, 1, 5, 9 }, new int[] { 1, 0, 0, 0, 2, 0, 0, 0, 1 })]

        public void SortingTest(int[] array, params int[] solution)
        {
            CountingSort sorting = new CountingSort();
            sorting.SetUpList(array);
            int[] sorted = sorting.SortList(array);

            for (int i = 0; i < sorted.Length; i++)
            {
                Assert.True(sorted[i] == solution[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { 3, 2, 1, 4 }, new int[] { 1, 2, 3, 4 })]
        [InlineData(new int[] { 5, 1, 3, 9 }, new int[] { 1, 3, 5, 9 })]
        [InlineData(new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]

        public void BucketTest(int[] array, params int[] solution)
        {           
            BucketSort sorting = new BucketSort();
            int[] sorted = sorting.BucketSorting(array);

            for (int i = 0; i < sorted.Length; i++)
            {
                Assert.True(sorted[i] == solution[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { 34, 2, 100, 42 }, new int[] { 2, 34, 42, 100 })]
        [InlineData(new int[] { 150, 2000, 14, 3 }, new int[] { 3, 14, 150, 2000 })]
        [InlineData(new int[] { 19, 20, 1, 404 }, new int[] { 1, 19, 20, 404 })]
        [InlineData(new int[] { 10, 9, 8, 7, 6, 10000, 5, 4, 3, 2, 1, 0 }, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10000 })]
        public void RadixTest(int[] array, params int[] solution)
        {
            RadixSort sorting = new RadixSort();
            int[] sorted = sorting.Sort(array);

            for (int i = 0; i < sorted.Length; i++)
            {
                Assert.True(sorted[i] == solution[i]);
            }
        }
    }
}