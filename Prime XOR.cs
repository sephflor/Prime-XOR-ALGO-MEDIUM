using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    private const int MOD = 1000000007;
    private const int MAX_VAL = 8192;
    private static bool[] isPrime;
    
    static Solution() {
        isPrime = new bool[MAX_VAL];
        for (int i = 2; i < MAX_VAL; i++) isPrime[i] = true;
        for (int i = 2; i * i < MAX_VAL; i++) {
            if (isPrime[i]) {
                for (int j = i * i; j < MAX_VAL; j += i) {
                    isPrime[j] = false;
                }
            }
        }
    }
    
    public static int primeXor(List<int> a) {
        int[] freq = new int[4501];
        foreach (int num in a) freq[num]++;
        
        long[] dp = new long[MAX_VAL];
        dp[0] = 1;
        
        for (int num = 3500; num <= 4500; num++) {
            if (freq[num] == 0) continue;
            
            long[] temp = new long[MAX_VAL];
            Array.Copy(dp, temp, MAX_VAL);
            
            long even = freq[num] / 2 + 1;
            long odd = (freq[num] + 1) / 2;
            
            for (int i = 0; i < MAX_VAL; i++) {
                if (dp[i] == 0) continue;
                
                temp[i] = (temp[i] + dp[i] * (even - 1)) % MOD;
                
                int newX = i ^ num;
                temp[newX] = (temp[newX] + dp[i] * odd) % MOD;
            }
            dp = temp;
        }
        
        long result = 0;
        for (int i = 2; i < MAX_VAL; i++) {
            if (isPrime[i]) {
                result = (result + dp[i]) % MOD;
            }
        }
        return (int)result;
    }

    public static void Main(string[] args) {
        int tests = Convert.ToInt32(Console.ReadLine());
        
        for (int t = 0; t < tests; t++) {
            int n = Convert.ToInt32(Console.ReadLine());
            List<int> a = Console.ReadLine().Split().Select(int.Parse).ToList();
            Console.WriteLine(primeXor(a));
        }
    }
}
