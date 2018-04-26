using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eVehicleType
    {
        FuelPoweredCar = 1, ElectricityPoweredCar, FuelPoweredMotorcycle, ElectricityPoweredMotorcycle, FuelPoweredTruck
    }

   public static class VehicleFactory
    {            
        public static Vehicle ConstructVehicle(eVehicleType vehicleTypeToConstruct)
        {
            Vehicle constructedVehicle;
            bool electricVehicle = true;

            switch (vehicleTypeToConstruct)
            {
                case eVehicleType.FuelPoweredCar:
                    constructedVehicle = new Car(!electricVehicle);
                    break;
                case eVehicleType.ElectricityPoweredCar:
                    constructedVehicle = new Car(electricVehicle);
                    break;
                case eVehicleType.FuelPoweredMotorcycle:
                    constructedVehicle = new Motorcycle(!electricVehicle);
                    break;
                case eVehicleType.ElectricityPoweredMotorcycle:
                    constructedVehicle = new Motorcycle(electricVehicle);
                    break;
                case eVehicleType.FuelPoweredTruck:
                    constructedVehicle = new Truck(!electricVehicle);
                    break;
                default:
                    throw new System.Exception("Type of vehicle not supported.");
            }

            return constructedVehicle;
        }
    }
}
