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

    public class GenericStatsClass
    {
        /*VARIABLES*/
        private statStruct m_HP;

        /*GET & SET*/
        public statStruct getHP() { return m_HP; }
        public void modifyHP(int value) { m_HP.modifyCurrentValue(value); }
        public void initializeHP(int value) { m_HP = new statStruct(value); }
        public void fillHPtoMax() { m_HP.modifyCurrentValue(m_HP.getMaxValue()); }

        /*CONSTRUCTORS*/
        public GenericStatsClass()
        {
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->GenericStatsClass()"); };

            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-GenericStatsClass()"); };
        }
    }
}
