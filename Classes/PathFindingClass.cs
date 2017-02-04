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
     *           
     *           
     */

    public class Node
    {
        /*VARIABLES*/
        public int m_X_value;
        public int m_Y_value;
        public int m_F_value;
        public int m_G_value;
        public int m_H_value;
        public Node m_parent;

        /*CONSTRUCTOR*/
        public Node()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public Node(Guid blockID)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_X_value = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[blockID].getPosition().getPositionX();
            m_Y_value = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[blockID].getPosition().getPositionY();

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        private int calculateH(int x, int y, int targetX, int targetY)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (Math.Abs(targetX - x) + Math.Abs(targetY - y));
        }
    }

    public class PathFindingClass  //using A* pathfinding algorithm to find from block A to block B
    {
        /*VARIABLES*/
        private Guid m_startBlock;
        private Guid m_endBlock;
        private Guid m_currentRoomID;
        private Node m_currentNode;
        private Node m_startNode;
        private Node m_endNode;
        private List<Node> m_openList;
        private List<Node> m_closedList;

        /*CONSTRUCTOR*/
        public PathFindingClass()
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
            
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public PathFindingClass(Guid startBlock, Guid endBlock)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_startBlock = startBlock;
            m_endBlock = endBlock;
            m_currentRoomID = ConstantClass.MAPPING_TABLE_FOR_ALL_BLOCKS.getMappingTable()[startBlock].getRoomID();
            m_startNode = new Node(m_startBlock);
            m_endNode = new Node(m_endBlock);
            m_openList = new List<Node>();
            m_closedList = new List<Node>();
            m_currentNode = null;

            m_openList.Add(m_startNode);

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        /*METHODS*/
        public Guid returnNextBlockGuidToMove () //returns the next best block guid to move based on A* pathfinding algorithm
        {            
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            int g = 0; //value of starting location to the current tile

            while (m_openList.Count > 0)
            {
                //algorithm logic
                int lowest = m_openList.Min(obj => obj.m_F_value);                              //1. finds the lowest F value node in the open list
                m_currentNode = m_openList.First(obj => obj.m_F_value == lowest);               //2. picks the first node with the lowest F value and assigns as current node
                m_closedList.Add(m_currentNode);                                                //3. add current node to close list
                m_openList.Remove(m_currentNode);                                               //4. remove current node from closed list
                if (m_closedList.FirstOrDefault(obj => obj.m_X_value == m_endNode.m_X_value     //5. if current nocde equals endnode then we finished.
                    && obj.m_Y_value == m_endNode.m_Y_value) != null)
                {
                    break;
                }

                List<Node> walkableAdjBlocks = findWalkableAdjBlocks(m_currentNode.m_X_value, m_currentNode.m_Y_value);


            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH            
        }

        public List<Node> findWalkableAdjBlocks (int x, int y)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            List<Node> proposal = new List<Node>()
            {
                new Node ()
            }

            RoomClass room = ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[m_currentRoomID];

            

            return proposal;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        
    }
}
