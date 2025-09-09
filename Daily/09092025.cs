// https://leetcode.com/problems/number-of-people-aware-of-a-secret/description/

public class Solution {
    public int PeopleAwareOfSecret(int n, int delay, int forget) {
        const int MOD = 1000000007;
        var dp = new long[n+1]; 
        dp[1] = 1; // at day 1, 1 person knows the secret
        long sharing = 0; // Secret cannot be shared on day 1 because of delay
        long total = 1 ;// total number of people knows the secret

        for (int day = 2; day <= n; day++) 
        {
            // Add people who start sharing today
            if (day - delay >= 1) 
            {
                sharing = (sharing + dp[day - delay]) % MOD;
            }

            // Remove people who forget today (they stop sharing)
            if (day - forget >= 1) 
            {
                sharing = (sharing - dp[day - forget] + MOD) % MOD;
                total = (total - dp[day - forget] + MOD) % MOD;
            }

            // New people learn the secret today
            dp[day] = sharing;
            total = (total + dp[day]) % MOD;
        }

        return (int)total;
    }
}