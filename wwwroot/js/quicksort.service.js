window.quickSort = (data) => {
  const result = quickSortRecursive(data, 0, data.length - 1)
  
  return result;
}

function swap(items, leftIndex, rightIndex) {
  let temp = items[leftIndex];
  items[leftIndex] = items[rightIndex];
  items[rightIndex] = temp;
}

// Wählt Pivot Element aus der Mitte und
function partition(items, left, right) {
  let pivot = items[Math.floor((right + left) / 2)]
  let leftPointer = left;
  let rightPointer = right; //right pointer
  while (leftPointer <= rightPointer) {
    while (items[leftPointer] < pivot) {
      leftPointer++;
    }
    while (items[rightPointer] > pivot) {
      rightPointer--;
    }
    if (leftPointer <= rightPointer) {
      swap(items, leftPointer, rightPointer); //sawpping two elements
      leftPointer++;
      rightPointer--;
    }
  }
  return leftPointer;
}

function quickSortRecursive(items, left, right) {
  let index;
  if (items.length > 1) {
    index = partition(items, left, right); //index returned from partition
    if (left < index - 1) { //more elements on the left side of the pivot
      quickSortRecursive(items, left, index - 1);
    }
    if (index < right) { //more elements on the right side of the pivot
      quickSortRecursive(items, index, right);
    }
  }
  return items;
}
