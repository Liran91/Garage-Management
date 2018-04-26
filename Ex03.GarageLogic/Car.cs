using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
  public enum eCarColors
    {
        Yellow = 1, White, Black, Red
    }

  public class Car : Vehicle
  {
     private const int k_NumOfWheelsInCar = 4;
     private const float k_MaxAirPressureInCarWheel = 30;
     private const float k_MaxBatteryCapacityInElectricCar = 3.3f;
     private const float k_MaxFuelTankCapacityInFuelPoweredCar = 38.0f;
     private const eFuelType k_FuelPoweredCarFuelType = eFuelType.Octan98;
      private eCarColors m_CarColor;
      private int m_numOfCarDoors;

     public PowerType CarPowerType
      {
          get
          {
              return m_PowerType;
          }

          set
          {
              m_PowerType = value;
          }  
      }

     public Car(bool isElectricPoweredCar)
     {
         m_VehicleWheels = new List<Wheel>();

         for (int i = 0; i < k_NumOfWheelsInCar; i++)
         {
             Wheel wheelToAdd = new Wheel();
             wheelToAdd.MaximumAirPressure = k_MaxAirPressureInCarWheel;
             m_VehicleWheels.Add(wheelToAdd);
         }

         if (isElectricPoweredCar)
         {
             CarPowerType = new PoweredByElectricity(k_MaxBatteryCapacityInElectricCar);
         }
         else
         {
             CarPowerType = new PoweredByFuel(k_FuelPoweredCarFuelType, k_MaxFuelTankCapacityInFuelPoweredCar);
         }
     }
      
     public override StringBuilder GetSpecificVehicleInformation(StringBuilder infoStr)
     {
         infoStr.Append("Number of car doors: ");
         infoStr.Append(m_numOfCarDoors.ToString());
         infoStr.Append(Environment.NewLine);
         infoStr.Append(Environment.NewLine);

         infoStr.Append("Car color: ");
         string carColorStr = Enum.GetName(typeof(eCarColors), m_CarColor);
         infoStr.Append(carColorStr);
         infoStr.Append(Environment.NewLine);
         infoStr.Append(Environment.NewLine);

         return infoStr;
     }

     public override List<string> GetInputRequestMesseges()
     {
         List<string> inputReqMsgsList = new List<string>();

         inputReqMsgsList.Add(@"Please select car color:
1.Yellow
2.White
3.Black
4.Red");
         inputReqMsgsList.Add("Please enter number of car doors:");

         return inputReqMsgsList;
     }

     public override void ExtractSpecificVehicleDataValues(List<string> inputList)
     {
             string carColorInput = inputList[0];
             TrySetCarColor(carColorInput);

             string numOfDoorsInCarInput = inputList[1];
             TrySetNumOfCarDoors(numOfDoorsInCarInput);       
     }

      public void TrySetCarColor(string inputtedColor)
      {
          int selectedColor;
          bool validSelectedColor = int.TryParse(inputtedColor, out selectedColor);

          if (!validSelectedColor)
          {
              throw new FormatException("CarColor");
          }
          else if (selectedColor < 1 || selectedColor > 4)
          {
              throw new ValueOutOfRangeException("CarColorSelection", 1, 4);
          }

          m_CarColor = (eCarColors)selectedColor;
      }

      public void TrySetNumOfCarDoors(string userSelectedNumOfDoorsStr)
      {
          int userSelectedNumOfDoors;
          bool selectDoorsAmountValid = int.TryParse(userSelectedNumOfDoorsStr, out userSelectedNumOfDoors);

          if (!selectDoorsAmountValid)
          {
              throw new FormatException("NumOfDoorsInCar");
          }
          else if (userSelectedNumOfDoors < 2 || userSelectedNumOfDoors > 5)
          {
              throw new ValueOutOfRangeException("NumOfDoorsInCar", 2, 5);
          }

          m_numOfCarDoors = userSelectedNumOfDoors;
      }
  }
}
