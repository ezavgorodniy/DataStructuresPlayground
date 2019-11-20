using System;

namespace Heaps.Base
{
    public abstract class BaseIntHeap
    {
        private const int InitialCapacity = 10;

        protected int Capacity = InitialCapacity;
        protected int Size = 0;
        protected int[] Items = new int[InitialCapacity];

        protected int GetLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }
        protected int GetRightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 2;
        }

        protected int GetParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        protected bool HasLeftChild(int index)
        {
            return GetLeftChildIndex(index) < Size;
        }

        protected bool HasRightChild(int index)
        {
            return GetRightChildIndex(index) < Size;
        }

        protected bool HasParent(int index)
        {
            return GetParentIndex(index) >= 0;
        }

        protected int LeftChild(int index)
        {
            return Items[GetLeftChildIndex(index)];
        }

        protected int RightChild(int index)
        {
            return Items[GetRightChildIndex(index)];
        }

        protected int Parent(int index)
        {
            return Items[GetParentIndex(index)];
        }

        protected abstract void HeapifyDown();

        protected abstract void HeapifyUp();

        protected void Swap(int indexOne, int indexTwo)
        {
            var tmp = Items[indexOne];
            Items[indexOne] = Items[indexTwo];
            Items[indexTwo] = tmp;
        }

        protected void EnsureExtraCapacity()
        {
            if (Size != Capacity)
            {
                return;
            }

            Array.Resize(ref Items, Capacity * 2);
            Capacity *= 2;
        }

        public int Peek()
        {
            if (Size == 0)
            {
                throw new Exception("Empty heap");
            }

            return Items[0];
        }

        public int Pop()
        {
            if (Size == 0)
            {
                throw new Exception("Empty heap");
            }

            int item = Items[0];
            Items[0] = Items[Size - 1];
            Size--;

            HeapifyDown();

            return item;
        }

        public void Push(int item)
        {
            EnsureExtraCapacity();
            Items[Size] = item;
            Size++;
            HeapifyUp();
        }
    }
}
