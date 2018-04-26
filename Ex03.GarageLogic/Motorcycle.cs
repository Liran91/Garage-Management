using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
   public enum eMotorcycleLicenseType
    {
        A = 1, A1, B, B1
    }

    public class Motorcycle : Vehicle
    {
        private const int k_NumOfWheelsInMotorcycle = 2;
        private const float k_MaxAirPressureInMotorcycleWheel = 31;
        private const float k_MaxBatteryCapacityInElectricMotorcycle = 1.9f;
        private const float k_MaxFuelTankCapacityInFuelPoweredMotorcycle = 7.2f;
        private const eFuelType k_FuelPoweredMotorcycleeFuelType = eFuelType.Octan95;
        private eMotorcycleLicenseType m_LicenseType;
        private int m_EngineCapacity;

     public Motorcycle(bool isElectricMotorcycle)
     {
         m_VehicleWheels = new List<Wheel>();

         for (int i = 0; i < k_NumOfWheelsInMotorcycle; i++)
         {
             Wheel wheelToAdd = new Wheel();
             wheelToAdd.MaximumAirPressure = k_MaxAirPressureInMotorcycleWheel;
             m_VehicleWheels.Add(wheelToAdd);
         }

         if (isElectricMotorcycle)
         {
             MotorcyclePowerType = new PoweredByElectricity(k_MaxBatteryCapacityInElectricMotorcycle);
         }
         else
         {
             MotorcyclePowerType = new PoweredByFuel(k_FuelPoweredMotorcycleeFuelType, k_MaxFuelTankCapacityInFuelPoweredMotorcycle);
         }
     }

        public List<Wheel> MotorcycleWheels
        {
            get
            {
                return m_VehicleWheels;
            }

            set
            {
                m_VehicleWheels = value;
            }
        }

        public PowerType MotorcyclePowerType
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

        public override StringBuilder GetSpecificVehicleInformation(StringBuilder infoStr)
        {
            infoStr.Append("License Type: ");
            infoStr.Append(Enum.GetName(typeof(eMotorcycleLicenseType), m_LicenseType));
            infoStr.Append(Environment.NewLine);
            infoStr.Append(Environment.NewLine);

            infoStr.Append("Engine Capacity: ");          
            infoStr.Append(m_EngineCapacity.ToString());
            infoStr.Append(Environment.NewLine);
            infoStr.Append(Environment.NewLine);

            return infoStr;
        }

        public override List<string> GetInputRequestMesseges()
        {
            List<string> inputReqMsgsList = new List<string>();

            inputReqMsgsList.Add(@"Please select Motorcycle license type:
1.A
2.A1
3.B
4.B1");
            inputReqMsgsList.Add("Please enter the Motorcycle's engine capacity:");

            return inputReqMsgsList;
        }

        public override void ExtractSpecificVehicleDataValues(List<string> inputList)
        {
            string licenseTypeInput = inputList[0];
            TrySetLicenseType(licenseTypeInput);
            string engineCapacityInput = inputList[1];
            TrySetEngineCapacity(engineCapacityInput);
        }

        public void TrySetEngineCapacity(string inputtedEngineCapacity)
        {
            int engineCapacityToInsert;
            bool validEngineCapacity = int.TryParse(inputtedEngineCapacity, out engineCapacityToInsert);

            if (!validEngineCapacity)
            {
                throw new FormatException("EngineCapacity");
            }
            else if (engineCapacityToInsert <= 0)
            {
                throw new Exception("Engine Capacity must be bigger than 0!");
            }

            m_EngineCapacity = engineCapacityToInsert;
        }

        public void TrySetLicenseType(string inputtedLicenseType)
        {
            int selectedLicenseType;
            bool validSelectedLicenseType = int.TryParse(inputtedLicenseType, out selectedLicenseType);

            if (!validSelectedLicenseType)
            {
                throw new FormatException("MotorcycleLicenseType");
            }
            else if (selectedLicenseType < 1 || selectedLicenseType > 4)
            {
                throw new ValueOutOfRangeException("LicenseType", 1, 4);
            }

            m_LicenseType = (eMotorcycleLicenseType)selectedLicenseType;
        }
    }
}
