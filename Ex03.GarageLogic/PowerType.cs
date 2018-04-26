using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class PowerType
    {
        protected float m_RemainingEnergy;
        protected float m_MaximumEnergyCapacity;

        public float RemainingEnergy
        {
            get
            {
                return m_RemainingEnergy;
            }

            set
            {
                if (value <= m_MaximumEnergyCapacity)
                {
                    m_RemainingEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException("RemainingEnergy", 0, m_MaximumEnergyCapacity - m_RemainingEnergy);
                }         
            }
        }

        public float MaxEnergy
        {
            get
            {
                return m_MaximumEnergyCapacity;
            }
        }
    }
}
