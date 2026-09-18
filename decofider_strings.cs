using System;

public class Solution
{
    private const int MOD = 1_000_000_007;

    public int NumDecodings(string s)
    {
        int n = s.Length;
        long prev2 = 1; // dp[i-2]
        long prev1 = CountSingle(s[0]); // dp[i-1]

        for (int i = 1; i < n; i++)
        {
            long curr = 0;

            // Decodificação de 1 dígito
            curr = (curr + prev1 * CountSingle(s[i])) % MOD;

            // Decodificação de 2 dígitos
            curr = (curr + prev2 * CountPair(s[i - 1], s[i])) % MOD;

            prev2 = prev1;
            prev1 = curr;
        }

        return (int)prev1;
    }

    // Quantas letras podem ser formadas com um único caractere
    private int CountSingle(char c)
    {
        if (c == '*') return 9;      // 1-9
        if (c == '0') return 0;      // 0 não mapeia
        return 1;                     // 1-9
    }

    // Quantas letras podem ser formadas com dois caracteres
    private int CountPair(char a, char b)
    {
        if (a == '*' && b == '*')
            return 15; // 11-19 (9) + 21-26 (6) = 15

        if (a == '*')
        {
            // *b -> 1b ou 2b
            if (b >= '0' && b <= '6') return 2; // 1x e 2x válidos (x=0..6)
            if (b >= '7' && b <= '9') return 1; // só 1x válido
            return 0;
        }

        if (b == '*')
        {
            // a* -> depende de a
            if (a == '1') return 9; // 11-19
            if (a == '2') return 6; // 21-26
            return 0;
        }

        // Ambos dígitos
        int val = (a - '0') * 10 + (b - '0');
        if (val >= 10 && val <= 26) return 1;
        return 0;
    }
}
