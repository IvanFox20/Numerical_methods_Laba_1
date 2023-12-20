using System;

class Program
{
    static bool areThereSolutions = true;
    static int n, m;
    
    static void Main()
    {
        Console.WriteLine("Введите количество уравнений (строк матрицы): ");
        n = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество переменных (столбцов матрицы): ");
        m = int.Parse(Console.ReadLine());

        double[][] matrix = new double[n][];
        for (int i = 0; i < n; ++i)
        {
            matrix[i] = new double[m + 1];
        }

        Console.WriteLine("Введите коэффициенты матрицы построчно (по одному коэффициенту в строке):");
        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j <= m; ++j)
            {
                Console.Write($"A[{i + 1},{j + 1}] = ");
                matrix[i][j] = double.Parse(Console.ReadLine());
            }
        }

        double[][] matrixForNevyazka = new double[n][];
        Array.Copy(matrix, matrixForNevyazka, n);

        solveGauss(matrix);
        int rank = m;

        int tmp = 0;
        for (int i = 0; i < n; ++i)
        {
            int cntNullStr = 0;
            for (int j = 0; j <= m; ++j)
            {
                Console.Write(matrix[i][j] + " ");
                if (matrix[i][j] == 0 && j != m)
                    cntNullStr++;
            }

            if (cntNullStr == m)
            {
                tmp++;
                if (matrix[i][m] != 0)
                    areThereSolutions = false;
            }

            Console.WriteLine();
        }

        rank -= tmp;
        printSolution(matrix, rank);
    }

    static void swapRows(double[][] matrix, int row1, int row2)
    {
        double[] temp = matrix[row1];
        matrix[row1] = matrix[row2];
        matrix[row2] = temp;
    }

    static void solveGauss(double[][] matrix)
    {
        int n = matrix.Length;
        int m = matrix[0].Length - 1;

        for (int k = 0; k < n; ++k)
        {
            int firstNonZero = -1;
            for (int i = k; i < n; ++i)
            {
                if (matrix[i][k] != 0)
                {
                    firstNonZero = i;
                    break;
                }
            }

            if (firstNonZero != -1)
            {
                swapRows(matrix, k, firstNonZero);

                double div = matrix[k][k];
                for (int j = k; j <= m; ++j)
                {
                    matrix[k][j] /= div;
                }

                for (int i = 0; i < n; ++i)
                {
                    if (i != k)
                    {
                        double factor = matrix[i][k];
                        for (int j = k; j <= m; ++j)
                        {
                            matrix[i][j] -= factor * matrix[k][j];
                        }
                    }
                }
            }
        }
    }

    static void printSolution(double[][] matrix, int rank)
    {
        if (rank == m)
        {
            Console.WriteLine("Решение системы:");
            for (int i = 0; i < matrix.Length; ++i)
            {
                Console.WriteLine($"X{i + 1} = {matrix[i][matrix[i].Length - 1]}");
            }
        }
        else if (rank < m && areThereSolutions)
        {
            Console.WriteLine("Система имеет бесконечное количество решений.");
            Console.WriteLine("Частное решение:");
            for (int i = 0; i < matrix[0].Length - 1; ++i)
            {
                Console.WriteLine($"X{i + 1} = {(i < rank ? matrix[i][matrix[i].Length - 1] : 0)}");
            }
        }
        else
        {
            Console.WriteLine("Система не имеет решений");
        }
    }
}
