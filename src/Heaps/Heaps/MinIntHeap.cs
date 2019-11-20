using Heaps.Base;

namespace Heaps
{
    public class MinIntHeap : BaseIntHeap
    {
        protected override void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                int smallerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && RightChild(index) < LeftChild(index))
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (Items[index] < Items[smallerChildIndex])
                {
                    break;
                }

                Swap(index, smallerChildIndex);
                index = smallerChildIndex;
            }
        }

        protected override void HeapifyUp()
        {
            int index = Size - 1;
            while (HasParent(index) && Parent(index) > Items[index])
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }
    }
}
