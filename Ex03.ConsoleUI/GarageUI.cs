using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public enum eMainMenu
    {
        AddVehicle = 1, DisplayVehiclesIDList, ChangeVehicleStatus, InflateVehicleWheelsToMaximum, FuelVehicle, ChargeVehicle, PrintVehicleInformation, Exit
    }

    public class GarageUI
    {
        private const int k_Yes = 1;
        private const int k_No = 2;
        private Garage m_Garage;

        public GarageUI()
        {
            m_Garage = new Garage();
        }

        public void MainMenu()
        {
            bool keepRunning = true;

            while (keepRunning)
            {
                GarageUIMenuMesseges.PrinteMainMenu();
                eMainMenu selection = (eMainMenu)GetValidInput(1, 8);
                switch (selection)
                {
                    case eMainMenu.AddVehicle: 
                        AddVehicle();
                        break;
                    case eMainMenu.DisplayVehiclesIDList: 
                        DisplayVehiclesIDList();
                        break;
                    case eMainMenu.ChangeVehicleStatus: 
                        ChangeVehicleStatus();
                        break;
                    case eMainMenu.InflateVehicleWheelsToMaximum: 
                        InflateVehicleWheelsToMaximum();
                        break;
                    case eMainMenu.FuelVehicle: 
                        FuelVehicle();
                        break;
                    case eMainMenu.ChargeVehicle: 
                        ChargeVehicle();
                        break;
                    case eMainMenu.PrintVehicleInformation: 
                        PrintVehicleInformation();
                        break;
                    case eMainMenu.Exit:
                        GarageUIMenuMesseges.PrintExitMessege();
                        keepRunning = false;
                        break;
                }
            }
        }

        private void AddVehicle()
        {
            string[] EnumVehicleNameStrings = Enum.GetNames(typeof(eVehicleType));

            for (int i = 0; i < EnumVehicleNameStrings.Length; i++)
            {
                EnumVehicleNameStrings[i] = FixEnumStringLayout(EnumVehicleNameStrings[i]);
            }

            try
            {
                GarageUIMenuMesseges.PrintVehicleTypeToAddMenu(EnumVehicleNameStrings);
                int selection = GetValidInput(1, 5);
                eVehicleType vehicleType = (eVehicleType)selection;
                Vehicle vehicleToAdd = VehicleFactory.ConstructVehicle(vehicleType);
                GetBaseValuesForVehicle(vehicleToAdd);
                List<string> inputReqMsgs = vehicleToAdd.GetInputRequestMesseges();
                List<string> userInputList = new List<string>();

                foreach (string inputReqMsg in inputReqMsgs)
                {
                    System.Console.WriteLine(inputReqMsg);
                    string inputDataStr = System.Console.ReadLine();
                    userInputList.Add(inputDataStr);
                }

                vehicleToAdd.ExtractSpecificVehicleDataValues(userInputList);

                bool vehicleAlreadyInGarage = m_Garage.AddVehicleToGarage(vehicleToAdd.VehicleID, vehicleToAdd);

                if (vehicleAlreadyInGarage)
                {
                    System.Console.WriteLine("Error, vehicle already exists in garage!");
                    GarageUIMenuMesseges.ClearAndPrintAnyKey();
                }
                else
                {
                    GarageUIMenuMesseges.PrintOperationDone();
                }
            }
            catch (ValueOutOfRangeException ex)
            {
                System.Console.WriteLine(ex.ToString());
                GarageUIMenuMesseges.ClearAndPrintAnyKey();
            }
            catch (FormatException ex)
            {
                System.Console.WriteLine(ex.ToString());
                GarageUIMenuMesseges.ClearAndPrintAnyKey();
            }
            catch(Exception ex )
            {
                System.Console.WriteLine(ex.ToString());
                GarageUIMenuMesseges.ClearAndPrintAnyKey();
            }
        }

        private void GetBaseValuesForVehicle(Vehicle vehicle)
        {
            bool validInput = false;

            System.Console.WriteLine("Please enter the vehicle ID:");
            vehicle.VehicleID = System.Console.ReadLine();

            System.Console.WriteLine("Please enter the vehicle model name:");
            vehicle.ModelName = System.Console.ReadLine();

            System.Console.WriteLine("Please enter the vehicle owner name:");
            vehicle.OwnerCard.VehicleOwnerName = System.Console.ReadLine();

            System.Console.WriteLine("Please enter the vehicle owner phone number:");
            vehicle.OwnerCard.VehicleOwnerPhoneNumber = System.Console.ReadLine();

            System.Console.WriteLine("Please enter the vehicle wheels manufacturer name:");
            string wheelManufacturerName = System.Console.ReadLine();

            System.Console.WriteLine("Please enter the current air pressure in the vehicle wheels:");
            string currentAirPressureInVehicleWheels = System.Console.ReadLine();

            foreach (Wheel wheel in vehicle.VehicleWheels)
            {
                wheel.ManufacturerName = wheelManufacturerName;
                
                float currentAirPressureInWheels = 0;

                validInput = float.TryParse(currentAirPressureInVehicleWheels, out currentAirPressureInWheels);

                if (!validInput)
                {
                    throw new ValueOutOfRangeException("CurrentAirPressureInWheels", 0, vehicle.VehicleWheels[0].MaximumAirPressure);
                }
                else
                {
                        wheel.addAirPressure(currentAirPressureInWheels);
                }
            }

            GetValuesForPowerTypeTypeVariables(vehicle);   
        }

        private void GetValuesForPowerTypeTypeVariables(Vehicle vehicle)
        {
            bool valid = false;

            if (vehicle.PowerType is PoweredByFuel)
            {
                float currentAmountOfFuelInTank = 0;

                    System.Console.WriteLine("Please enter the current amount of fuel in the tank:");
                    valid = float.TryParse(System.Console.ReadLine(), out currentAmountOfFuelInTank);

                    if (!valid)
                    {
                        throw new FormatException("CurrentAmountOfFuelInTank");
                    }
                    else
                    {
                        if (currentAmountOfFuelInTank > (vehicle.PowerType as PoweredByFuel).MaximumFuelTankCapacity || currentAmountOfFuelInTank < 0)
                        {
                            throw new ValueOutOfRangeException("CurrentAmountOfFuelInTank", 0, (vehicle.PowerType as PoweredByFuel).MaximumFuelTankCapacity);
                        }
                        else
                        {
                            (vehicle.PowerType as PoweredByFuel).CurrentAmountOfFuel = currentAmountOfFuelInTank;
                        }
                    }
            }
            else 
            {
                float remainingBatteryTimeInHours = 0;

                    System.Console.WriteLine("Please enter the remaining battery time in hours:");
                    valid = float.TryParse(System.Console.ReadLine(), out remainingBatteryTimeInHours);

                    if (!valid)
                    {
                        throw new FormatException("remainingBatteryTimeInHours");
                    }
                    else
                    {
                        if (remainingBatteryTimeInHours > (vehicle.PowerType as PoweredByElectricity).MaximumBatteryCapacity || remainingBatteryTimeInHours < 0)
                        {
                            throw new ValueOutOfRangeException("RemainingBatteryTimeInHours", 0, (vehicle.PowerType as PoweredByElectricity).MaximumBatteryCapacity);
                        }
                        else
                        {
                            (vehicle.PowerType as PoweredByElectricity).RemainingBatteryTimeInHours = remainingBatteryTimeInHours;
                        }
                    }
            }
        }

        private void ChargeVehicle()
        {
            System.Console.WriteLine("Please enter the ID of the vehicle you would like to charge:");
            string vehicleID = System.Console.ReadLine();
            bool VehicleExistsInGarage = m_Garage.DoesVehicleExistsInGarage(vehicleID);

            if (VehicleExistsInGarage)
            {
                if (m_Garage.GetVehicle(vehicleID).PowerType is PoweredByElectricity)
                {
                    float durationOfChargeTimeInMins = GetValidChargeTimeValue();
                    m_Garage.ChargeVehicle(vehicleID, durationOfChargeTimeInMins);
                    GarageUIMenuMesseges.PrintOperationDone();
                }
                else
                {
                    System.Console.WriteLine("Error! Can only charge vehicles that are powered by Electricity!");
                }
            }
            else
            {
                System.Console.WriteLine("Error! no vehicle with vehicle ID {0} in garage!{1}", vehicleID, Environment.NewLine);
                GarageUIMenuMesseges.ClearAndPrintAnyKey();
            }
        }

        private float GetValidChargeTimeValue()
        {
            bool valid = false;
            float durationOfChargeTimeInMins = 0;

            System.Console.WriteLine("Please enter the desired charge time in minutes:");
            while (!valid)
            {
                valid = float.TryParse(System.Console.ReadLine(), out durationOfChargeTimeInMins);

                if (!valid)
                {
                    System.Console.WriteLine("Error,invalid input! please try again!");
                    System.Console.WriteLine("Please enter the duration of the vehicle the charging session:");
                }
            }

            return durationOfChargeTimeInMins;
        }

        private void ChangeVehicleStatus()
        {
            System.Console.WriteLine("Please enter the ID of the vehicle you would like to change the status of:");
            string vehicleID = System.Console.ReadLine();
            bool VehicleExistsInGarage = m_Garage.DoesVehicleExistsInGarage(vehicleID);
            
            if (VehicleExistsInGarage)
            {            
                GarageUIMenuMesseges.PrintAvailableVehicleStatusMenu();
                int selection = GetValidInput(1, 3);
                eStatusOfVehicleInGarage newVehicleStatus = (eStatusOfVehicleInGarage)selection;
                try
                {
                    m_Garage.ChangeVehicleStatus(vehicleID, newVehicleStatus);
                    GarageUIMenuMesseges.PrintOperationDone();
                }
                catch(Exception)
                {
                    System.Console.WriteLine("Error! Cannot change vehicle status because its already status: {0}!", newVehicleStatus);
                    GarageUIMenuMesseges.ClearAndPrintAnyKey();
                }
            }
            else
            {
                System.Console.WriteLine("Error! no vehicle with vehicle ID {0} in garage!{1}", vehicleID, Environment.NewLine);
                GarageUIMenuMesseges.ClearAndPrintAnyKey();
            }
        }

        private void FuelVehicle()
        {
            System.Console.WriteLine("Please enter the ID of the vehicle you would like to fuel:");
            string vehicleID = System.Console.ReadLine();
            bool VehicleExistsInGarage = m_Garage.DoesVehicleExistsInGarage(vehicleID);

            if (VehicleExistsInGarage)
            {
                Vehicle vehicleToFuel = m_Garage.GetVehicle(vehicleID);

                if (vehicleToFuel.PowerType is PoweredByFuel)
                {
                    GarageUIMenuMesseges.PrintFuelTypeMenu();
                    int selection = GetValidInput(1, 4);
                    eFuelType fuelTypeToAdd = (eFuelType)selection;

                    eFuelType vehicleFuelType = (vehicleToFuel.PowerType as PoweredByFuel).FuelType;

                    if (fuelTypeToAdd == vehicleFuelType)
                    {
                        try
                        {
                            float amountOfFuelToAdd = GetValidFuelToAddValue(fuelTypeToAdd);
                            m_Garage.FuelVehicle(vehicleID, fuelTypeToAdd, amountOfFuelToAdd);
                        }
                        catch (ValueOutOfRangeException ex)
                        {
                            System.Console.WriteLine(ex.ToString());
                        }

                        GarageUIMenuMesseges.PrintOperationDone();
                    }
                    else
                    {
                        System.Console.WriteLine("Error! cannot add fuel type {0} to a fuel type {1} vehicle!", Enum.GetName(typeof(eFuelType), fuelTypeToAdd), Enum.GetName(typeof(eFuelType), vehicleFuelType));
                        GarageUIMenuMesseges.ClearAndPrintAnyKey();
                    }
                }
                else
                {
                    System.Console.WriteLine("Error! Can only fuel vehicles that are powered by fuel!");
                    GarageUIMenuMesseges.ClearAndPrintAnyKey();
                }
            }
            else
            {
                System.Console.WriteLine("Error! no vehicle with vehicle ID {0} in garage!{1}", vehicleID, Environment.NewLine);
                GarageUIMenuMesseges.ClearAndPrintAnyKey();
            }
        }

        private float GetValidFuelToAddValue(eFuelType fuelType)
        {
            bool valid = false;
            float amountOfFuelToAdd = 0;
            System.Console.WriteLine("Please enter the desired amount of {0} fuel you would like to add:", fuelType);
            while (!valid)
            {
                valid = float.TryParse(System.Console.ReadLine(), out amountOfFuelToAdd);

                if (!valid)
                {
                    System.Console.WriteLine("Error,invalid input! please try again!");
                    System.Console.WriteLine("Please enter the desired amount of {0} fuel you would like to add:", fuelType);
                }
            }

            return amountOfFuelToAdd;
        }

        private void InflateVehicleWheelsToMaximum()
        {
            System.Console.WriteLine("Please enter the ID of the vehicle whos wheels you would like to inflate to maximum:");
            string vehicleID = System.Console.ReadLine();
            bool VehicleExistsInGarage = m_Garage.DoesVehicleExistsInGarage(vehicleID);

            if (VehicleExistsInGarage)
            {
                m_Garage.IncreaseAirPressureInWheelsToMaximum(vehicleID);
                GarageUIMenuMesseges.PrintOperationDone();
            }
            else
            {
                System.Console.WriteLine("No matching vehicles found in garage!{0}", Environment.NewLine);
            }
        }

        private void DisplayVehiclesIDList()
        {
            GarageUIMenuMesseges.PrintFilterOrNotMenu();
            int selection = GetValidInput(1, 2);
            List<string> vehicleIDsList;
            eStatusOfVehicleInGarage vehicleStatusToFilterBy;
            string tempPrintStr;
            int i = 1;

            if (selection == k_Yes)
            {
                GarageUIMenuMesseges.PrintFilterByWhatVehicleStatusMenu();
                selection = GetValidInput(1, 3);

                vehicleStatusToFilterBy = (eStatusOfVehicleInGarage)selection;
                vehicleIDsList = m_Garage.GetFilteredVehicleIDsList(vehicleStatusToFilterBy);

                string status = Enum.GetName(typeof(eStatusOfVehicleInGarage), vehicleStatusToFilterBy);
                tempPrintStr = string.Format(
@"Here are the ID numbers of the current vehicles inside the garage,
under the status of {0}: ", 
                          status);
            }
            else
            {
                vehicleIDsList = m_Garage.GetVehicleIDsList();
                tempPrintStr = "Here are the ID numbers of the current vehicles inside the garage:";
            }

            if (vehicleIDsList.Count > 0)
            {
                System.Console.WriteLine(tempPrintStr);
                foreach (string vehicleID in vehicleIDsList)
                {
                    System.Console.WriteLine(string.Format("{0}.{1}", i, vehicleID));
                    i++;
                }

                GarageUIMenuMesseges.PrintOperationDone();
            }
            else
            {
                System.Console.WriteLine("No matching vehicles found in garage!{0}", Environment.NewLine);
            }
        }

        private int GetValidInput(int selectionRangeMin, int selectionRangeMax)
        {
            bool validChoice = false;
            int choice = 0;

            while (!validChoice)
            {
                string inputString = System.Console.ReadLine();

                if (int.TryParse(inputString, out choice))
                {
                    if (choice >= selectionRangeMin && choice <= selectionRangeMax)
                    {
                        validChoice = true;
                    }
                }

                if (!validChoice)
                {
                    System.Console.WriteLine(@"Error,please enter a numeric value between {0} and {1} and try again,", selectionRangeMin, selectionRangeMax);
                }
            }

            return choice;
        }

        private void PrintVehicleInformation()
        {
            System.Console.WriteLine("Please enter the ID of the vehicle you would like to get information about:");
            string vehicleID = System.Console.ReadLine();                   
            bool VehicleExistsInGarage = m_Garage.DoesVehicleExistsInGarage(vehicleID);
            
            if (VehicleExistsInGarage)
            {
                Vehicle vehicle = m_Garage.GetVehicle(vehicleID);
                StringBuilder infoStr = new StringBuilder();

                infoStr.Append("Vehicle ID: ");
                infoStr.Append(vehicleID);
                infoStr.Append(Environment.NewLine);
                infoStr.Append(Environment.NewLine);

                infoStr.Append("Model Name: ");
                infoStr.Append(vehicle.ModelName);
                infoStr.Append(Environment.NewLine);
                infoStr.Append(Environment.NewLine);

                infoStr.Append("Owner Name: ");
                infoStr.Append(vehicle.OwnerCard.VehicleOwnerName);
                infoStr.Append(Environment.NewLine);
                infoStr.Append(Environment.NewLine);

                string vehicleStatusNameStr = Enum.GetName(typeof(eStatusOfVehicleInGarage), vehicle.OwnerCard.VehicleStatus);

                infoStr.Append("Vehicle Status: ");
                infoStr.Append(vehicleStatusNameStr);
                infoStr.Append(Environment.NewLine);
                infoStr.Append(Environment.NewLine);

                infoStr.Append("Percent of remaining energy in power source: ");
                infoStr.Append(vehicle.GetPercentOfRemainingEnergyInPowerSource());
                infoStr.Append(Environment.NewLine);
                infoStr.Append(Environment.NewLine);

                infoStr.Append("Current air pressure in vehicle wheels: ");
                infoStr.Append(vehicle.VehicleWheels[0].CurrentAirPressure);
                infoStr.Append(Environment.NewLine);
                infoStr.Append(Environment.NewLine);

                infoStr.Append("Wheels manufacturer: ");
                infoStr.Append(vehicle.VehicleWheels[0].ManufacturerName);
                infoStr.Append(Environment.NewLine);
                infoStr.Append(Environment.NewLine);

                if (vehicle.PowerType is PoweredByFuel)
                {
                    infoStr.Append("Fuel type: ");
                    string fuelTypeStr = Enum.GetName(typeof(eFuelType), (vehicle.PowerType as PoweredByFuel).FuelType);
                    infoStr.Append(fuelTypeStr);
                    infoStr.Append(Environment.NewLine);
                    infoStr.Append(Environment.NewLine);

                    infoStr.Append("Current amount of fuel: ");
                    infoStr.Append((vehicle.PowerType as PoweredByFuel).CurrentAmountOfFuel);
                    infoStr.Append(Environment.NewLine);
                    infoStr.Append(Environment.NewLine);
                }
                else
                {
                    infoStr.Append("Remaining battery time in hours: ");
                    string batteryTimeStr = (vehicle.PowerType as PoweredByElectricity).RemainingBatteryTimeInHours.ToString();
                    infoStr.Append(batteryTimeStr);
                    infoStr.Append(Environment.NewLine);
                    infoStr.Append(Environment.NewLine);
                }

                StringBuilder finalInfoStr = vehicle.GetSpecificVehicleInformation(infoStr);

                System.Console.WriteLine(finalInfoStr.ToString());

                GarageUIMenuMesseges.PrintOperationDone();
            }
            else
            {
                System.Console.WriteLine("Vehicle ID doesnt exist in garage!");
            }
        }

        private string FixEnumStringLayout(string msg)
        {
            StringBuilder msgToPrint = new StringBuilder();
            int lengthOfEnum = msg.Length;
            foreach (char ch in msg)
            {
                if (char.IsUpper(ch))
                {
                    msgToPrint.Append(" ");
                    msgToPrint.Append(ch);
                }
                else
                {
                    msgToPrint.Append(ch);
                }
            }

            return msgToPrint.ToString();
        }
    }
}