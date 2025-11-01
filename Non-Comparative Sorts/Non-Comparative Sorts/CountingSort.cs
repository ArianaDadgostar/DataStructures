namespace Non_Comparative_Sorts
{
    public class CountingSort
    {
        private int[] values;
        int largest;
        int smallest;
        
        public void SetUpList(int[] array)
        {
            largest = 0;
            float smallVal = float.PositiveInfinity;
            foreach(int val in array)
            {
                if(val > largest)
                {
                    largest = val;
                    continue;
                }
                if (val > smallVal) continue;

                smallVal = val;
            }

            smallest = (int)smallVal;
        }
        
        public int[] SortList(int[] array)
        {
            values = new int[largest - (smallest - 1)];
            
            foreach(int val in array)
            {
                values[val - smallest]++;
            }

            return values;
        }
    }
}
