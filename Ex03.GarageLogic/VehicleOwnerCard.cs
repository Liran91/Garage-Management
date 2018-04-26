using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eStatusOfVehicleInGarage
    {
        Fixing = 1, Fixed, Paid
    }

    public class VehicleOwnerCard
    {    
        private string m_VehicleOwnerName;
        private string m_VehicleOwnerPhoneNumber;
        private eStatusOfVehicleInGarage m_VehicleStatus;

      public VehicleOwnerCard()
        {
            m_VehicleOwnerName = string.Empty;
            m_VehicleOwnerPhoneNumber = string.Empty;
        }

        public eStatusOfVehicleInGarage VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public string VehicleOwnerName
        {
            get
            {
                return m_VehicleOwnerName;
            }

            set
            {
                m_VehicleOwnerName = value;
            }
        }

        public string VehicleOwnerPhoneNumber
        {
            get
            {
                return m_VehicleOwnerPhoneNumber;
            }

            set
            {
                m_VehicleOwnerPhoneNumber = value;
            }
        }
    }
}