using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class MappingClass<T>
    {
        /*VARIABLES*/
        private Dictionary<Guid,T> m_mappingTable;

        /*GET & SET*/
        public Dictionary<Guid,T> getMappingTable() { return m_mappingTable; }

        /*CONSTRUCTORS*/
        public MappingClass()
        {
            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->MappingClass()"); };

            m_mappingTable = new Dictionary<Guid, T>();

            /*DEBUG HIGH*/ if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-MappingClass()"); };
        }
    }
}
