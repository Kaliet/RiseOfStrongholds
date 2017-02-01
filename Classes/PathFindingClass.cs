using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseOfStrongholds.Classes
{
    /* A* pathfinding algorithm
     * 
     *  for each block, we need to calculate:
     *      G: The length of the path from the start block to current block
     *      H: The straight-line distance from this block to the end block
     *      F: An estimate of the total distance if taking this route. It’s calculated simply using F = G + H.
     * 
     *  If end block is in a different room, then algorithm should calculate nearest exit, then calculate nearest exit to end block (with similiar assumptions).
     * 
     *  see URL: http://blog.two-cats.com/2014/06/a-star-example/
     *           http://www.policyalmanac.org/games/aStarTutorial.htm
     *           http://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/
     */

    public class PathFindingClass  //using A* pathfinding algorithm to find from block A to block B
    {
        /*VARIABLES*/
        private Guid m_startBlock;
        private Guid m_endBlock;
        private Guid m_roomID;
        private float F;
        private float G;
        private float H;

        /*CONSTRUCTOR*/
        public PathFindingClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public PathFindingClass(Guid startBlock, Guid endBlock, Guid roomID)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_startBlock = startBlock;
            m_endBlock = endBlock;
            m_roomID = roomID;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        public Guid returnNextBlockGuidToMove () //returns the next best block guid to move based on A* pathfinding algorithm
        {

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return Guid.Empty;
        }

        
    }
}
