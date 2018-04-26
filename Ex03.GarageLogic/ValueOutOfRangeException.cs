using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_maxValue;
        private float m_minValue;

        public ValueOutOfRangeException(string msg, float minValue, float maxValue)
            : base(msg)
        {
            m_maxValue = maxValue;
            m_minValue = minValue;
        }

        public override string ToString()
        {
            return string.Format("{0} value must be between the range {1:f} and {2:f}.", Message, m_minValue, m_maxValue);
        }

        public float MaxValue
        {
            get
            {
                return m_maxValue;
            }

            set
            {
                m_maxValue = value;
            }
        }

        public float MinValue
        {
            get
            {
                return m_minValue;
            }

            set
            {
                m_minValue = value;
            }
        }
    }
}
