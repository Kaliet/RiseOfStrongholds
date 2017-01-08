using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{      
    public class CharacterClass
    {
        /* VARIABLES */
        private string m_character_name; 
        private StatsClass m_stats;
        private GameTimeClass m_birthDate;
        private Guid m_unique_character_id;

        /*GET & SET*/
        public string getName() { return m_character_name; }
        public StatsClass getStats() { return m_stats; }
        public GameTimeClass getBirthDate() { return m_birthDate; }  
        public Guid getUniqueCharacterID () { return m_unique_character_id; }      

        /*CONSTRUCTORS*/
        public CharacterClass()
        {
            /*DEBUG HIGH*/if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->CharacterClass()"); };
            
            m_character_name = "";
            m_stats = new StatsClass();
            m_birthDate = new GameTimeClass(ConstantClass.gameTime);
            m_unique_character_id = Guid.NewGuid(); //unique id for character
            
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-CharacterClass()"); };
        }
    }
}
