using System.Collections.Generic;

namespace JobProject
    {
    public static class Calculate
        {
        public static double GetMedianNumber(List<ulong> numbersList)
            {
            numbersList.Sort();
            var countNumbers = numbersList.Count;
            if (countNumbers % 2 == 0)
                {
                var result = ((double)numbersList[(countNumbers / 2 - 1)] + numbersList[countNumbers / 2]) / 2;
                Log.Logging(result.ToString(), "result.txt");
                return result;
                }
            else
                {
                var result = (double)numbersList[countNumbers / 2];
                Log.Logging(result.ToString(), "result.txt");
                return result;
                }
            }

        public static int[,] TaskCalculate(int maxNumbers, int countThread)
            {
            var rangeStart = 1;
            double rangeNumber = maxNumbers / countThread;
            var rangeFinalNumber = maxNumbers / countThread;
            if (maxNumbers % countThread == 0)
                {
                var calculatedThreadNumberSent = new int[countThread, 2];
                for (int i = 0; i < countThread; i++)
                    {
                    calculatedThreadNumberSent[i, 0] = rangeStart;
                    calculatedThreadNumberSent[i, 1] = rangeFinalNumber;
                    rangeStart += (int)rangeNumber;
                    rangeFinalNumber += (int)rangeNumber;
                    }
                return calculatedThreadNumberSent;
                }
            else
                {
                var modulo = maxNumbers % countThread;
                var calculatedThreadNumberSent = new int[countThread + 1, 2];
                calculatedThreadNumberSent[0, 0] = 1;
                calculatedThreadNumberSent[0, 1] = modulo;
                rangeStart = modulo + 1;
                maxNumbers -= modulo;
                rangeFinalNumber = (maxNumbers / countThread) + modulo;

                for (int i = 1; i < countThread + 1; i++)
                    {
                    calculatedThreadNumberSent[i, 0] = rangeStart;
                    calculatedThreadNumberSent[i, 1] = rangeFinalNumber;
                    rangeStart += (int)rangeNumber;
                    rangeFinalNumber += (int)rangeNumber;
                    }
                return calculatedThreadNumberSent;
                }
            }
        }
    }