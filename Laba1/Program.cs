using System;
 
namespace Laba1
{
    class Program
    {
        static void Main()
    {
        Console.WriteLine("Введите количество уравнений (строк матрицы):");
        int n = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество переменных (столбцов матрицы):");
        int m = int.Parse(Console.ReadLine());

        double[,] matrix = new double[n, m + 1]; // Расширенная матрица (коэффициенты + столбец свободных членов)

        Console.WriteLine("Введите коэффициенты матрицы построчно (по одному коэффициенту в строке):");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m + 1; j++)
            {
                Console.Write($"A[{i + 1},{j + 1}] = ");
                matrix[i, j] = double.Parse(Console.ReadLine());
            }
        }

        // Решение СЛАУ методом Гаусса
        int rank = SolveGauss(matrix, n, m);

        // Вывод решения
        PrintSolution(matrix, rank);
    }

    static int SolveGauss(double[,] matrix, int n, int m)
    {
        int rank = 0;

        // Прямой ход
        for (int k = 0; k < n; k++)
        {
            // Поиск первого ненулевого элемента в текущем столбце
            int firstNonZero = -1;
            for (int i = k; i < n; i++)
            {
                if (matrix[i, k] != 0)
                {
                    firstNonZero = i;
                    break;
                }
            }

            if (firstNonZero != -1)
            {
                // Обмен текущей строки с строкой, содержащей первый ненулевой элемент
                SwapRows(matrix, k, firstNonZero);

                // Нормализация строки
                double div = matrix[k, k];
                for (int j = k; j < m + 1; j++)
                {
                    matrix[k, j] /= div;
                }

                // Вычитание текущей строки из остальных строк
                for (int i = 0; i < n; i++)
                {
                    if (i != k)
                    {
                        double factor = matrix[i, k];
                        for (int j = k; j < m + 1; j++)
                        {
                            matrix[i, j] -= factor * matrix[k, j];
                        }
                    }
                }

                rank++;
            }
        }

        return rank;
    }
    
    static bool IsZeroVector(double[] v)
    {
        foreach (var x in v)
        {
            if (x != 0)
            {
                return false;
            }
        }
        return true;
    }

    static void PrintSolution(double[,] matrix, int rank)
    {
        if (rank == matrix.GetLength(0))
        {
            Console.WriteLine("Решение системы:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine($"X{i + 1} = {matrix[i, matrix.GetLength(1) - 1]}");
            }
        }
        else
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double[] copyV = new double[matrix.GetLength(1) - 1];
                for (int j = 0; j < copyV.Length; j++)
                {
                    copyV[j] = matrix[i, j];
                }

                Console.Write(string.Join(", ", copyV) + " " + matrix[i, matrix.GetLength(1) - 1] + "\n");

                if (IsZeroVector(copyV) && matrix[i, matrix.GetLength(1) - 1] != 0)
                {
                    Console.WriteLine("Решений нет");
                    return;
                }
            }
            Console.WriteLine("Система имеет бесконечное количество решений.");
            Console.WriteLine("Частное решение:");
            for (int i = 0; i < matrix.GetLength(1) - 1; i++)
            {
                Console.WriteLine($"X{i + 1} = {(i < rank ? matrix[i, matrix.GetLength(1) - 1] : 0)}");
            }
        }
    }

    static void SwapRows(double[,] matrix, int row1, int row2)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            double temp = matrix[row1, j];
            matrix[row1, j] = matrix[row2, j];
            matrix[row2, j] = temp;
        }
    }    
    }
}