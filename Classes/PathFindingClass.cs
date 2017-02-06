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
        public Guid m_blockID;
        public int m_X_value;
        public int m_Y_value;
        public int m_F_value;
        public int m_G_value;
        public int m_H_value;
        public Node m_parent = null;

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
            m_blockID = blockID;
            
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }

        public Node(int x , int y, Guid blockID)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            m_X_value = x;
            m_Y_value = y;
            m_blockID = blockID;            

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
        public Guid returnNextBlockGuidToMove() //returns the next best block guid to move based on A* pathfinding algorithm
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

                List<Node> walkableAdjBlocks = findWalkableAdjBlocks(m_currentNode.m_X_value, m_currentNode.m_Y_value); //retrieve list of walkable adj blocks 
                g++;

                foreach (Node adjBlock in walkableAdjBlocks)
                {
                    if (m_closedList.FirstOrDefault(obj => obj.m_X_value == adjBlock.m_X_value  //if adjBlock is already in closed list, ignore
                        && obj.m_Y_value == adjBlock.m_Y_value) != null)
                    {
                        continue;
                    }

                    if (m_openList.FirstOrDefault(obj => obj.m_X_value == adjBlock.m_X_value //if adjBlock is not in open list
                        && obj.m_Y_value == adjBlock.m_Y_value) == null)
                    {
                        adjBlock.m_G_value = g;
                        adjBlock.m_H_value = calculateH(adjBlock.m_X_value, adjBlock.m_Y_value, m_endNode.m_X_value, m_endNode.m_Y_value);
                        adjBlock.m_F_value = adjBlock.m_G_value + adjBlock.m_H_value;
                        adjBlock.m_parent = m_currentNode;

                        m_openList.Insert(0, adjBlock);
                    }
                    else //adjBlock is in open list
                    {
                        if (g + adjBlock.m_H_value < adjBlock.m_F_value) // test if using the current G score makes the adjacent square's F score
                                                                         // lower, if yes update the parent because it means it's a better path
                        {
                            adjBlock.m_G_value = g;
                            adjBlock.m_F_value = adjBlock.m_G_value + adjBlock.m_H_value;
                            adjBlock.m_parent = m_currentNode;
                        }
                    }
                }
            }

            while (m_currentNode.m_parent.m_parent != null) //1 node before the start node 
            {
                m_currentNode = m_currentNode.m_parent;
            }

            return m_currentNode.m_blockID;

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH            
        }

        private List<Node> findWalkableAdjBlocks(int x, int y)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            List<Node> proposal = new List<Node>();
            RoomClass room = ConstantClass.MAPPING_TABLE_FOR_ALL_ROOMS.getMappingTable()[m_currentRoomID];

            //Node rightNode = new Node(x + 1, y, room.getRoom()[x + 1, y].getUniqueBlockID()); //right of node
            //Node leftNode = new Node(x - 1, y, room.getRoom()[x - 1, y].getUniqueBlockID()); //left of node
            //Node bottomNode = new Node(x, y + 1, room.getRoom()[x, y + 1].getUniqueBlockID()); //bottom of node
            //Node topNode = new Node(x, y - 1, room.getRoom()[x, y - 1].getUniqueBlockID()); //top of node
            Node rightNode = null;
            Node leftNode = null;
            Node bottomNode = null;
            Node topNode = null;

            bool rightFlag = true;
            bool leftFlag = true;
            bool bottomFlag = true;
            bool topFlag = true;

            //proposal.Add(rightNode);
            //proposal.Add(leftNode);
            //proposal.Add(bottomNode);
            //proposal.Add(topNode);

            //do not add x - 1(left node)
            if (x == 0 || (room.getRoom()[x - 1, y].getBuildingID() != Guid.Empty)) //out of room bounds or building exists 
            {
                //rightNode = new Node(x + 1, y, room.getRoom()[x + 1, y].getUniqueBlockID()); //right of node                
                //bottomNode = new Node(x, y + 1, room.getRoom()[x, y + 1].getUniqueBlockID()); //bottom of node
                //topNode = new Node(x, y - 1, room.getRoom()[x, y - 1].getUniqueBlockID()); //top of node
                leftFlag = false;
            }

            //do not add y - 1 (top node)
            if (y == 0 || (room.getRoom()[x, y - 1].getBuildingID() != Guid.Empty)) //out of room bounds or building exists 
            {
                //rightNode = new Node(x + 1, y, room.getRoom()[x + 1, y].getUniqueBlockID()); //right of node
                //leftNode = new Node(x - 1, y, room.getRoom()[x - 1, y].getUniqueBlockID()); //left of node
                //bottomNode = new Node(x, y + 1, room.getRoom()[x, y + 1].getUniqueBlockID()); //bottom of node                
                topFlag = false;
            }

            //do not add x + 1 (right node)
            if ((x == room.getSize() - 1) || (room.getRoom()[x + 1, y].getBuildingID() != Guid.Empty)) //out of room bounds or building exists 
            {
                //leftNode = new Node(x - 1, y, room.getRoom()[x - 1, y].getUniqueBlockID()); //left of node
                //bottomNode = new Node(x, y + 1, room.getRoom()[x, y + 1].getUniqueBlockID()); //bottom of node
                //topNode = new Node(x, y - 1, room.getRoom()[x, y - 1].getUniqueBlockID()); //top of node
                rightFlag = false;
            }

            // do not add y + 1 (bottom node)
            if ((y == room.getSize() - 1) || (room.getRoom()[x, y + 1].getBuildingID() != Guid.Empty)) //out of room bounds or building exists
            {
                //rightNode = new Node(x + 1, y, room.getRoom()[x + 1, y].getUniqueBlockID()); //right of node
                //leftNode = new Node(x - 1, y, room.getRoom()[x - 1, y].getUniqueBlockID()); //left of node                
                //topNode = new Node(x, y - 1, room.getRoom()[x, y - 1].getUniqueBlockID()); //top of node
                bottomFlag = false;
            }

            if (topFlag)
            {
                topNode = new Node(x, y - 1, room.getRoom()[x, y - 1].getUniqueBlockID()); //top of node
                proposal.Add(topNode);
            }
            if (leftFlag)
            {
                leftNode = new Node(x - 1, y, room.getRoom()[x - 1, y].getUniqueBlockID()); //left of node   
                proposal.Add(leftNode);
            }
            if (rightFlag)
            {
                rightNode = new Node(x + 1, y, room.getRoom()[x + 1, y].getUniqueBlockID()); //right of node
                proposal.Add(rightNode);
            }
            if (bottomFlag)
            {
                bottomNode = new Node(x, y + 1, room.getRoom()[x, y + 1].getUniqueBlockID()); //bottom of node
                proposal.Add(bottomNode);
            }

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return proposal;            
        }

        private int calculateH (int x, int y, int targetX, int targetY)
        {
            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("->" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH

            return (Math.Abs(targetX - x) + Math.Abs(targetY - y)); //city block distance calculation method

            if (ConstantClass.DEBUG_LOG_LEVEL == ConstantClass.DEBUG_LEVELS.HIGH) { ConstantClass.LOGGER.writeToDebugLog("<-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); } //DEBUG HIGH
        }
    }
}
