using System;
using System.Collections.Generic;

namespace J.LeetCodeTest
{
    public interface ISolution
    {
        void Test();
    }

    #region

    public class LongestPalindrome : ISolution
    {
        public void Test()
        {
            throw new NotImplementedException();
        }

        private string Solution(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            int[] result = new int[s.Length];
            for (int i = 1; i < s.Length; ++i)
            {
                int last = result[i - 1];
                if (s[i] == s[i - result[i] - 1])
                    result[i] = result[i - 1] + 1;
            }
            return null;
        }
    }
    #endregion

    #region 寻找两个有序数组的中位数
    /// <summary>
    /// https://leetcode-cn.com/problems/median-of-two-sorted-arrays/
    /// 2019.6.5
    /// </summary>
    public class FindMedianSortedArrays : ISolution
    {
        private float Solution(int[] nums1, int[] nums2)
        {
            int idx = 0;
            int idx1 = 0;
            int idx2 = 0;
            int target = 0;
            int count = nums1.Length + nums2.Length;
            target = count / 2;

            //偶数
            if ((count & 1) == 0)
            {
                int current1 = 0;
                int current2 = 0;
                for (; idx <= target; ++idx)
                {
                    int n1 = idx1 < nums1.Length ? nums1[idx1] : int.MaxValue;
                    int n2 = idx2 < nums2.Length ? nums2[idx2] : int.MaxValue;

                    if (n1 > n2)
                        ++idx2;
                    else
                        ++idx1;

                    if (idx == target - 1)
                        current1 = Math.Min(n1, n2);
                    if (idx == target)
                        current2 = Math.Min(n1, n2);
                }

                return (current1 + current2) / 2f;
            }
            else
            {
                for (; idx <= target; ++idx)
                {
                    int n1 = idx1 < nums1.Length ? nums1[idx1] : int.MaxValue;
                    int n2 = idx2 < nums2.Length ? nums2[idx2] : int.MaxValue;

                    if (n1 > n2)
                        ++idx2;
                    else
                        ++idx1;

                    if (idx == target)
                        return Math.Min(n1, n2);
                }
                return 0;
            }
        }

        public void Test()
        {
            int[] nums1 = new int[] { 0 };
            int[] nums2 = new int[] { 1, 3 };

            Console.WriteLine(Solution(nums1, nums2));
            Console.ReadLine();
        }
    }
    #endregion

    #region 单调递增的数字
    /// <summary>
    /// https://leetcode-cn.com/problems/monotone-increasing-digits/description/
    /// 2018.9.13
    /// </summary>
    public class MonotoneIncreasingDigits : ISolution
    {
        private int Solution(int N)
        {
            if (N < 0)
                throw new Exception("N error:" + N.ToString());
            while (true)
            {
                int n = N;
                int last = n % 10;
                n /= 10;
                while (n > 0)
                {
                    int cur = n % 10;
                    if (cur > last)
                        break;
                    last = cur;
                    n /= 10;
                }
                if (n == 0)
                    return N;
                --N;
            }
        }

        public void Test()
        {
            Console.WriteLine(Solution(777616726));
            Console.ReadLine();
        }
    }
    #endregion

    #region 三维形体的表面积
    /// <summary>
    /// https://leetcode-cn.com/problems/surface-area-of-3d-shapes/description/
    /// 2018.9.13
    /// </summary>
    public class SurfaceArea : ISolution
    {
        private int Solution(int[][] grid)
        {
            if (grid == null || grid.Length == 0)
                return 0;

            int s = 0;
            int line = grid.Length;
            int row = grid[0].Length;
            for (int i = 0; i < line; ++i)
            {
                for (int j = 0; j < row; ++j)
                {
                    int num = grid[i][j];
                    if (num <= 0)
                        continue;
                    while (num > 0)
                    {
                        //上
                        if (num == grid[i][j])
                            ++s;
                        //下
                        if (num == 1)
                            ++s;
                        //左
                        if (j == 0 || grid[i][j - 1] < num)
                            ++s;
                        //右
                        if (j == row - 1 || grid[i][j + 1] < num)
                            ++s;
                        //前
                        if (i == 0 || grid[i - 1][j] < num)
                            ++s;
                        //后
                        if (i == line - 1 || grid[i + 1][j] < num)
                            ++s;
                        --num;
                    }
                }
            }
            return s;
        }

        public void Test()
        {
            int[][] grid = new int[][]
            {
                new int[2] { 1,0 },
                new int[2] { 0,2 },
            };
            Console.WriteLine(Solution(grid));
            Console.ReadLine();
        }
    }
    #endregion

    #region 复制带随机指针的链表

    /// <summary>
    /// https://leetcode-cn.com/problems/copy-list-with-random-pointer/description/
    /// 2018.9.11
    /// </summary>
    public class CopyRandomList : ISolution
    {
        private class RandomListNode
        {
            public int label;
            public RandomListNode next, random;
            public RandomListNode(int x)
            {
                label = x;
            }
        }

        /// <summary>
        /// 1.先在原链表每个结点后插入一个新的复制结点，原结点next指向新结点
        /// 2.所有新结点random等于原结点.random.next
        /// 3.断开新旧两个链表
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        private RandomListNode Solution(RandomListNode head)
        {
            if (head == null)
                return null;
            RandomListNode current = head;
            while (current != null)
            {
                RandomListNode node = new RandomListNode(current.label);
                node.next = current.next;
                current.next = node;
                current = node.next;
            }
            current = head;
            while (current != null)
            {
                if (current.random != null)
                    current.next.random = current.random.next;
                current = current.next.next;
            }

            current = head;
            RandomListNode newHead = head.next;
            while (current != null)
            {
                RandomListNode node = current.next;
                current.next = node.next;
                if (node.next != null)
                    node.next = node.next.next;
                current = current.next;
            }
            return newHead;
        }

        public void Test()
        {
            var node1 = new RandomListNode(1);
            var node2 = new RandomListNode(2);
            var node3 = new RandomListNode(3);
            var node4 = new RandomListNode(4);
            var node5 = new RandomListNode(5);
            node1.next = node2;
            node2.next = node3;
            node3.next = node4;
            node4.next = node5;
            node1.random = node3;
            node2.random = node3;
            node3.random = null;
            node4.random = node4;
            node5.random = node1;
            var current = node1;
            while (current != null)
            {
                string info = current.label.ToString();
                if (current.next != null)
                    info += current.next.label.ToString();
                else
                    info += " ";
                if (current.random != null)
                    info += current.random.label.ToString();
                else
                    info += " ";
                Console.WriteLine(info);
                current = current.next;
            }
            Console.WriteLine("===================");
            var head = Solution(node1);
            current = head;
            while (current != null)
            {
                string info = current.label.ToString();
                if (current.next != null)
                    info += current.next.label.ToString();
                else
                    info += " ";
                if (current.random != null)
                    info += current.random.label.ToString();
                else
                    info += " ";
                Console.WriteLine(info);
                current = current.next;
            }
            Console.ReadLine();
        }
    }
    #endregion

    #region 下一个更大元素 II
    /// <summary>
    /// https://leetcode-cn.com/problems/next-greater-element-ii/description/
    /// 2018.9.7
    /// </summary>
    public class NextGreaterElements : ISolution
    {
        private int[] Solution(int[] nums)
        {
            if (nums == null)
                return null;
            if (nums.Length == 0)
                return new int[0];
            int[] greaters = new int[nums.Length];
            for (int i = 0; i < nums.Length; ++i)
            {
                greaters[i] = nums[i];
                for (int j = 1; j < nums.Length; ++j)
                {
                    int idx = (i + j) % nums.Length;
                    if (nums[idx] > nums[i])
                    {
                        greaters[i] = nums[idx];
                        break;
                    }

                }
                if (greaters[i] == nums[i])
                    greaters[i] = -1;
            }
            return greaters;
        }

        public void Test()
        {
            int[] nums = new int[3] { 1, 2, 1 };
            var result = Solution(nums);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
    #endregion

    #region 三角形最小路径和
    /// <summary>
    /// https://leetcode-cn.com/problems/triangle/description/
    /// 2018.9.6
    /// </summary>
    public class MinimumTotal : ISolution
    {
        /// <summary>
        /// 动态规划，通过一个有两行的二维数组暂存结果，计算当前行依赖于上一行的结果，从上而下依次交替计算
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        private int Solution1(List<List<int>> triangle)
        {
            if (triangle == null || triangle.Count == 0)
                throw new Exception("Triangle error");

            int longest = triangle[triangle.Count - 1].Count;
            int[,] sums = new int[2, longest];
            sums[0, 0] = triangle[0][0];
            for (int i = 1; i < triangle.Count; ++i)
            {
                var row = triangle[i];
                int current = i % 2;
                int last = (i + 1) % 2;
                for (int j = 0; j < row.Count; ++j)
                {
                    if (j == 0)
                    {
                        sums[current, j] = sums[last, 0] + row[j];
                    }
                    else if (j == row.Count - 1)
                    {
                        sums[current, j] = sums[last, row.Count - 2] + row[j];
                    }
                    else
                    {
                        if (sums[last, j - 1] > sums[last, j])
                            sums[current, j] = sums[last, j] + row[j];
                        else
                            sums[current, j] = sums[last, j - 1] + row[j];
                    }
                }
            }
            int idx = (triangle.Count - 1) % 2;
            int min = sums[idx, 0];
            for (int i = 1; i < longest; ++i)
            {
                if (sums[idx, i] < min)
                    min = sums[idx, i];
            }
            return min;
        }

        /// <summary>
        /// 动态规划，倒序寻找路径。先复制最后一行，从倒数第二行开始计算
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        private int Solution2(List<List<int>> triangle)
        {
            if (triangle == null || triangle.Count == 0)
                throw new Exception("Triangle error");

            int[] sums = new int[triangle.Count];
            var lastLine = triangle[triangle.Count - 1];
            for (int i = 0; i < triangle.Count; ++i)
            {
                sums[i] = lastLine[i];
            }

            for (int i = triangle.Count - 2; i > -1; --i)
            {
                for (int j = 0; j <= i; ++j)
                {
                    sums[j] = (sums[j] < sums[j + 1] ? sums[j] : sums[j + 1]) + triangle[i][j];
                }
            }
            return sums[0];
        }

        public void Test()
        {
            //int[] nums = new int[10] { 2, 3, 4, 6, 5, 7, 4, 1, 8, 3 };
            List<List<int>> triangle = new List<List<int>>();
            triangle.Add(new List<int>() { 2 });
            triangle.Add(new List<int>() { 3, 4 });
            triangle.Add(new List<int>() { 6, 5, 7 });
            triangle.Add(new List<int>() { 4, 1, 8, 3 });
            Console.WriteLine(Solution2(triangle));
            Console.ReadLine();
        }
    }
    #endregion

    #region 不同路径 II
    /// <summary>
    /// https://leetcode-cn.com/problems/unique-paths-ii/description/
    /// 2018.9.6
    /// </summary>
    public class UniquePathsWithObstacles : ISolution
    {
        private int Solution(int[,] obstacleGrid)
        {
            if (obstacleGrid == null || obstacleGrid.Length == 0)
                throw new Exception("Data error");
            if (obstacleGrid[0, 0] == 1)
                return 0;

            int m = obstacleGrid.GetLength(0);
            int n = obstacleGrid.GetLength(1);
            int[,] paths = new int[m, n];
            paths[0, 0] = 1;
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == 0 && j == 0)
                        continue;

                    if (obstacleGrid[i, j] == 1)
                    {
                        paths[i, j] = 0;
                        continue;
                    }
                    if (i == 0)
                        paths[i, j] = paths[i, j - 1] == 0 ? 0 : 1;
                    else if (j == 0)
                        paths[i, j] = paths[i - 1, j] == 0 ? 0 : 1;
                    else
                        paths[i, j] = paths[i - 1, j] + paths[i, j - 1];
                }
            }
            return paths[m - 1, n - 1];
        }

        public void Test()
        {
            //             int[,] obstacleGrid = new int[1, 2]
            //                 {
            //                     {0,1}
            //                 };
            int[,] obstacleGrid = new int[3, 3]
                {
                    { 0, 0, 0 },{0, 1, 0 },{0, 0, 0 }
                };
            Console.WriteLine(Solution(obstacleGrid));
            Console.ReadLine();
        }
    }
    #endregion

    #region 不同路径
    /// <summary>
    /// https://leetcode-cn.com/problems/unique-paths/description/
    /// 2018.9.5
    /// </summary>
    public class UniquePaths : ISolution
    {
        private int Solution(int m, int n)
        {
            if (m <= 0 || n <= 0)
                throw new Exception("Number error");
            int[,] paths = new int[m, n];
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == 0 || j == 0)
                        paths[i, j] = 1;
                    else
                        paths[i, j] = paths[i - 1, j] + paths[i, j - 1];
                }
            }
            return paths[m - 1, n - 1];
        }

        public void Test()
        {
            Console.WriteLine(Solution(7, 3));
            Console.ReadLine();
        }
    }
    #endregion

    #region  最大子序和

    /// <summary>
    /// https://leetcode-cn.com/problems/maximum-subarray/description/
    /// 2018.9.5
    /// </summary>
    public class MaxSubArray : ISolution
    {
        private int Solution(int[] nums)
        {
            if (nums == null)
                throw new Exception("Array is null");
            int last = nums[0];
            int max = nums[0];
            for (int i = 1; i < nums.Length; ++i)
            {
                int sum = last + nums[i];
                last = nums[i];
                if (sum > nums[i])
                    last = sum;

                if (last > max)
                    max = last;
            }
            return max;
        }

        public void Test()
        {
            int[] nums = new int[2] { -2, 1 };
            Console.WriteLine(Solution(nums));
            Console.ReadLine();
        }
    }

    #endregion

    #region 最长上升子序列

    /// <summary>
    /// https://leetcode-cn.com/problems/longest-increasing-subsequence/description/
    /// 2018.9.4
    /// </summary>
    public class LengthOfLIS : ISolution
    {
        /* 
         * 动态规划：
         * 1.最优子结构
         * 2.边界
         * 3.状态转移方程
         */
        /// <summary>
        /// 以每一个元素为结尾的最长子序列，等于其前面的比其小的最长子序列加1
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private int Solution(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            int[] l = new int[nums.Length];
            int max = 0;
            for (int i = 0; i < nums.Length; ++i)
            {
                l[i] = 1;
                for (int j = 0; j < i; ++j)
                {
                    if (nums[i] >= nums[j] && l[i] < l[j] + 1)
                        l[i] = l[j] + 1;
                }
                if (l[i] > max)
                    max = l[i];
            }
            return max;
        }

        public void Test()
        {
            int[] nums = new int[6] { 10, 9, 2, 5, 3, 4 };
            Console.WriteLine(Solution(nums));
            Console.ReadLine();
        }
    }
    #endregion

    #region 数组中的第K个最大元素
    /// <summary>
    /// https://leetcode-cn.com/problems/kth-largest-element-in-an-array/description/
    /// 2018.9.3
    /// </summary>
    public class FindKthLargest : ISolution
    {
        //冒泡
        private int Solution1(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                throw new Exception("Array error");
            for (int i = 0; i < k; ++i)
            {
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    if (nums[i] < nums[j])
                    {
                        int temp = nums[i];
                        nums[i] = nums[j];
                        nums[j] = temp;
                    }
                }
            }
            return nums[k - 1];
        }

        //快排
        private int Solution2(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                throw new Exception("Array error");

            return QucikSort(nums, 0, nums.Length - 1, k - 1);
        }

        private int Separate(int[] nums, int min, int max)
        {
            int temp = nums[min];
            while (min < max)
            {
                while (min < max && nums[max] <= temp)
                    --max;
                nums[min] = nums[max];
                while (min < max && nums[min] >= temp)
                    ++min;
                nums[max] = nums[min];
            }
            nums[min] = temp;
            return min;
        }

        private int QucikSort(int[] nums, int left, int right, int k)
        {
            int middle = Separate(nums, left, right);
            if (middle > k)
                return QucikSort(nums, left, middle, k);
            else if (middle < k)
                return QucikSort(nums, middle + 1, right, k);
            else
                return nums[k];
        }

        public void Test()
        {
            int[] nums = new int[6] { 3, 2, 1, 5, 6, 4 };
            Console.WriteLine(Solution2(nums, 2));
            Console.ReadLine();
        }
    }
    #endregion

    #region 最长公共前缀
    /// <summary>
    /// https://leetcode-cn.com/problems/longest-common-prefix/description/
    /// 2018.8.31
    /// </summary>
    public class LongestCommonPrefix : ISolution
    {
        private string Solution(string[] strs)
        {
            if (strs == null || strs.Length == 0)
                return "";
            if (strs[0] == null || strs[0].Length == 0)
                return "";
            if (strs.Length == 1)
                return strs[0];
            string pre = "";
            char current = strs[0][0];
            int j = 0;
            while (j < strs[0].Length)
            {
                current = strs[0][j];
                for (int i = 1; i < strs.Length; ++i)
                {
                    if (strs[i] == null || j >= strs[i].Length)
                        return pre;
                    if (strs[i][j] != current)
                        return pre;
                    if (i == strs.Length - 1)
                        pre += current.ToString();
                }
                ++j;
            }
            return pre;
        }

        public void Test()
        {
            //string[] strs = new string[3] { "flower","flow","flight" };
            //string[] strs = new string[1] { "a" };
            string[] strs = new string[2] { "aca", "cba" };
            Console.WriteLine(Solution(strs));
            Console.ReadLine();
        }
    }
    #endregion

    #region 回文数
    /// <summary>
    /// https://leetcode-cn.com/problems/palindrome-number/description/
    /// 2018.8.30
    /// </summary>
    public class IsPalindrome : ISolution
    {
        private bool Solution(int x)
        {
            if (x < 0)
                return false;
            if (x == 0)
                return true;
            if (x % 10 == 0)
                return false;

            int rev = 0;
            while (x > rev)
            {
                rev = rev * 10 + x % 10;
                x /= 10;
            }
            return x == rev || x == rev / 10;
        }

        public void Test()
        {
            Console.WriteLine(Solution(12321));
            Console.ReadLine();
        }
    }
    #endregion

    #region 反转整数
    /// <summary>
    /// https://leetcode-cn.com/problems/reverse-integer/description/
    /// 2018.8.30
    /// </summary>
    public class RevertInteger : ISolution
    {
        private int Solution(int x)
        {
            int MAX = int.MaxValue / 10;
            int MIN = int.MinValue / 10;
            int rev = 0;
            while (x != 0)
            {
                int pop = x % 10;
                x /= 10;
                if (rev > MAX || (rev == MAX && pop > 7))
                    return 0;
                if (rev < MIN || (rev == MIN && pop < -8))
                    return 0;
                rev = rev * 10 + pop;
            }
            return rev;
        }

        public void Test()
        {
            Console.WriteLine(Solution(-2113847412));
            Console.ReadLine();
        }
    }
    #endregion

    #region 无重复字符的最长子串
    /// <summary>
    /// https://leetcode-cn.com/problems/longest-substring-without-repeating-characters/description/
    /// 2018.8.28
    /// </summary>
    public class LengthOfLongestSubstring : ISolution
    {
        private int Solution(string s)
        {
            int[] chars = new int[128];
            int ans = 0;
            for (int i = 0, j = 0; i < s.Length; ++i)
            {
                j = Math.Max(chars[s[i]], j);
                ans = Math.Max(ans, i - j + 1);
                chars[s[i]] = i + 1;
            }
            return ans;
        }

        public void Test()
        {
            Console.WriteLine(Solution("abcdef"));
            Console.ReadLine();
        }
    }
    #endregion

    #region 两数相加
    /// <summary>
    /// https://leetcode-cn.com/problems/add-two-numbers/description/
    /// 2018.2.28
    /// </summary>
    public class AddTwoNumber : ISolution
    {
        private class ListNode
        {
            public ListNode(int _val)
            {
                val = _val;
            }
            public int val;
            public ListNode next;
        }

        private ListNode Solution(ListNode l1, ListNode l2)
        {
            ListNode head = new ListNode((l1.val + l2.val) % 10);
            ListNode current = head;
            ListNode node1 = l1;
            ListNode node2 = l2;
            int carry = (l1.val + l2.val) / 10;
            while (node1.next != null || node2.next != null || carry > 0)
            {
                int l1val = node1.next == null ? 0 : node1.next.val;
                int l2val = node2.next == null ? 0 : node2.next.val;
                current.next = new ListNode((l1val + l2val + carry) % 10);
                carry = (l1val + l2val + carry) / 10;
                current = current.next;
                node1 = node1.next ?? node1;
                node2 = node2.next ?? node2;
            }
            current.next = null;
            return head;
        }

        public void Test()
        {
            //             ListNode l1 = new ListNode(2)
            //             {
            //                 next = new ListNode(4)
            //                 {
            //                     next = new ListNode(3)
            //                 }
            //             };
            ListNode l1 = new ListNode(1)
            {
                next = new ListNode(8)
            };

            ListNode l2 = new ListNode(0);

            var result = Solution(l1, l2);
            var node = result;
            Console.WriteLine(node.val);
            while (node.next != null)
            {
                node = node.next;
                Console.WriteLine(node.val);
            }
            Console.ReadLine();
        }
    }
    #endregion

    #region 从数组中查找两个元素的目标和
    /// <summary>
    /// https://leetcode-cn.com/problems/two-sum/description/
    /// 2018.8.27
    /// </summary>
    public class TwoSum : ISolution
    {
        /// <summary>
        /// 暴力解决，两两相加，时间复杂度O(n^2)，空间复杂度O(1)
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private int[] Solution1(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                throw new Exception("array error");
            for (int i = 0; i < nums.Length; ++i)
            {
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    if (nums[i] + nums[j] == target)
                        return new int[2] { i, j };
                }
            }
            throw new Exception("No solution");
        }

        /// <summary>
        /// 以空间换时间，时间复杂度O(n)，空间复杂度O(n)
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private int[] Solution2(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                throw new Exception("Array error");
            Dictionary<int, int> numDic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; ++i)
            {
                numDic.Add(nums[i], i);
            }
            for (int i = 0; i < nums.Length; ++i)
            {
                int complement = target - nums[i];
                if (numDic.ContainsKey(complement) && numDic[complement] != i)
                    return new int[2] { i, numDic[complement] };
            }
            throw new Exception("No solution");
        }

        /// <summary>
        /// 时间复杂度O(n)，空间复杂度O(n)
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private int[] Solution3(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                throw new Exception("Array error");
            Dictionary<int, int> numDic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; ++i)
            {
                int complement = target - nums[i];
                if (numDic.ContainsKey(complement))
                    return new int[2] { numDic[complement], i };
                numDic.Add(nums[i], i);
            }
            throw new Exception("No solution");
        }

        public void Test()
        {
            int[] nums = new int[5] { -2, 0, 1, -3, 5 };
            int target = 6;
            int[] result = Solution1(nums, target);
            Console.Write("1:");
            foreach (var item in result)
            {
                Console.Write(item + " ");
            }
            result = Solution2(nums, target);
            Console.WriteLine("");
            Console.Write("2:");
            foreach (var item in result)
            {
                Console.Write(item + " ");
            }
            result = Solution3(nums, target);
            Console.WriteLine("");
            Console.Write("3:");
            foreach (var item in result)
            {
                Console.Write(item + " ");
            }
            Console.ReadLine();
        }
    }
    #endregion

    #region 从排序数组中删除重复项
    /// <summary>
    /// https://leetcode-cn.com/problems/remove-duplicates-from-sorted-array/description/
    /// 2018/8/27
    /// </summary>
    public class RemoveDuplicates : ISolution
    {
        public void Test()
        {
            int[] nums = new int[5] { 0, 1, 1, 2, 3 };
            int count = Solution(nums);
            for (int i = 0; i < count; ++i)
            {
                Console.WriteLine(nums[i]);
            }
            Console.ReadLine();
        }

        public int Solution(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            int count = 0;
            for (int i = 1; i < nums.Length; ++i)
            {
                if (nums[count] != nums[i])
                    nums[++count] = nums[i];
            }
            return count + 1;
        }
    }
    #endregion
}