// https://leetcode.com/problems/minimum-number-of-people-to-teach/?envType=daily-question&envId=2025-09-10

public class Solution {
    public int MinimumTeachings(int n, int[][] languages, int[][] friendships) {
        var userLanguages = new HashSet<int>[languages.Length];
        for (int i = 0; i < languages.Length; i++) {
            userLanguages[i] = new HashSet<int>(languages[i]);
        }
        
        var problematicFriendships = new List<(int, int)>();
        var problematicUsers = new HashSet<int>();
        
        foreach (var friendship in friendships) {
            int user1 = friendship[0] - 1;
            int user2 = friendship[1] - 1;
            
            bool canCommunicate = userLanguages[user1].Overlaps(userLanguages[user2]);
            
            if (!canCommunicate) {
                problematicFriendships.Add((user1, user2));
                problematicUsers.Add(user1);
                problematicUsers.Add(user2);
            }
        }
        
        if (problematicFriendships.Count == 0) {
            return 0;
        }
        
        // Only consider languages known by problematic users
        var candidateLanguages = new HashSet<int>();
        foreach (int user in problematicUsers) {
            foreach (int lang in userLanguages[user]) {
                candidateLanguages.Add(lang);
            }
        }
        
        // Also consider all languages as candidates (in case teaching a new language is optimal)
        for (int i = 1; i <= n; i++) {
            candidateLanguages.Add(i);
        }
        
        int minUsersToTeach = int.MaxValue;
        
        foreach (int lang in candidateLanguages) {
            var usersToTeach = new HashSet<int>();
            
            foreach (var (user1, user2) in problematicFriendships) {
                bool user1KnowsLang = userLanguages[user1].Contains(lang);
                bool user2KnowsLang = userLanguages[user2].Contains(lang);
                
                if (!user1KnowsLang && !user2KnowsLang) {
                    usersToTeach.Add(user1);
                    usersToTeach.Add(user2);
                }
                else if (!user1KnowsLang) {
                    usersToTeach.Add(user1);
                }
                else if (!user2KnowsLang) {
                    usersToTeach.Add(user2);
                }
            }
            
            minUsersToTeach = Math.Min(minUsersToTeach, usersToTeach.Count);
        }
        
        return minUsersToTeach;
    }
}