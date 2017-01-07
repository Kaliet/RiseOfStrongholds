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
            if (currentValue + value > maxValue || currentValue + value < 0) { return -1; }
            else { currentValue += value; return 1; }
        }
    }

    public class StatsClass
    {
        /* VARIABLES */
        public statStruct m_HP;
        public statStruct m_MP;

        /* METHODS */

        //constructor
        public StatsClass()
        {
            /*DEBUG HIGH*/if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t->StatsClass()"); };

            /*TEST CASE for statStruct
            for (int i = 0; i <= 10; i++)
            {
                m_HP = new statStruct(2*i);
                ConstantClass.LOGGER.writeToLog("\tHP: " + m_HP.getCurrentValue());
                ConstantClass.LOGGER.writeToLog("\tHP(" + i + "): " + m_HP.modifyCurrentValue(i) + " newVal: " + m_HP.getCurrentValue());
                ConstantClass.LOGGER.writeToLog("\tHP(" + -i + "): " + m_HP.modifyCurrentValue(-i) + " newVal: " + m_HP.getCurrentValue());
                ConstantClass.LOGGER.writeToLog("\tHP(" + i + "): " + m_HP.modifyCurrentValue(i) + " newVal: " + m_HP.getCurrentValue());
                ConstantClass.LOGGER.writeToLog("\tHP(" + i + "): " + m_HP.modifyCurrentValue(i) + " newVal: " + m_HP.getCurrentValue());
            }
            */           
            
             
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToLog("\t<-StatsClass()"); };
        }
    }
}
