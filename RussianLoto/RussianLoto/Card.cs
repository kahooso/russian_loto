using System;
using System.Collections.Generic;
using System.Linq;

namespace RussianLoto
{
    public class Card
    {
        private static Random random = new Random();

        public int[,] numbers { get; private set; }

        public bool[,] marked { get; private set; }

        public Card()
        {
            numbers = new int[3, 9];
            marked = new bool[3, 9];
            GenerateNumbers();
        }

        public bool IsRowComplete(int rowIndex)
        {
            for (int col = 0; col < 9; col++)
            {
                if (numbers[rowIndex, col] != 0 && !marked[rowIndex, col])
                {
                    // Если есть ненулевое число, которое не отмечено
                    return false;
                }
            }
            return true;
        }

        // Метод генерации уникальных чисел для карты
        private void GenerateNumbers()
        {
            // Генерация 15 уникальных случайных чисел от 1 до 90
            List<int> all_numbers = Enumerable.Range(1, 90).OrderBy(x => random.Next()).Take(15).ToList();

            // Разделение на 3 строки по 5 чисел
            for (int row = 0; row < 3; row++)
            {
                List<int> row_numbers = all_numbers.Skip(row * 5).Take(5).OrderBy(x => x).ToList();
                List<int> empty_positions = Enumerable.Range(0, 9).OrderBy(x => random.Next()).Take(4).ToList();

                int numberIndex = 0;
                for (int col = 0; col < 9; col++)
                {
                    if (empty_positions.Contains(col))
                    {
                        numbers[row, col] = 0;  // Пустое поле
                    }
                    else
                    {
                        numbers[row, col] = row_numbers[numberIndex++];  // Число
                    }
                }
            }
        }

        //public void MarkNumber(int number)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        for (int j = 0; j < 9; j++)
        //        {
        //            if (this.numbers[i, j] == number)
        //            {
        //                this.marked[i, j] = true;
        //            }
        //        }
        //    }
        //}
    }
}