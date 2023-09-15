namespace CloneTest
{
    internal class Quicksort
    {
        //Finalnie kod sortowania nie jest używany
        public static List<Tuple<int, int, int>> SortList(List<Tuple<int, int, int>> arr, int startIndex, int endIndex)
        {

            int i = startIndex;
            int j = endIndex;

            int pivot = arr[endIndex].Item3;

            while (i <= j) {
                while (arr[i].Item3 < pivot) {
                    i++;
                }

                while (arr[j].Item3 > pivot) {
                    j--;
                }

                if (i <= j) {
                    Tuple<int, int, int> temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    i++;
                    j--;
                }
            }

            if (startIndex < j) {
                SortList(arr, startIndex, j);
            }

            if (i < endIndex) {
                SortList(arr, i, endIndex);
            }

            return arr;
        }
    }

    internal class BinarySearch {
        public static int SearchClosest(List<double> list, double key)
        {
            int low = 0;
            int high = list.Count() - 1;

            while (high != low + 1) {
                int midIndex = low + (high - low) / 2;

                if (list[midIndex] == key) {
                    return midIndex;
                }
                else if (list[midIndex] < key) {
                    low = midIndex;
                }
                else {
                    high = midIndex;
                }

            }

            if (list[low] < key && list[high] > key) {
                return low;
            }

            return -1;
        }

    }
}



