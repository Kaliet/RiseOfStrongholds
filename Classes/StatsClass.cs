﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    //generic stats structure
    public struct statStruct
    {
        /*VARIABLES*/
        private int maxValue;
        private int currentValue;

        /*CONSTRUCTORS*/
        public statStruct(int current, int max)
        {
            currentValue = current;
            maxValue = max;
        }

        //constructor with initial value, maxvalue = currentvalue = value
        public statStruct(int value)
        {
            currentValue = value;
            maxValue = currentValue;
        }

        /*GET & SET*/
        public int getCurrentValue() { return currentValue; }        
        public int getMaxValue() { return maxValue; }

        //method to change value, method ensures 0<=currentvalue <=maxvalue
        //RETURNS: 1 = SUCCESS , -1 = FAILED (currentVal is above maxValue or currentVal is below 0)
        public void modifyCurrentValue(int value)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->modifyCurrentValue()"); };

            if (currentValue + value > maxValue)
            {
                currentValue = maxValue;
            }
            else if (currentValue + value < 0)
            {
                currentValue = 0;
            }
            else { currentValue += value; }

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-modifyCurrentValue()"); };
        }
    }

    public class StatsClass
    {
        /* VARIABLES */
        private ConstantClass.CHARACTER_HUNGER_STATUS m_hunger_status;
        private ConstantClass.CHARACTER_SLEEP_STATUS m_sleep_status;

        private statStruct m_hunger_rate; // max = how many hours character can last without eating, when current = max then hungry
        private statStruct m_sleep_rate; //max = how many hours character can last without sleeping, when current = max, then sleepy
        private statStruct m_HP;
        private statStruct m_Energy;

        /*GET & SET*/
        public statStruct getHP() { return m_HP; }
        public statStruct getEnergey() { return m_Energy; }
        public statStruct getHungerRate() { return m_hunger_rate; }
        public statStruct getSleepRate() { return m_sleep_rate; }
        public ConstantClass.CHARACTER_HUNGER_STATUS getHungerStatus() { return m_hunger_status; }
        public ConstantClass.CHARACTER_SLEEP_STATUS getSleepStatus() { return m_sleep_status; }

        public void setHP(int value) { m_HP = new statStruct(value); }
        public void setEnergy (int value) { m_Energy = new statStruct(value); }        
        public void setHungerStatus (ConstantClass.CHARACTER_HUNGER_STATUS newStatus) { m_hunger_status = newStatus; }
        public void setSleepStatus (ConstantClass.CHARACTER_SLEEP_STATUS newStatus) { m_sleep_status = newStatus; }
        public void setHungerRate(int value) { m_hunger_rate.modifyCurrentValue(value); }
        public void setSleepRate(int value) { m_sleep_rate.modifyCurrentValue(value); }

        public void initializeHungerRate(int current, int max) { m_hunger_rate = new statStruct(current, max); }
        public void initializeSleepRate(int current, int max) { m_sleep_rate = new statStruct(current, max); }

        /* METHODS */

        //constructor
        public StatsClass()
        {
            /*DEBUG HIGH*/if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->StatsClass()"); };
                                                                         
            /*DEBUG HIGH*/if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-StatsClass()"); };
        }

        //print outs
        public string printHungerStatus()
        {
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->printHungerStatus()"); };
            
            string output = "";
            if (m_hunger_status == ConstantClass.CHARACTER_HUNGER_STATUS.FULL) output += "FULL";
            else if (m_hunger_status == ConstantClass.CHARACTER_HUNGER_STATUS.HUNGRY) output += "HUNGRY";
            else output += "ERROR";

            output += "(" + m_hunger_rate.getCurrentValue() + "/" + m_hunger_rate.getMaxValue() + ")";

            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-printHungerStatus()"); };

            return output;
        }

        public string printSleepStatus()
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->printSleepStatus()"); };

            string output = "";

            if (m_sleep_status == ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE) output += "AWAKE";
            else if (m_sleep_status == ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY) output += "SLEEPY";
            else output += "ERROR";

            output += "(" + m_sleep_rate.getCurrentValue() + "/" + m_sleep_rate.getMaxValue() + ")";

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-printSleepStatus()"); };

            return output;
        }
    }
}
