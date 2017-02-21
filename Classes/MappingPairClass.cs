using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class MappingPairClass<T>
    {
        /*VARIABLES*/
        private Dictionary<GuidPairClass, T> m_mappingTable;

        /*GET & SET*/
        public Dictionary<GuidPairClass, T> getMappingTable() { return m_mappingTable; }

        /*CONSTRUCTOR*/
        public MappingPairClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_mappingTable = new Dictionary<GuidPairClass, T>();

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        public bool isGuidAKey (Guid input)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                foreach (KeyValuePair<GuidPairClass, T> pair in m_mappingTable)
                {
                    if (pair.Key.isGuidOneofthePairs(input)) return true;
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return false;
        }

        public Guid returnNonVisitedGuidPair (Guid input, List<Guid> visitedGuids)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            try
            {
                foreach (KeyValuePair<GuidPairClass, T> pair in m_mappingTable)
                {
                    //check second Guid Pair to see if it is visited. to avoid endless loop
                    if (visitedGuids.Contains(pair.Key.returnSecondGuidPair(input))) { ConstantClass.LOGGER.writeToGameLog("Block ID " + pair.Key.returnSecondGuidPair(input) + " has been visited already. Skipping."); }
                    if (pair.Key.isGuidOneofthePairs(input) && !visitedGuids.Contains(pair.Key.returnSecondGuidPair(input))) return pair.Key.returnSecondGuidPair(input);
                }
            }
            catch (Exception e)
            {
                ConstantClass.LOGGER.writeToCrashLog(e);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return Guid.Empty;
        }
    }
}
