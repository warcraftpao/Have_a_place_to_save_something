using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePieces.Codility
{
    public class Solution
    {
        public static int FrogJump(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            var step = new int[A.Length];
            step[0] = A[0];
            step[1] = A[0] + A[1];

            // 第三格子
            if (A.Length >= 3)
            {
                step[2] = A[2] + Math.Max(step[0], step[1]);
            }

            // 第四格子
            if (A.Length >= 4)
            {
                step[3] = A[3] + Math.Max(step[0], Math.Max(step[1], step[2]));
            }

            //第五格子
            if (A.Length >= 5)
            {
                step[4] = A[4] + Math.Max(step[0], Math.Max(step[1], Math.Max(step[2], step[3])));
            }
            //第六格子
            if (A.Length >= 6)
            {
                step[5] = A[5] + Math.Max(step[0], Math.Max(step[1], Math.Max(step[2], Math.Max(step[4], step[3]))));
            }

            for (var i = 6; i < A.Length; i++)
            {
                step[i] = A[i] + Math.Max(step[i - 6], Math.Max(step[i - 5], Math.Max(step[i - 4], Math.Max(step[i - 3], Math.Max(step[i - 1], step[i - 2])))));
            }

            return step[A.Length - 1];
        }

        public static int OddOccurrencesInArray(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            var dic = new Dictionary<int, int>();
            for (var i = 0; i < A.Length; i++)
            {
                if (!dic.ContainsKey(Math.Abs(A[i])))
                {
                    dic.Add(A[i], 1);
                }
            }

            return dic.Count;
        }

        public static int MaxSliceSum(int M, int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            if (M == 0)
            {
                return A.Length;
            }
            var count = 0;
            var head = 0;
            var dic = new Dictionary<int, int>();
            for (var i = 0; i < A.Length; i++)
            {
                if (!dic.ContainsKey(A[i]))
                {
                    dic.Add(A[i], i);
                }
                else
                {
                    //有重复的，
                    var tail = dic[A[i]];
                    var len = tail - head + 1;
                    count += len * (len + 1) / 2;
                    dic.Clear();
                    for (var j = tail + 1; j <= i; j++)
                    {
                        dic.Add(A[j], j);
                    }
                    head = tail + 1;
                }
            }
            var len1 = dic.Count;
            count += len1 * (len1 + 1) / 2;

            return Math.Min(1000000000, count);
        }
        public int MinAbs(int[] A)
        {
            Array.Sort(A);

            var result = int.MaxValue;
            var left = 0;
            var right = A.Length - 1;

            while (left <= right)
            {
                //最左边加最右边的值，左边可能是负数，右边应该是正数
                //绝对值最小的是0
                //所以当和是负数的时候，说明负的厉害，坐标前进，否则后退
                var curr = A[left] + A[right];
                result = Math.Min(Math.Abs(curr), result);

                if (curr <= 0)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="X"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static int Frogoneriver(int X, int[] A)
        {
            //一个长度x的数组，代表某个位子是否会被覆盖
            //x代表计数器，某个位子被覆盖了。x就-1.当x=0的时候的时间点肯定是就是要的值
            bool[] flagArr = new bool[X];
            for (int i = 0; i < A.Length; i++)
            {
                if (flagArr[A[i] - 1] == false)
                {
                    flagArr[A[i] - 1] = true;
                    X--;
                }
                if (X == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public static int Distinct(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            var dic = new Dictionary<int, int>();
            for (var i = 0; i < A.Length; i++)
            {
                if (dic.ContainsKey(A[i]))
                {
                    dic[A[i]]++;
                }
                else
                {
                    dic.Add(A[i], 1);
                }
            }
            var keys = new List<int>(dic.Keys);
            var count = 0;
            for (var i = 0; i < keys.Count; i++)
            {
                if (dic[keys[i]] == 1)
                {
                    count++;
                }
            }
            return count;
        }


        public static int MissingElement(int[] A)
        {
            Array.Sort(A);
            int i = 0;
            while (A[i] <= 0 && i < A.Length - 1)
            {
                i++;

            }

            if (i == A.Length - 1)
                return 1;

            var num1 = 1;
            if (A[i] != 1)
                return 1;

            for (var j = i + 1; j < A.Length - 1; j++)
            {
                if (A[j] - A[j - 1] >= 2)
                    return A[j - 1] + 1;

            }
            return A[A.Length - 1] + 1;
        }

        


    }
}




 
