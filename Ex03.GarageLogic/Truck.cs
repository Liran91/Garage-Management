using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
   public class Truck : Vehicle
    {
       private const int k_NumOfWheelsInTruck = 16;
       private const float k_MaxAirPressureInTruckWheel = 28;
       private const float k_MaxFuelTankCapacityInFuelPoweredTruck = 135;
       private const eFuelType k_FuelPoweredTruckFuelType = eFuelType.Soler;
        private bool m_CarriesHazardousMaterials;
        private float m_MaximumCarryWeight;

        public Truck(bool isElectricPoweredTruck)
        {
            m_VehicleWheels = new List<Wheel>();

            for (int i = 0; i < k_NumOfWheelsInTruck; i++)
            {
                Wheel wheelToAdd = new Wheel();
                wheelToAdd.MaximumAirPressure = k_MaxAirPressureInTruckWheel;
                m_VehicleWheels.Add(wheelToAdd);
            }

            TruckPowerType = new PoweredByFuel(k_FuelPoweredTruckFuelType, k_MaxFuelTankCapacityInFuelPoweredTruck);
            }
        
        public PowerType TruckPowerType
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

        public bool CarriesHazardousMaterials
        {
            get
            {
                return m_CarriesHazardousMaterials;
            }

            set
            {
                m_CarriesHazardousMaterials = value;
            }
        }

        public float MaximumCarryWeight
        {
            get
            {
                return m_MaximumCarryWeight;
            }

            set
            {
                m_MaximumCarryWeight = value;
            }
        }

        public override StringBuilder GetSpecificVehicleInformation(StringBuilder infoStr)
        {
            infoStr.Append("Carries hazardous materials: ");
            infoStr.Append(m_CarriesHazardousMaterials.ToString());
            infoStr.Append(Environment.NewLine);
            infoStr.Append(Environment.NewLine);

            infoStr.Append("Maximum carry weight: ");        
            infoStr.Append(m_MaximumCarryWeight.ToString());
            infoStr.Append(Environment.NewLine);
            infoStr.Append(Environment.NewLine);

            return infoStr;
        }

        public override void ExtractSpecificVehicleDataValues(List<string> inputList)
        {
            string carriesHazMat = inputList[0];
            TrySetCarriesHazardousMaterials(carriesHazMat);

            string maxCarryWeight = inputList[1];
            TrySetMaxCarryWeight(maxCarryWeight);
        }

        public override List<string> GetInputRequestMesseges()
        {
            List<string> inputReqMsgsList = new List<string>();

            inputReqMsgsList.Add(@"Is the truck carrying Hazardous Materials?
1.Yes
2.No
");
            inputReqMsgsList.Add("Please enter truck's maximum carry weight:");

            return inputReqMsgsList;
        }

        private void TrySetMaxCarryWeight(string maxCarryWeightInput)
        {
            float maxCarryWeightToInsert;
            bool validMaxCarryWeight = float.TryParse(maxCarryWeightInput, out maxCarryWeightToInsert);

            if (!validMaxCarryWeight)
            {
                throw new FormatException("CarriesHazardousMaterials");
            }
            else if (maxCarryWeightToInsert < 0)
            {
                throw new Exception("Out of range exception");
            }

            m_MaximumCarryWeight = maxCarryWeightToInsert;
        }

        private void TrySetCarriesHazardousMaterials(string inputBoolStr)
        {
            int selection;
            bool validSelection = int.TryParse(inputBoolStr, out selection);

          if (!validSelection)
          {
              throw new FormatException("CarriesHazardousMaterials");
          }
          else if (selection < 1 || selection > 2)
          {
              throw new ValueOutOfRangeException("Out of range exception", 1, 2);
          }

            if (selection == 1)
            {
                m_CarriesHazardousMaterials = true;
            }
            else 
            {
                m_CarriesHazardousMaterials = false;
            }
        }
    }
}
