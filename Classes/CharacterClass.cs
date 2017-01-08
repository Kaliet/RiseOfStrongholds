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
        private List<ActionClass> m_action_this_turn;

        /*GET & SET*/
        public string getName() { return m_character_name; }
        public StatsClass getStats() { return m_stats; }
        public GameTimeClass getBirthDate() { return m_birthDate; }  
        public Guid getUniqueCharacterID () { return m_unique_character_id; } 
        public ConstantClass.CHARACTER_ACTIONS getActionThisTurn() { return m_action_this_turn; }     

        /*CONSTRUCTORS*/
        public CharacterClass()
        {
            /*DEBUG HIGH*/if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->CharacterClass()"); };
            
            m_character_name = "";

            m_stats = new StatsClass();
            m_stats.setHP(10);
            m_stats.setEnergy(10);
            m_stats.setHungerStatus(ConstantClass.CHARACTER_HUNGER_STATUS.FULL);
            m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE);

            m_birthDate = new GameTimeClass(ConstantClass.gameTime);
            m_unique_character_id = Guid.NewGuid(); //unique id for character
            m_action_this_turn = new List<ActionClass>();
            
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-CharacterClass()"); };
        }

        /*METHODS*/
        public void updateAction() //character decides what to do now and performs the action
        {
            if (m_action_this_turn.Count > 0) //there are still actions left in the queue
            {
                //perform highest priority action in action list
            }
            else 
            {
                /*DECISIONS BASED ON BIOLOGICAL NEEDS*/
                if (m_stats.getHungerStatus() == ConstantClass.CHARACTER_HUNGER_STATUS.HUNGRY) { m_action_this_turn.Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.EAT)); }
                if (m_stats.getSleepStatus() == ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY) { m_action_this_turn.Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.SLEEP)); }

                /*NOTHING ELSE TO DO*/
                
            }
        }
    }
}
