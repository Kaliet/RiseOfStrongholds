using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    class PositionClass
    {
        private int positionX;
        private int positionY;
        /*GET & SET*/
        public void setPositionX(int x) { positionX = x; }
        public void setPositionY(int y) { positionY = y; }

        public int x() { return positionX; }
        public int y() { return positionY; }

        /*CONSTRUCTORS*/
        public PositionClass(int positionX, int positionY)
        {
            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t->PositionClass()"); };

            setPositionX(positionX);
            setPositionY(positionY);

            /*DEBUG HIGH*/
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("\t<-PositionClass()"); };
        }
    }
}
