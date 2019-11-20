using Heaps.Base;

namespace Heaps
{
    public class MaxIntHeap : BaseIntHeap
    {
        protected override void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                int largerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && RightChild(index) > LeftChild(index))
                {
                    largerChildIndex = GetRightChildIndex(index);
                }

                if (Items[index] > Items[largerChildIndex])
                {
                    break;
                }

                Swap(index, largerChildIndex);
                index = largerChildIndex;
            }
        }

        protected override void HeapifyUp()
        {
            int index = Size - 1;
            while (HasParent(index) && Parent(index) < Items[index])
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }
    }
}
