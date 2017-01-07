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
        public statStruct m_HP;
        public statStruct m_Energy;

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
