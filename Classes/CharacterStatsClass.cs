using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{    
    public class CharacterStatsClass:GenericStatsClass //inherits from GenericStatClass
    {
        /* VARIABLES */
        private ConstantClass.CHARACTER_SATIETY_STATUS m_hunger_status;
        private ConstantClass.CHARACTER_SLEEP_STATUS m_sleep_status;

        private statStruct m_hunger_rate; // max = how many hours character can last without eating, when current = max then hungry
        private statStruct m_sleep_rate; //max = how many hours character can last without sleeping, when current = max, then sleepy        
        private statStruct m_Energy;        
        
        /*GET & SET*/
        public statStruct getEnergy() { return m_Energy; }
        public statStruct getHungerRate() { return m_hunger_rate; }
        public statStruct getSleepRate() { return m_sleep_rate; }
        public ConstantClass.CHARACTER_SATIETY_STATUS getHungerStatus() { return m_hunger_status; }
        public ConstantClass.CHARACTER_SLEEP_STATUS getSleepStatus() { return m_sleep_status; }
               
        public void setHungerStatus (ConstantClass.CHARACTER_SATIETY_STATUS newStatus) { m_hunger_status = newStatus; }
        public void setSleepStatus (ConstantClass.CHARACTER_SLEEP_STATUS newStatus) { m_sleep_status = newStatus; }
        public void modifyHungerRate(int value) { m_hunger_rate.modifyCurrentValue(value); }
        public void modifySleepRate(int value) { m_sleep_rate.modifyCurrentValue(value); }
        public void modifyEnergy(int value) { m_Energy.modifyCurrentValue(value); }        

        public void initializeHungerRate(int current, int max) { m_hunger_rate = new statStruct(current, max); }
        public void initializeSleepRate(int current, int max) { m_sleep_rate = new statStruct(current, max); }
        public void initializeEnergy(int value) { m_Energy = new statStruct(value); }        

        public void fillEnergytoMax() { m_Energy.modifyCurrentValue(m_Energy.getMaxValue()); }                
        public bool isEnergyAtMax() { return (m_Energy.getCurrentValue() == m_Energy.getMaxValue()); }

        /* METHODS */

        //constructor
        public CharacterStatsClass()
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        //print outs
        public string printHungerStatus()
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH


            string output = "";
            if (m_hunger_status == ConstantClass.CHARACTER_SATIETY_STATUS.FULL) output += "FULL";
            else if (m_hunger_status == ConstantClass.CHARACTER_SATIETY_STATUS.HUNGRY) output += "HUNGRY";
            else if (m_hunger_status == ConstantClass.CHARACTER_SATIETY_STATUS.STARVING) output += "STARVING";
            else if (m_hunger_status == ConstantClass.CHARACTER_SATIETY_STATUS.FAMISHED) output += "FAMISHED";
            else output += "Missing Hunger status definition!";

            //output += "(" + m_hunger_rate.getCurrentValue() + "/" + m_hunger_rate.getMaxValue() + ")";

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return output;
        }

        public string printSleepStatus()
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            string output = "";

            if (m_sleep_status == ConstantClass.CHARACTER_SLEEP_STATUS.AWAKE) output += "AWAKE";
            else if (m_sleep_status == ConstantClass.CHARACTER_SLEEP_STATUS.SLEEPY) output += "SLEEPY";
            else if (m_sleep_status == ConstantClass.CHARACTER_SLEEP_STATUS.TIRED) output += "TIRED";
            else output += "ERROR";

            output += "(" + m_sleep_rate.getCurrentValue() + "/" + m_sleep_rate.getMaxValue() + ")";

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return output;
        }

        public void printStats(string charID)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            /*
             * Stats:
             *      - Hunger status:    [status]
             *      - Hunger rate:      [rate]
             *      - Sleep status:     [status]
             *      - Sleep rate:       [rate]
             *      - Energy:           [energy]
             */

            ConstantClass.LOGGER.writeToCharLog("Stats|Hunger status|" + printHungerStatus(),charID);
            ConstantClass.LOGGER.writeToCharLog("Stats|Hunger rate|" + m_hunger_rate.printStat(), charID);
            ConstantClass.LOGGER.writeToCharLog("Stats|Sleep status|" + printSleepStatus(), charID);
            ConstantClass.LOGGER.writeToCharLog("Stats|Sleep rate|" + m_sleep_rate.printStat(), charID);
            ConstantClass.LOGGER.writeToCharLog("Stats|Energy|" + m_Energy.printStat(), charID);
            ConstantClass.LOGGER.writeToCharLog("Stats|Health Points|" + base.getHP().printStat(), charID);
            ConstantClass.LOGGER.writeToCharLog("Stats|Life Status|" + base.getLifeStatus().ToString(), charID);

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
