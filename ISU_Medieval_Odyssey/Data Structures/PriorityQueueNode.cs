// Author: Joon Song
// File Name: PriorityQueueNode.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold PriorityQueue object

namespace ISU_Medieval_Odyssey
{
    public sealed class PriorityQueueNode<T>
    {
        /// <summary>
        /// The <see cref="T"/>-typed value of this <see cref="PriorityQueueNode{T}"/>
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// The next <see cref="PriorityQueueNode{T}"/> to this <see cref="PriorityQueueNode{T}"/>
        /// </summary>
        public PriorityQueueNode<T> Next { get; set; }

        /// <summary>
        /// Constructor for <see cref="PriorityQueueNode{T}"/> object
        /// </summary>
        /// <param name="value">The value assosiated with this <see cref="PriorityQueueNode{T}"/></param>
        public PriorityQueueNode(T value)
        {
            // Setting priority queue node value
            Value = value;
        }
    }
}
