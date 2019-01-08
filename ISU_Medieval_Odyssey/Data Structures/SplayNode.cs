using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    class SplayNode<T> where T : IComparable<T>
    {
        T Key { get; set; }

        SplayNode<T> Left { get; set; }
        SplayNode<T> Right { get; set; }
        SplayNode<T> Parent { get; set; }

        public SplayNode(T key, SplayNode<T> parent)
        {
            Key = key;
            Parent = parent;
        }
    }
}
