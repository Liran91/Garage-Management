using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_VehicleID;
        protected float m_PercentOfRemainingEnergyInPowerSource;
        protected List<Wheel> m_VehicleWheels;
        protected PowerType m_PowerType;
        protected VehicleOwnerCard m_OwnerCard;

       protected Vehicle()
        {
            m_ModelName = string.Empty;
            m_VehicleID = string.Empty;
            m_PercentOfRemainingEnergyInPowerSource = 0;
            m_PowerType = null;
            m_OwnerCard = new VehicleOwnerCard();
        }

      public abstract StringBuilder GetSpecificVehicleInformation(StringBuilder infoStr);

      public abstract List<string> GetInputRequestMesseges();

      public abstract void ExtractSpecificVehicleDataValues(List<string> inputList);

        public float GetPercentOfRemainingEnergyInPowerSource()
      {
          return (PowerType.RemainingEnergy / PowerType.MaxEnergy) * 100;
      }

      public List<Wheel> VehicleWheels
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

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public string VehicleID
        {
            get
            {
                return m_VehicleID;
            }

            set
            {
                m_VehicleID = value;
            }
        }

        public float PercentOfRemainingEnergyInPowerSource
        {
            get
            {
                return m_PercentOfRemainingEnergyInPowerSource;
            }

            set
            {
                m_PercentOfRemainingEnergyInPowerSource = value;
            }
        }

        public VehicleOwnerCard OwnerCard
        {
            get
            {
                return m_OwnerCard;
            }
        }

        public PowerType PowerType
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
    }
}
