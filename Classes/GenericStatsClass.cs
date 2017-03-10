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

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (currentValue + value > maxValue)
            {
                currentValue = maxValue;
            }
            else if (currentValue + value < 0)
            {
                currentValue = 0;
            }
            else { currentValue += value; }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public string printStat()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (currentValue + "/" + maxValue);

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }

    public class GenericStatsClass
    {
        /*VARIABLES*/
        private statStruct m_HP;
        private ConstantClass.CHARACTER_LIFE_STATUS m_lifeStatus;
        private GameTimeClass m_deathDate; //date of death

        /*GET & SET*/
        public statStruct getHP() { return m_HP; }
        public GameTimeClass getDeathDate() { return m_deathDate; }
        public ConstantClass.CHARACTER_LIFE_STATUS getLifeStatus() { return m_lifeStatus; }

        /*METHODS*/
        public void modifyHP(int value)
        {
            if (m_HP.getCurrentValue() + value <= 0)// dead
            {
                m_lifeStatus = ConstantClass.CHARACTER_LIFE_STATUS.DEAD;
                m_deathDate = new GameTimeClass(ConstantClass.gameTime);
            }
            m_HP.modifyCurrentValue(value);
        }
        public void initializeHP(int value)
        {
            m_HP = new statStruct(value);
            if (value > 0) { m_lifeStatus = ConstantClass.CHARACTER_LIFE_STATUS.ALIVE; }
            else
            {
                m_lifeStatus = ConstantClass.CHARACTER_LIFE_STATUS.DEAD;
                m_deathDate = new GameTimeClass(ConstantClass.gameTime);
            }
        }
        public void fillHPtoMax() { m_HP.modifyCurrentValue(m_HP.getMaxValue()); }
        

        /*CONSTRUCTORS*/
        public GenericStatsClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_deathDate = null;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
