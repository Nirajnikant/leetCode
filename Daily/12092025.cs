//https://leetcode.com/problems/longest-substring-without-repeating-characters/description/
// examples are misleading
public class Solution {
    public bool DoesAliceWin(string s) {
        var vowels = new HashSet<char>(['a', 'e', 'i', 'o', 'u']);
        int count = 0;
        foreach(char c in s) {
            if (vowels.Contains(c))
                count++;
        }
        return count > 0 ? true : false;
    }
}