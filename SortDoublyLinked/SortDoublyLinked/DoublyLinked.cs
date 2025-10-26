using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortDoublyLinked
{
    public class DoublyLinked<T> where T:IComparable<T>
    {
        private Node<T> Sentinal = new Node<T>();
        public Node<T> Head => Sentinal.Next;

        public void AddNode(T value)
        {
            Node<T> previous = Sentinal;
            Node<T> current = Head;

            while(current != null && current.value.CompareTo(value) <= 0)
            {
                previous = current;
                current = current.Next;
            }

            Node<T> addition = new Node<T>(value);

            FormConnections(previous, current, addition);
        }

        public void FormConnections(Node<T> previous, Node<T> current, Node<T> addition)
        {
            addition.Next = current;
            addition.Previous = previous;

            if(current != null)
            {
                current.Previous = addition;
            }

            if (previous == null) return;
            previous.Next = addition;
        }

        public void Remove(T value)
        {
            Node<T> previous = Sentinal;
            Node<T> current = Head;

            while(current.value.CompareTo(value) != 0 && current != null)
            {
                previous = current;
                current = current.Next;
            }

            if(current == null) return;

            RemoveLinks(previous, current);
        }

        public void RemoveLinks(Node<T> previous, Node<T> removed)
        {
            removed.Next.Previous = previous;
            previous.Next = removed.Next;
        }
    }
}
