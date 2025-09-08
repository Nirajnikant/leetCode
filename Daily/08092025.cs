// https://leetcode.com/problems/convert-integer-to-the-sum-of-two-no-zero-integers/description/

public class Solution {
    public int[] GetNoZeroIntegers(int n) {
        var arr = new int[2];
        for(int i = 1; i<n ;i++) {
            arr[0] = i;
            arr[1] = n-i;
            if (arr[0].ToString().Contains("0") || arr[1].ToString().Contains("0")) {
                continue;   
            }
            return arr;
        }
        return [];
    }
}