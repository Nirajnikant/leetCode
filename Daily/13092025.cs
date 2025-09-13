// https://leetcode.com/problems/find-most-frequent-vowel-and-consonant/description/

public class Solution {
    public int MaxFreqSum(string s) //* 3541 Find Most Frequent Vowel and Consonant
    {
        int[] dict = new int[26];
        int maxVowel = 0, maxConsonant = 0;

        foreach (char c in s)
        {
            int val = ++dict[c - 'a']; //* +1 Add to the frequency dictionary and set value.

            if ("aeiou".Contains(c)) //* If c is vowel, detect maxVowel.
            {
                if (val > maxVowel)
                    maxVowel = val;
            }

            else if (val > maxConsonant) //* Else detect maxConstant.
            {
                maxConsonant = val;
            }
        }

        return maxVowel + maxConsonant; //* Return total.
    }
}