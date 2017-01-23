using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class TerrainClass : CharacterStatsClass
    {
        /* VARIABLES */
        private Guid m_unique_terrain_id;                
        private int m_fatigueCost;
        private ConstantClass.TERRAIN_TYPE m_terrainType;

        /*GET & SET*/
        public Guid getUniqueTerrainID() { return m_unique_terrain_id; }        
        public int getFatigueCost() { return m_fatigueCost; }
        

        /*CONSTRUCTORS*/
        public TerrainClass(ConstantClass.TERRAIN_TYPE terrainType)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_unique_terrain_id = Guid.NewGuid();            
            m_terrainType = terrainType;
            switch (terrainType)
            {
                case ConstantClass.TERRAIN_TYPE.GRASS:
                    m_fatigueCost = ConstantClass.TERRAIN_FATIGUE_FOR_GRASS;
                    break;
                case ConstantClass.TERRAIN_TYPE.DESERT:
                    m_fatigueCost = ConstantClass.TERRAIN_FATIGUE_FOR_DESERT;
                    break;
                case ConstantClass.TERRAIN_TYPE.HILL:
                    m_fatigueCost = ConstantClass.TERRAIN_FATIGUE_FOR_HILL;
                    break;
                case ConstantClass.TERRAIN_TYPE.DIRT:
                    m_fatigueCost = ConstantClass.TERRAIN_FATIGUE_FOR_DIRT;
                    break;
            }

            ConstantClass.MAPPING_TABLE_FOR_ALL_TERRAINS.getMappingTable().Add(m_unique_terrain_id, this);

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
