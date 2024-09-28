using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Генерация массивов для последовательной сортировки
        int[] array1 = GenerateArray(50000000);
        int[] array2 = GenerateArray(50000000);
        int[] array3 = GenerateArray(50000000);

        // Копирование массивов для параллельной сортировки
        int[] array1Copy = (int[])array1.Clone();
        int[] array2Copy = (int[])array2.Clone();
        int[] array3Copy = (int[])array3.Clone();

        // Замер времени последовательной сортировки
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Array.Sort(array1);
        Array.Sort(array2);
        Array.Sort(array3);

        stopwatch.Stop();
        Console.WriteLine($"Время выполнения последовательной сортировки: {stopwatch.ElapsedMilliseconds} мс");

        // Замер времени параллельной сортировки
        stopwatch.Reset();
        stopwatch.Start();

        // Создание и запуск задач для параллельной сортировки массивов
        Task task1 = Task.Run(() => Array.Sort(array1Copy));
        Task task2 = Task.Run(() => Array.Sort(array2Copy));
        Task task3 = Task.Run(() => Array.Sort(array3Copy));

        Task.WaitAll(task1, task2, task3);

        stopwatch.Stop();
        Console.WriteLine($"Время выполнения параллельной сортировки: {stopwatch.ElapsedMilliseconds} мс");

        Console.WriteLine("Нажмите любую клавишу для завершения...");
        Console.ReadKey();
    }

    static int[] GenerateArray(int size)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next();
        }
        return array;
    }
}
