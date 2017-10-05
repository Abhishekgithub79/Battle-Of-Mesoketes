using System;
using System.Collections.Generic;

namespace BattleOfMesoketes
{
    public class Program
    {
        private static IMesoketesBattle _helper = null;

        /// <summary>
        /// Main entry of the Application.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string [] args)
        {
            do
            {
                string [] days = new string [Constants.NUMBEROFDAYS];

                using (_helper = new MesoketesBattle())
                {
                    _helper.InitializeSides();

                    Console.WriteLine(string.Format("Enter battles followed with ';' and battle details followed with '-' for {0} days.", Constants.NUMBEROFDAYS));

                    for (int i = 0; i < Constants.NUMBEROFDAYS; i++)
                    {
                        Console.WriteLine(string.Format("Day {0} battles:", (i + 1)));

Re_Enter:
                        days [i] = Console.ReadLine();

                        string errmessage = string.Empty;
                        if (!_helper.ValidateInput(days [i], ref errmessage))
                        {
                            Console.WriteLine(string.Format("Day {0} battles are invalid. Error Message: [{1}]. Please enter again for Day {0}.", (i + 1), errmessage));

                            goto Re_Enter;
                        }
                    }

                    int successfulBattleCount = _helper.GetSuccessfulBattlesCount(days);
                    Console.WriteLine("Total successful battles : " + successfulBattleCount);
                }

                Console.WriteLine("Enter 1 to try more battles OR press any key to exit.");
                Console.WriteLine("-----------------------------------------------------");

            } while (Console.ReadLine() == "1");
        }
    }
}
