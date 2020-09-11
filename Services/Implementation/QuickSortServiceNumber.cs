using BlazorApp.Models;
using BlazorApp.Services.Interfaces;
using System;
using System.Linq;

namespace BlazorApp.Services.Implementation
{
    public class QuickSortServiceNumber: IQuickSortService<int>
	{
		public void sort(ref int[] array, string key)
		{
            quickSortRecursive(ref array, 0, array.Length - 1);
		}

        private int[] quickSortRecursive(ref int[] items, int left, int right)
        {
            var index = 0;
            if (items.Count() > 1)
            {
                index = partition(ref items, left, right); //index returned from partition
                if (left < index - 1)
                { //more elements on the left side of the pivot
                    quickSortRecursive(ref items, left, index - 1);
                }
                if (index < right)
                { //more elements on the right side of the pivot
                    quickSortRecursive(ref items, index, right);
                }
            }
            return items;
        }

        private void swap(ref int[] items,int leftIndex,int rightIndex)
        {
            var temp = items[leftIndex];
            items[leftIndex] = items[rightIndex];
            items[rightIndex] = temp;
        }

        private int partition(ref int[] items, int left, int right)
        {
            var pivot = items[(right + left) / 2];
            var leftPointer = left;
            var rightPointer = right; //right pointer
            while (leftPointer <= rightPointer)
            {
                while (items[leftPointer] < pivot)
                {
                    leftPointer++;
                }
                while (items[rightPointer] > pivot)
                {
                    rightPointer--;
                }
                if (leftPointer <= rightPointer)
                {
                    swap(ref items, leftPointer, rightPointer); //sawpping two elements
                    leftPointer++;
                    rightPointer--;
                }
            }
            return leftPointer;
        }

    }


}
