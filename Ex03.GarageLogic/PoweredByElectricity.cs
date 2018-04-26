using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class PoweredByElectricity : PowerType
    {
       public PoweredByElectricity(float maxBatteryCapacity)
        {            
            RemainingBatteryTimeInHours = 0;
            MaximumBatteryCapacity = maxBatteryCapacity;
        }

        public float RemainingBatteryTimeInHours
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

        public float MaximumBatteryCapacity
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

        public void ChargeBattery(float AmountOfHoursToCharge)
        {
            float desiredBatteryChargeTime = RemainingBatteryTimeInHours + AmountOfHoursToCharge;

            if (desiredBatteryChargeTime <= m_MaximumEnergyCapacity)
            {
                RemainingBatteryTimeInHours = desiredBatteryChargeTime;
            }
        }
    }
}
