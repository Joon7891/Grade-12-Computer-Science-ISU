// Author: Joon Song

using System;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    public sealed class PriorityQueue<T>
    {
        /// <summary>
        /// The delegate to determine which elements to prioritize in the <see cref="PriorityQueue{T}"/>
        /// </summary>
        /// <param name="a">The first element</param>
        /// <param name="b">The second element</param>
        /// <returns>Whether the first element 'a' should take priority over the second element 'b'</returns>
        public delegate bool Comparator(T a, T b);
        private readonly Comparator comparator;

        /// <summary>
        /// The size of this <see cref="PriorityQueue{T}"/>
        /// </summary>
        public int Size { get; private set; } = 0;

        // The head node/element with higtest priority in this priorty queue
        private PriorityQueueNode<T> headNode = null;

        /// <summary>
        /// Constructor for <see cref="PriorityQueue{T}"/> object
        /// </summary>
        /// <param name="comparator">The <see cref="Comparator"/> for this <see cref="PriorityQueue{T}"/></param>
        public PriorityQueue(Comparator comparator)
        {
            // Setting class comparator and constructing LinkedList
            this.comparator = comparator;
        }

        /// <summary>
        /// Subprogram to enqueue an object into the <see cref="PriorityQueue{T}"/>
        /// </summary>
        /// <param name="item">The item to insert into the <see cref="PriorityQueue{T}"/></param>
        public void Enqueue(T item)
        {
            // Various PQ nodes to be modified/inserted
            PriorityQueueNode<T> currentNode;
            PriorityQueueNode<T> newNode;
            
            // Adding item to front of PQ if PQ is empty
            if (Size == 0)
            {
                headNode = new PriorityQueueNode<T>(item);
            }
            else
            {
                // Initializing current node as the head node
                currentNode = headNode;
                newNode = new PriorityQueueNode<T>(item);

                // Traversing down the PQ to determine the appropriate place to place the item
                while (currentNode.Next != null && comparator(item, currentNode.Next.Value))
                {
                    currentNode = currentNode.Next;
                }

                // Inserting item into appropraiate location in PQ
                newNode.Next = currentNode.Next;
                currentNode.Next = newNode;
            }

            // Incrementing size
            ++Size;
        }

        public void Output()
        {
            if (headNode == null) return;

            PriorityQueueNode<T> start = headNode;
            int counter = 0;
            Console.WriteLine(counter + " " +(start.Value as TileNode).Distance);

            while (start.Next != null)
            {
                start = start.Next;
                counter += 1;
                Console.WriteLine(counter + " " + (start.Value as TileNode).Distance);
            }
        }

        /// <summary>
        /// Subprogram to dequeue the item in front of the <see cref="PriorityQueue{T}"/>
        /// </summary>
        /// <returns>The <see cref="T"/> item in front of the <see cref="PriorityQueue{T}"/></returns>
        public T Dequeue()
        {
            // Setting the item as the default value of the PQ type
            T item = default(T);

            // Retrieving item in front and removing front node
            if (headNode != null)
            {
                item = headNode.Value;
                headNode = headNode.Next;
                --Size;
            }

            // Returning the item in front of the queue
            return item;
        }

        /// <summary>
        /// Subprogram to peek the front of the <see cref="PriorityQueue{T}"/>
        /// </summary>
        /// <returns>The <see cref="T"/> item in front of the <see cref="PriorityQueue{T}"/></returns>
        public T Peek() => headNode == null ? default(T) : headNode.Value;

        /// <summary>
        /// Determines whether this <see cref="PriorityQueue{T}"/> is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => headNode == null;
    }
}
