using aman.Models;
using aman.Utils;
using System.Text;
using _01 = aman._01_highest_number;
using _02 = aman._02_tic_tac_toe;
using _03 = aman._03_tic_tac_toe;

namespace aman;

public class Program
{
    private static void Main(string[] args)
    {
        PlayTicTacToePro();
    }



    private static void PlayTicTacToe()
    {
        List<ModelBase> models = Model02(false);
        TicTacToe.Run((_02.Model)models[0]);
    }
    private static void PlayTicTacToePro()
    {
        List<ModelBase> models = Model03(true);
        TicTacToe.Run((_03.Model)models[0]);
    }



    private static List<ModelBase> Model01(bool debug = true)
    {
        return new _01.Training().Run(debug);
    }
    private static List<ModelBase> Model02(bool debug = true)
    {
        return new _02.Training().Run(debug);
    }
    private static List<ModelBase> Model03(bool debug = true)
    {
        return new _03.Training().Run(debug);
    }
}