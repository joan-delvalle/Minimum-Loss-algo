using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'minimumLoss' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts LONG_INTEGER_ARRAY price as parameter.
     */

    public static int minimumLoss(List<long> price)
    {
        int n = price.Count;

        // Keep both price and original index (year)
        List<(long value, int index)> pricesWithIndex = new List<(long, int)>();
        for (int i = 0; i < n; i++)
        {
            pricesWithIndex.Add((price[i], i));
        }

        // Sort prices ascending
        pricesWithIndex.Sort((a, b) => a.value.CompareTo(b.value));

        long minLoss = long.MaxValue;

        // Check adjacent pairs in sorted list
        for (int i = 1; i < n; i++)
        {
            long highPrice = pricesWithIndex[i].value;
            long lowPrice = pricesWithIndex[i - 1].value;

            int highIndex = pricesWithIndex[i].index;
            int lowIndex = pricesWithIndex[i - 1].index;

            // We can only "sell after buy", so the lower price must come after the higher price
            if (lowIndex > highIndex)
            {
                long loss = highPrice - lowPrice;
                if (loss > 0)
                    minLoss = Math.Min(minLoss, loss);
            }
        }

        return (int)minLoss;
    }
}



class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<long> price = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(priceTemp => Convert.ToInt64(priceTemp)).ToList();

        int result = Result.minimumLoss(price);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
