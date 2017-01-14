using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    class TerrainClass : StatsClass
    {
        /* VARIABLES */
        private Guid m_unique_character_id;
        private GameTimeClass m_birthDate;
        private BlockClass m_ownerBlockClass;

        private int m_fatigueCost;
        private string m_terrainType;

        /*GET & SET*/
        public Guid getGuid() { return m_unique_character_id; }
        public GameTimeClass getBirthDate() { return m_birthDate; }
        public int getFatigueCost() { return m_fatigueCost; }

        public void setBlockOwner(BlockClass block) { m_ownerBlockClass = block; }

        /*CONSTRUCTORS*/
        public TerrainClass(BlockClass block, string terrainType)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->TerrainClass()"); };

            m_birthDate = new GameTimeClass(ConstantClass.gameTime);
            m_unique_character_id = new Guid();

            m_fatigueCost = 0;
            m_terrainType = terrainType;
            setBlockOwner(block);
            

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-TerrainClass()"); };
        }
    }
}
