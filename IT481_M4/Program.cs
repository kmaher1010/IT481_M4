using System.Data;

namespace IT481_M4 {
    internal class Program {
        static void Main(string[] args) {

            var _dataSet = new DataSet();

            RunSort(() => Sorter.BubbleSort(_dataSet.Small), "Bubble Sort Small");
            RunSort(() => Sorter.BubbleSort(_dataSet.Medium), "Bubble Sort Medium");
            RunSort(() => Sorter.BubbleSort(_dataSet.Large), "Bubble Sort Large");

            Console.WriteLine();

            RunSort(() => Sorter.MergeSort(_dataSet.Small), "Merge Sort Small");
            RunSort(() => Sorter.MergeSort(_dataSet.Medium), "Merge Sort Medium");
            RunSort(() => Sorter.MergeSort(_dataSet.Large), "Merge Sort Large");


            Console.WriteLine();

            RunSort(() => Sorter.QuickSort(_dataSet.Small), "Quick Sort Small");
            RunSort(() => Sorter.QuickSort(_dataSet.Medium), "Quick Sort Medium");
            RunSort( () => Sorter.QuickSort(_dataSet.Large), "Quick Sort Large");
        }

        public static void RunSort(Func<List<int>> action, string name) {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var result = action();
            watch.Stop();
            Console.WriteLine($"Running {name} with {result.Count} items in {watch.ElapsedMilliseconds}ms");
        }
    }

    public class DataSet {
        private List<int> _small;
        private List<int> _medium;
        private List<int> _large;
        public DataSet() {
            _small = CreateRandomData(100);
            _medium = CreateRandomData(1000);
            _large = CreateRandomData(10000);
        }

        public List<int> Small => _small.ToList();
        public List<int> Medium => _medium.ToList();
        public List<int> Large => _large.ToList();

        private List<int> CreateRandomData(int size) {
            List<int> data = new List<int>();
            Random rand = new Random();
            for (int i = 0; i < size; i++) {
                data.Add(rand.Next(0, 1000));
            }
            return data;
        }
    }

    public static class Sorter {
        public static List<int> BubbleSort(List<int> data) {
            List<int> sorted = new List<int>(data);
            for (int i = 0; i < sorted.Count; i++) {
                for (int j = 0; j < sorted.Count - 1; j++) {
                    if (sorted[j] > sorted[j + 1]) {
                        int temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }
            return sorted;
        }
        public static List<int> MergeSort(List<int> data) {
            if (data.Count <= 1) {
                return data;
            }

            var left = new List<int>();
            var right = new List<int>();

            int middle = data.Count / 2;
            for (int i = 0; i < middle; i++)  //Dividing the unsorted list
            {
                left.Add(data[i]);
            }
            for (int i = middle; i < data.Count; i++) {
                right.Add(data[i]);
            }

            left = MergeSort(left);
            right = MergeSort(right);
            return Merge(left, right);
        }
        private static List<int> Merge(List<int> left, List<int> right) {
            var result = new List<int>();

            while (left.Count > 0 && right.Count > 0) {
                if (left.First() <= right.First())  //Comparing First two elements to see which is smaller
                {
                    result.Add(left.First());
                    left.Remove(left.First());      //Rest of the list minus the first element
                } else {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }

            while (left.Count > 0)    //If left List has only one element in it
            {
                result.Add(left.First());
                left.Remove(left.First());
            }

            while (right.Count > 0)   //If right List has only one element in it
            {
                result.Add(right.First());
                right.Remove(right.First());
            }

            return result;
        }

        public static List<int> QuickSort(List<int> data) {
            if (data.Count <= 1) {
                return data;
            }

            int pivot = data[data.Count - 1];
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            for (int i = 0; i < data.Count - 1; i++) {
                if (data[i] < pivot) {
                    left.Add(data[i]);
                } else {
                    right.Add(data[i]);
                }
            }

            List<int> sorted = new List<int>();
            sorted.AddRange(QuickSort(left));
            sorted.Add(pivot);
            sorted.AddRange(QuickSort(right));

            return sorted;
        }

    }
}