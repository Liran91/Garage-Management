using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
   public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaximumAirPressure;

       public Wheel()
        {
            m_ManufacturerName = string.Empty;
            m_MaximumAirPressure = 0;
            m_CurrentAirPressure = 0;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float MaximumAirPressure
        {
            get
            {
                return m_MaximumAirPressure;
            }

            set
            {
                m_MaximumAirPressure = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public void addAirPressure(float amountOfPressureToAdd)
        {       
            float desiredNewAirPressure = m_CurrentAirPressure + amountOfPressureToAdd;

            if (desiredNewAirPressure > m_MaximumAirPressure)
            {
                throw new ValueOutOfRangeException("Value out of range exception", 0, m_MaximumAirPressure);
            }

            m_CurrentAirPressure = desiredNewAirPressure;
        }
    }
}
