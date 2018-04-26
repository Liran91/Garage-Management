using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Vehicle> m_VehiclesInGarage;

        public Garage()
        {
            m_VehiclesInGarage = new Dictionary<string, Vehicle>();
        }

        public Dictionary<string, Vehicle> VehiclesInGarage
        {
            get
            {
                return m_VehiclesInGarage;
            }
        }

        public bool DoesVehicleExistsInGarage(string vehicleID)
        {
            bool res = m_VehiclesInGarage.ContainsKey(vehicleID);

            return res;
        }

        public Vehicle GetVehicle(string vehicleID)
        {
            if (!m_VehiclesInGarage.ContainsKey(vehicleID))
            {
                throw new ArgumentException("vehicleID");
            }

           Vehicle vehicle = m_VehiclesInGarage[vehicleID];
 
            return vehicle;
        }

        public bool AddVehicleToGarage(string vehicleID, Vehicle vehicleToAdd)
        {
            bool vehicleAlreadyInside = m_VehiclesInGarage.ContainsKey(vehicleID);

            if (!vehicleAlreadyInside)
            {
                m_VehiclesInGarage.Add(vehicleID, vehicleToAdd);
            }

            m_VehiclesInGarage[vehicleID].OwnerCard.VehicleStatus = eStatusOfVehicleInGarage.Fixing;

            return vehicleAlreadyInside;
        }

        public void ChangeVehicleStatus(string vehicleID, eStatusOfVehicleInGarage newVehicleStatus)
        {
            if (m_VehiclesInGarage[vehicleID].OwnerCard.VehicleStatus == newVehicleStatus)
            {
                string errorStr = string.Format("vehicle status is already {0}", Enum.GetName(typeof(eStatusOfVehicleInGarage), newVehicleStatus));
                throw new Exception(errorStr.ToString());
            }

            m_VehiclesInGarage[vehicleID].OwnerCard.VehicleStatus = newVehicleStatus;
        }

        public List<string> GetVehicleIDsList()
        {
            List<string> vehicleIDs = new List<string>();

                foreach (Vehicle vehicle in m_VehiclesInGarage.Values)
                {
                    vehicleIDs.Add(vehicle.VehicleID);
                }
            
            return vehicleIDs;
        }

        public List<string> GetFilteredVehicleIDsList(eStatusOfVehicleInGarage filterByStatus)
        {
            List<string> vehicleIDs = new List<string>();

                foreach (Vehicle vehicle in m_VehiclesInGarage.Values)
                {
                    if (vehicle.OwnerCard.VehicleStatus == filterByStatus)
                    {
                        vehicleIDs.Add(vehicle.VehicleID);
                    }
                }

            return vehicleIDs;
        }  

        public void IncreaseAirPressureInWheelsToMaximum(string vehicleID)
        {
            float AmountOfAirPressureToAdd;

            foreach (Wheel vehicleWheel in m_VehiclesInGarage[vehicleID].VehicleWheels)
            {
                AmountOfAirPressureToAdd = vehicleWheel.MaximumAirPressure - vehicleWheel.CurrentAirPressure;
                vehicleWheel.addAirPressure(AmountOfAirPressureToAdd);
            }
        }

        public void FuelVehicle(string vehicleID, eFuelType fuelType, float amountOfFuelToAdd)
        {
            PoweredByFuel powerType = m_VehiclesInGarage[vehicleID].PowerType as PoweredByFuel;

            if (powerType != null)
            {
                powerType.addFuel(fuelType, amountOfFuelToAdd);
            }
        }

        public void ChargeVehicle(string vehicleID, float NumberOfMinutesToCharge)
        {
            PoweredByElectricity powerType = m_VehiclesInGarage[vehicleID].PowerType as PoweredByElectricity;

            if (powerType != null)
            {
                float NumOfhoursToCharge = NumberOfMinutesToCharge / 60;
                powerType.ChargeBattery(NumOfhoursToCharge);
            }
        }

        public string GetVehicleInformation(string vehicleID)
        {
          Vehicle vehicle = m_VehiclesInGarage[vehicleID];

            StringBuilder returnedDataStr = new StringBuilder("Vehicle ID Number: ");
            returnedDataStr.Append(Environment.NewLine);

            string str = " ";
            return str;
        }
    }
}