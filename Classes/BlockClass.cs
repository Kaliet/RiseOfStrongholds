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
        private string m_block_name;
        private string m_block_type; // Should be TypeClass in future
        private CharacterClass m_block_owner;
        private StatsClass m_stats;
        private GameTimeClass m_birthDate;

        /*GET & SET*/
        public string getBlockName() { return m_block_name; }
        public string getBlockType() { return m_block_type; }
        public CharacterClass getBlockOwner() { return m_block_owner; }
        public StatsClass getStats() { return m_stats; }
        public GameTimeClass getBirthDate() { return m_birthDate; }

        public void setBlockName(string blockName) { m_block_name = blockName; }
        public void setBlockType(string blockType) { m_block_type = blockType; }
        public void setBlockOwner(CharacterClass character) { m_block_owner = character; }

        /*CONSTRUCTORS*/
        public BlockClass()
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->BlockClass()"); };

            m_block_name = "";
            m_block_type = "";
            m_stats = new StatsClass();
            m_birthDate = new GameTimeClass(ConstantClass.gameTime);
            m_block_owner = new CharacterClass();

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-BlockClass()"); };
        }
    }
}
