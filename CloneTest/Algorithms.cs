namespace CloneTest;

internal class QuickSort
{
    /*
    public static Dictionary<Tuple<int, int, int, int>, int> SortList(Dictionary<Tuple<int, int, int, int>, int> arr, int startIndex, int endIndex)
    {

        int i = startIndex;
        int j = endIndex;

        int pivot = arr[endIndex].Value;

        while (i <= j) {
            while (arr[i].Value < pivot) {
                i++;
            }

            while (arr[j].Value > pivot) {
                j--;
            }

            if (i <= j) {
                Tuple<int, int, int, int> temp = arr[i].Key;
                arr[i].Key = arr[j].Key;
                arr[j].Key = temp;
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
    */
}
    

internal class BinarySearch {
    public static int SearchClosest(List<double> list, double key)
    {
        int low = 0;
        int high = list.Count - 1;

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



