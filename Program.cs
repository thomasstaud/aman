namespace aman
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // _01_highest_number.Main.Run();
            // _02_tic_tac_toe.Main.Run();

            var model = _02_tic_tac_toe.Main.Run();
            TicTacToe.Run(model);
        }
    }
}