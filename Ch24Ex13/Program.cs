using static System.Console;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ch24Ex13
{
    internal static class Program
    {
        private static int[] _data;

        private static void MyTransform(int i)
        {
            _data[i] = _data[i] / 10;

            if (_data[i] < 1000)
            {
                _data[i] = 0;
            }
            if (_data[i] > 1000 & _data[i] < 2000)
            {
                _data[i] = 100;
            }
            if (_data[i] > 2000 & _data[i] < 3000)
            {
                _data[i] = 200;
            }
            if (_data[i] > 3000)
            {
                _data[i] = 300;
            }
        }

        private static void Main()
        {
            WriteLine("Основной поток выполнения запущен.");

            // Таймер для засечек времени. 
            var sw = new Stopwatch();

            _data = new int[100000000];

            sw.Start(); // Засечка времени выполнения 01.

            // Параллельный вариант инициализации массива в цикле.
            Parallel.For(0, _data.Length, i => _data[i] = i);
            sw.Stop(); // Остановка секундромера 01.

            WriteLine($"Параллельно выполняемый цикл инициализации массива: {sw.Elapsed.TotalSeconds} секунд");

            sw.Reset(); // Обнуление (сброс) таймера.
            sw.Start(); // Засечка времени выполнения 02.

            // Последовательный вариант инициализации массива в цикле.
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = i;
            }

            sw.Stop(); // Остановка секундромера 02.

            WriteLine($"Последовательно выполняемый цикл инициализации массива: {sw.Elapsed.TotalSeconds} секунд");

            WriteLine();

            // Выполнение преобразований.

            sw.Reset(); // Обнуление (сброс) таймера.
            sw.Start(); // Засечка времени выполнения 03.

            // Параллельный вариант преобразования данных массива в цикле.
            Parallel.For(0, _data.Length, MyTransform);
            sw.Stop(); // Остановка секундромера 03.

            WriteLine($"Параллельно выполняемый цикл преобразования массива: {sw.Elapsed.TotalSeconds} секунд");

            sw.Reset(); // Обнуление (сброс) таймера.
            sw.Start(); // Засечка времени выполнения 04.

            // Последовательный вариант преобразования данных массива в цикле.
            for (var i = 0; i < _data.Length; i++)
            {
                MyTransform(i);
            }

            sw.Stop(); // Остановка секундромера 04.

            WriteLine($"Последовательно выполняемый цикл преобразования массива: {sw.Elapsed.TotalSeconds} секунд");

            WriteLine("Основной поток выполнения завершён.");
        }
    }
}
