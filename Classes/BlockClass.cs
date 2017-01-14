using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    class BlockClass
    {
        /* VARIABLES */
        private Guid m_unique_character_id;
        private StatsClass m_stats;
        private GameTimeClass m_birthDate;
        private TerrainClass m_terrain;
        private PositionClass m_pos;

        /*GET & SET*/
        public Guid getBlockOwner() { return m_unique_character_id; }
        public StatsClass getStats() { return m_stats; }
        public GameTimeClass getBirthDate() { return m_birthDate; }

        public void setBlockOwner(Guid character_id) { m_unique_character_id = character_id; }
        public void setTerrainType(TerrainClass terrain) { m_terrain = terrain; }

        /*CONSTRUCTORS*/
        public BlockClass()
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->BlockClass()"); };

            m_stats = new StatsClass();
            m_birthDate = new GameTimeClass(ConstantClass.gameTime);
            m_unique_character_id = new Guid();
            m_pos = new PositionClass(0, 0);

            setTerrainType(new TerrainClass(this, "Grass"));

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-BlockClass()"); };
        }
    }
}
