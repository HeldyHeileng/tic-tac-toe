using System;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static T[,] Populate<T>(this T[,] array, Func<T> provider)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = provider();
                }
            }
            return array;
        }
    }
}