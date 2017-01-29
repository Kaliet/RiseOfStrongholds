using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class BuildingClass
    {
        /*VARIABLES*/
        //private GenericStatsClass m_GenericStats;  --> 
        private ConstantClass.BUILDING m_type;
        private Guid m_block_id; //id of block that building is built on it
        private Guid m_building_id; //id of building

        /*GET & SETS*/
        //public GenericStatsClass getGenericStats () { return m_GenericStats; }
        public ConstantClass.BUILDING getType() { return m_type; }
        public Guid getBlockID () { return m_block_id; }        
        public Guid getUniqueID() { return m_building_id; }

        /*CONSTRUCTORS*/
        public BuildingClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public BuildingClass(ConstantClass.BUILDING type, Guid blockID)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_type = type;
            m_block_id = blockID;
            m_building_id = Guid.NewGuid();

            switch (m_type)
            {
                case (ConstantClass.BUILDING.WALL): //case building type is WALL
                    {
                        
                        break;
                    }                    
            }
            
            //updates the mapping tables
            ConstantClass.MAPPING_TABLE_FOR_ALL_BUILDINGS.getMappingTable().Add(m_building_id, this);
            ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].setBuildingID(m_building_id);

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
