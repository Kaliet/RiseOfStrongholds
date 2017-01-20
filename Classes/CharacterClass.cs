﻿using System;
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
        private QueueClass<ActionClass> m_action_queue;
        private Guid m_block_id; 

        /*GET & SET*/
        public string getName() { return m_character_name; }
        public StatsClass getStats() { return m_stats; }
        public GameTimeClass getBirthDate() { return m_birthDate; }  
        public Guid getUniqueCharacterID () { return m_unique_character_id; } 
        public Guid getBlockID() { return m_block_id; }

        /*CONSTRUCTORS*/
        public CharacterClass(Guid blockID)
        {
            /*DEBUG HIGH*/if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->CharacterClass()"); };
            
            m_character_name = "";

            m_stats = new StatsClass();
            m_stats.initializeHP(10);
            m_stats.initializeEnergy(50);
            //m_stats.initializeHungerRate(0, ConstantClass.RANDOMIZER.produceInt(1, ConstantClass.HOURS_BETWEEN_EATING * ConstantClass.HOURS_IN_ONE_DAY));
            //m_stats.initializeSleepRate(0, ConstantClass.RANDOMIZER.produceInt(1, ConstantClass.HOURS_BETWEEN_SLEEPING * ConstantClass.HOURS_IN_ONE_DAY));
            m_stats.initializeHungerRate(0, 2000);
            m_stats.initializeSleepRate(0, 2000);
            m_stats.setHungerStatus(ConstantClass.CHARACTER_HUNGER_STATUS.FULL);
            m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE);

            m_birthDate = new GameTimeClass(ConstantClass.gameTime);
            m_unique_character_id = Guid.NewGuid(); //unique id for character            
            m_action_queue = new QueueClass<ActionClass>();

            m_block_id = blockID;

            ConstantClass.MAPPING_TABLE_FOR_ALL_CHARS.getMappingTable().Add(m_unique_character_id, this);
                    
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-CharacterClass()"); };
        }

        /*METHODS*/       
        public int returnIndexOfActionWithHighestIndex()//returns the FIRST index from m_action_this_turn that has the highest priority action
        {
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->returnIndexOfActionWithHighestIndex()"); };

            int highestPriority = ConstantClass.ACTION_NO_PRIORITY; //0 is highest, then 1,2,3...is lower, no priority = 99999
            int index = -1;

            if (m_action_queue.getQueue().Count <= 0) { return -1; }
            else if (m_action_queue.getQueue().Count == 1) { return 0; }
            else //list has at least 2 elements
            {
                int count = 0;
                foreach (ActionClass action in m_action_queue.getQueue())
                {
                    if (action.getPriority() < highestPriority)
                    {
                        highestPriority = action.getPriority();  //found higher index
                        index = count;
                    }
                    count++;
                }
                return index;
            }
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-returnIndexOfActionWithHighestIndex()"); };
        }

        public string outputPersonGUID()
        {
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->outputPersonGUID()"); };
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-outputPersonGUID()"); };
            return "Person " + m_unique_character_id.ToString().Substring(4, 2) + "(" + m_stats.getEnergy().getCurrentValue() + "/" + m_stats.getEnergy().getMaxValue() + ")";

        }


        public void updateAction() //character decides what to do now and performs the action
        {
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->updateAction()"); };
            int index = -1;

            ConstantClass.LOGGER.writeToQueueLog(outputPersonGUID() + " = " + m_action_queue.printQueue());//print queue
            ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is in block position (" + 
                ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getPosition().getPositionX() + "," + 
                ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].getPosition().getPositionY() + "). Exits: " + ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[m_block_id].printAllAvailableExits());

            m_stats.modifyHungerRate(ConstantClass.GAME_SPEED); //hunger increases based on game speed (1 sec = how many game time mins)
            m_stats.modifySleepRate(ConstantClass.GAME_SPEED); //sleepiness increases based on game speed (1 sec = how many game time mins)

            if (m_action_queue.getQueue().Count > 0) //there are still actions left in the queue
            {
                //perform highest priority action in action list                
                index = returnIndexOfActionWithHighestIndex();
                if (index < 0) throw new Exception("Queue empty.");
                if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.EAT) // EAT
                {
                    //TODO: character eats something or goes to find something to eat
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is EATING");
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_FOR_EATING);
                    m_stats.initializeHungerRate(0, m_stats.getHungerRate().getMaxValue());
                    m_stats.setHungerStatus(ConstantClass.CHARACTER_HUNGER_STATUS.FULL);
                    //TODO: add HP from eating.
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is FULL");
                    m_action_queue.getQueue().RemoveAt(index); //EAT action completed - removed from queue
                }
                else if (m_action_queue.getQueue()[index].getAction() == ConstantClass.CHARACTER_ACTIONS.SLEEP) // SLEEP
                {
                    //TODO: character goes to sleep until he replenishes his energy                    
                    if (m_action_queue.getQueue()[index].getVarForAction() > 0)//wait until MINIMUM_NUMBER_OF_SLEEP_HOURS 
                    {
                        //wait - character is sleeping
                        ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is SLEEPING");
                        m_action_queue.getQueue()[index].modifyVarForAction(-1*ConstantClass.GAME_SPEED);
                        m_stats.modifyEnergy(ConstantClass.ENERGY_ADD_WHEN_SLEEP);
                    }
                    else //sleeping is over, reinitialize sleep rate and set status to AWAKE
                    {
                        m_stats.initializeSleepRate(0, m_stats.getSleepRate().getMaxValue());
                        m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE);
                        ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is AWAKE");
                        m_action_queue.getQueue().RemoveAt(index); //SLEEP action completed - removed from queue
                    }
                }
                
            }
            else 
            {
                /*UPDATE BIOLOGICAL DETERIORATION*/
                if (m_stats.getHungerRate().getCurrentValue() == m_stats.getHungerRate().getMaxValue()) //current = max --> hunger state
                {
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is HUNGRY");
                    m_stats.setHungerStatus(ConstantClass.CHARACTER_HUNGER_STATUS.HUNGRY);
                }
                if (m_stats.getSleepRate().getCurrentValue() == m_stats.getSleepRate().getMaxValue() || (m_stats.getEnergy().getCurrentValue() == 0))
                {
                    ConstantClass.LOGGER.writeToGameLog(outputPersonGUID() + " is SLEEPY");
                    m_stats.setSleepStatus(ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY);
                }

                /*DECISIONS BASED ON BIOLOGICAL NEEDS*/
                if (m_stats.getHungerStatus() == ConstantClass.CHARACTER_HUNGER_STATUS.HUNGRY)
                {
                    m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.EAT,ConstantClass.ACTION_EAT_PRIORITY,ConstantClass.VARIABLE_FOR_ACTION_NONE));
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_WHEN_HUNGRY); //if hungry , start deducting energy
                }
                if (m_stats.getSleepStatus() == ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY)
                {
                    m_action_queue.getQueue().Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.SLEEP,ConstantClass.ACTION_SLEEP_PRIORITY,ConstantClass.MINIMUM_NUMBER_OF_SLEEP_HOURS*ConstantClass.MINUTES_IN_ONE_HOUR));
                    m_stats.modifyEnergy(ConstantClass.ENERGY_COST_WHEN_SLEEPY); //if sleepy , start deducting energy
                }

                /*NOTHING ELSE TO DO*/
                //if (m_stats.getHungerStatus() != ConstantClass.CHARACTER_HUNGER_STATUS.HUNGRY && //if not hungry and tired then remain idle
                //    m_stats.getSleepStatus() != ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY)
                //{
                //    m_action_this_turn.Add(new ActionClass(ConstantClass.CHARACTER_ACTIONS.IDLE)); 
                //}                
            }
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-updateAction()"); };
        }
    }
}
