using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
   public enum eFuelType
    {
        Soler = 1, Octan95, Octan96, Octan98
    }

   public class PoweredByFuel : PowerType
    {
        private eFuelType m_FuelType;

       public PoweredByFuel(eFuelType fuelType, float i_maxFuelTankCapacity)
        {
            FuelType = fuelType;
            MaximumFuelTankCapacity = i_maxFuelTankCapacity;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public float CurrentAmountOfFuel
        {
            get
            {
                return m_RemainingEnergy;
            }

            set
            {
                m_RemainingEnergy = value;
            }
        }

        public float MaximumFuelTankCapacity
        {
            get
            {
                return m_MaximumEnergyCapacity;
            }

            set
            {
                m_MaximumEnergyCapacity = value;
            }
        }

        public void addFuel(eFuelType fuelTypeToAdd, float AmountOfFuelToAdd)
        {
            if (m_FuelType == fuelTypeToAdd)
            {
                float futureAmountOfFuel = CurrentAmountOfFuel + AmountOfFuelToAdd;

                if (futureAmountOfFuel > m_MaximumEnergyCapacity)
                {
                    throw new ValueOutOfRangeException("Value out of range exception", 0, m_MaximumEnergyCapacity);
                }

                FuelType = fuelTypeToAdd;
                CurrentAmountOfFuel = futureAmountOfFuel;
            }
            else
            {
                throw new ArgumentException("FuelType");
            }
        }
    }
}
