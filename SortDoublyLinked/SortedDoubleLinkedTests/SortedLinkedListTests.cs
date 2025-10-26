using SortDoublyLinked;

namespace SortedDoubleLinkedTests
{
    public class SortedLinkedListTests
    {
        [Theory]
        [InlineData(new int[] { 3, 2, 1 })]
        [InlineData(new int[] { 7, 2, 4, 1, 9 })]
        [InlineData(new int[] { 2, 6, 1, 3 })]
        public void AddTest(params int[] array)
        {
            DoublyLinked<int> list = new DoublyLinked<int>();
            for (int i = 0; i < array.Length; i++)
            {
                list.AddNode(array[i]);
            }

            Node<int> previous = list.Head;
            Node<int> current = list.Head;

            for (int i = 0; i < array.Length - 1; i++)
            {
                current = previous.Next;
                Assert.True(previous.value <= current.value);
                previous = current;
            }
        }

        [Theory]
        [InlineData(new int[] { 3, 2, 1 })]
        [InlineData(new int[] { 7, 2, 4, 1, 9 })]
        [InlineData(new int[] { 2, 6, 1, 3 })]
        public void DeleteTest(params int[] array)
        {
            Random random = new Random();
            DoublyLinked<int> list = new DoublyLinked<int>();
            for (int i = 0; i < array.Length; i++)
            {
                list.AddNode(array[i]);
            }

            Node<int> current = list.Head;
            for (int i = 0; i < random.Next(array.Length - 1) - 1; i++)
            {
                current = current.Next;
            }
            list.Remove(current.value);
            int removed = current.value;

            Node<int> previous = list.Head;
            current = list.Head;

            for (int i = 0; i < array.Length - 2; i++)
            {
                current = previous.Next;
                Assert.True(previous.value <= current.value);
                Assert.True(current.value != removed);
                previous = current;
            }
        }
    }
}