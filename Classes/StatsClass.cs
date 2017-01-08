using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    //generic stats structure
    public struct statStruct
    {
        private int maxValue;
        private int currentValue;

        //constructor with initial value, maxvalue = currentvalue = value
        public statStruct(int value)
        {
            currentValue = value;
            maxValue = currentValue;
        }

        //return currentValue
        public int getCurrentValue()
        {
            return currentValue;
        }

        //return maxValue
        public int getMaxValue()
        {
            return maxValue;
        }

        //method to change value, method ensures 0<=currentvalue <=maxvalue
        //RETURNS: 1 = SUCCESS , -1 = FAILED (currentVal is above maxValue or currentVal is below 0)
        public int modifyCurrentValue(int value)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->modifyCurrentValue()"); };
            
            if (currentValue + value > maxValue || currentValue + value < 0) { return -1; }
            else { currentValue += value; return 1; }

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-modifyCurrentValue()"); };
        }
    }

    public class StatsClass
    {
        /* VARIABLES */
        private ConstantClass.CHARACTER_HUNGER_STATUS m_hunger_status;
        private ConstantClass.CHARACTER_SLEEP_STATUS m_sleep_status;

        private statStruct m_HP;
        private statStruct m_Energy;

        /*GET & SET*/
        public statStruct getHP() { return m_HP; }
        public statStruct getEnergey() { return m_Energy; }
        public ConstantClass.CHARACTER_HUNGER_STATUS getHungerStatus() { return m_hunger_status; }
        public ConstantClass.CHARACTER_SLEEP_STATUS getSleepStatus() { return m_sleep_status; }

        public void setHP(int value) { m_HP = new statStruct(value); }
        public void setEnergy (int value) { m_Energy = new statStruct(value); }
        public void setHungerStatus (ConstantClass.CHARACTER_HUNGER_STATUS newStatus) { m_hunger_status = newStatus; }
        public void setSleepStatus (ConstantClass.CHARACTER_SLEEP_STATUS newStatus) { m_sleep_status = newStatus; }


        /* METHODS */

        //constructor
        public StatsClass()
        {
            /*DEBUG HIGH*/if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->StatsClass()"); };
                                                                         
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-StatsClass()"); };
        }
    }
}
