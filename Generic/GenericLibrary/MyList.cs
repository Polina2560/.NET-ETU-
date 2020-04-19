using System;
using System.Collections.Generic;
using System.Collections;

namespace GenericLibrary
{

    public class MyList<T> : IEnumerable<T>  // односвязный список
    {
        Node<T> head;
        Node<T> tail; 
        int count;  

        // Добавление элемента
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
                head = node;
            else
                tail.Next = node;
            tail = node;

            count++;
        }

        //Удаление элемена
        public bool Remove(T data)
        {
            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        head = head.Next;

                        if (head == null)
                            tail = null;
                    }
                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            return false;
        }

        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }

        //Очистка списка
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        // Проверка присутствия элемента в списке
        public bool Contains(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        // Проверка присутствия элемента в списке
        public void Reverse()
        {
            if (head == null)
                return;

            Node<T> prev = null, current = head, next = null;

            while (current.Next != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }

            current.Next = prev;
            head = current;
        }

        //Добавить в начало списка
        public void AppendFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            node.Next = head;
            head = node;
            if (count == 0)
                tail = head;
            count++;
        }

        //Получить список
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
