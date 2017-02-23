using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    public class ResourceObjectClass:GenericObjectClass
    {
        /*VARIABLES*/
        private ConstantClass.RESOURCE_TYPE m_resource_type;


        /*CONSTRUCTORS*/
        public ResourceObjectClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public ResourceObjectClass(ConstantClass.RESOURCE_TYPE type, int value)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_resource_type = type;
            switch (type)
            {
                case ConstantClass.RESOURCE_TYPE.WOOD:
                    base.setName("WOOD");
                    base.setQuantity(value);
                    break;
                case ConstantClass.RESOURCE_TYPE.FOOD:
                    base.setName("FOOD");
                    base.setQuantity(value);
                    break;
                case ConstantClass.RESOURCE_TYPE.NONE:
                    base.setName("");
                    base.setQuantity(0);
                    break;
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public ResourceObjectClass(ConstantClass.RESOURCE_TYPE type)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            
            m_resource_type = type;
            switch (type)
            {
                case ConstantClass.RESOURCE_TYPE.WOOD:
                    base.setName("WOOD");
                    base.setQuantity(1);
                    break;
                case ConstantClass.RESOURCE_TYPE.FOOD:
                    base.setName("FOOD");
                    base.setQuantity(1);
                    break;
                case ConstantClass.RESOURCE_TYPE.NONE:
                    base.setName("");
                    base.setQuantity(0);
                    break;
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*OVERRIDE*/
        public override string ToString()
        {
            return base.ToString();
        }

    }
}
