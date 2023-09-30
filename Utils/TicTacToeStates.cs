using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace aman.Utils;

public class TicTacToeStates
{
    public static List<string> GetActiveLegalStates()
    {
        // generate a list of all legal TTT-positions that are not over

        // 4536 positions
        List<string> positions = new();
        for (int i = 0; i < Math.Pow(3, 9); i++)
        {
            string key = ToTernary(i);

            if (!GameOver(key))
            {
                int difference = key.Count(c => c == '1') - key.Count(c => c == '2');
                if (difference == 0 || difference == 1)
                {
                    positions.Add(key);
                    // Console.WriteLine(key);
                    // PrintPlayfield(key);
                }
            }
        }
        // Console.WriteLine($"Total positions: {positions.Count}");

        return positions;
    }

    public static string ToTernary(int value)
    {
        if (value == 0)
            return "000000000";

        StringBuilder Sb = new StringBuilder();
        while (value > 0)
        {
            Sb.Insert(0, value % 3);
            value /= 3;
        }
        return Sb.ToString().PadLeft(9, '0');
    }

    private static bool GameOver(string position)
    {
        int num = 0;

        for (int i = 1; i <= 2; i++)
        {
            char player = i == 1 ? '1' : '2';

            // check rows
            if (position[0] == player && position[1] == player && position[2] == player) return true;
            if (position[3] == player && position[4] == player && position[5] == player) return true;
            if (position[6] == player && position[7] == player && position[8] == player) return true;

            // check columns
            if (position[0] == player && position[3] == player && position[6] == player) return true;
            if (position[1] == player && position[4] == player && position[7] == player) return true;
            if (position[2] == player && position[5] == player && position[8] == player) return true;

            // check diagonals
            if (position[0] == player && position[4] == player && position[8] == player) return true;
            if (position[6] == player && position[4] == player && position[2] == player) return true;
        }

        return false;
    }

    private static void PrintPlayfield(string playfield)
    {
        Console.Write("\n┌─┬─┬─┐");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("\n│");
            for (int j = 0; j < 3; j++)
            {
                int field = 3 * i + j;
                char sign = playfield[field];
                switch (sign)
                {
                    case '0':
                        Console.Write(" │");
                        break;
                    case '1':
                        Console.Write("X│");
                        break;
                    case '2':
                        Console.Write("O│");
                        break;
                }
            }

            if (i != 2)
            {
                Console.Write("\n├─┼─┼─┤");
            }
        }
        Console.Write("\n└─┴─┴─┘");
    }
}
