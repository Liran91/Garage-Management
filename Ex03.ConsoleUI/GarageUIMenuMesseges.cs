using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public static class GarageUIMenuMesseges
    {
        public static void PrintVehicleTypeToAddMenu(string[] vehicleTypes)
        {
            System.Console.WriteLine(@"Please select the vehicle type you'd like to add:");
            int i = 1;
            foreach (string vehicleType in vehicleTypes)
            {
                System.Console.WriteLine("{0}.{1}", i, vehicleType);
                i++;
            }
        }

        public static void PrintOperationDone()
        {
            System.Console.WriteLine("Operation complete!");
            System.Console.WriteLine("Press any key to return to main menu...");
            System.Console.ReadKey();
            System.Console.Clear();
        }

        public static void ClearAndPrintAnyKey()
        {
            System.Console.WriteLine("Press any key to return to main menu...");
            System.Console.ReadKey();
            System.Console.Clear();
        }

        public static void PrintFilterOrNotMenu()
        {
            System.Console.WriteLine(@"Would you like to filter the vehicles ID by vehicle status?
1.Yes
2.No");
        }

        public static void PrintExitMessege()
        {
            System.Console.WriteLine(@"Thank you for using the Garage, Bye Bye...");
        }

        public static void PrintFilterByWhatVehicleStatusMenu()
        {
            System.Console.WriteLine(@"By what status would you like to filter?
1.Fixing
2.Fixed
3.Paid");
        }

        public static void PrintFuelTypeMenu()
        {
            System.Console.WriteLine(@"Please select the fuel type you would like to fuel your vehicle with:
1.Soler
2.Octan95
3.Octan96
4.Octan98");
        }

        public static void PrintAvailableVehicleStatusMenu()
        {
            System.Console.WriteLine(@"Please select the new status for the vehicle
1.Fixing
2.Fixed
3.Paid");
        }

        public static void PrinteMainMenu()
        {
            string mainMenu = @"
Welcome to the Garage!

Please select one of following options:

1.Insert new vehicle

2.Display vehicles in garage license plates

3.Change vehicle status

4.Inflate vehicle wheels to maximum

5.Fuel vehicle powered by fuel

6.Charge vehicle powered by electricity

7.Display vehicle information

8.Exit
";
            System.Console.WriteLine(mainMenu);
        }
    }
}
