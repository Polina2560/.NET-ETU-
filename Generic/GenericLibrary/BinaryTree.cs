using System;
using System.Collections.Generic;
namespace GenericLibrary
{
    public class BinaryTree<T> where T : IComparable
    {
        public BinaryNode<T> RootNode { get; set; }

        public BinaryNode<T> Add(BinaryNode<T> node, BinaryNode<T> currentNode = null)
        {
            if (RootNode == null)
            {
                node.ParentNode = null;
                return RootNode = node;
            }

            currentNode = currentNode ?? RootNode;
            node.ParentNode = currentNode;
            int result;
            return (result = node.Data.CompareTo(currentNode.Data)) == 0
                ? currentNode
                : result < 0
                    ? currentNode.LeftNode == null
                        ? (currentNode.LeftNode = node)
                        : Add(node, currentNode.LeftNode)
                    : currentNode.RightNode == null
                        ? (currentNode.RightNode = node)
                        : Add(node, currentNode.RightNode);
        }

        public BinaryNode<T> Add(T data)
        {
            return Add(new BinaryNode<T>(data));
        }

    public BinaryNode<T> FindNode(T data, BinaryNode<T> startWithNode = null)
    {
        startWithNode = startWithNode ?? RootNode;
        int result;
        return (result = data.CompareTo(startWithNode.Data)) == 0
            ? startWithNode
            : result < 0
                ? startWithNode.LeftNode == null
                    ? null
                    : FindNode(data, startWithNode.LeftNode)
                : startWithNode.RightNode == null
                    ? null
                    : FindNode(data, startWithNode.RightNode);
    }

    public void Remove(BinaryNode<T> node)
    {
        if (node == null)
        {
            return;
        }

        var currentNodeSide = node.NodeSide;
        //если у узла нет подузлов, можно его удалить
        if (node.LeftNode == null && node.RightNode == null)
        {
            if (currentNodeSide == Side.Left)
            {
                node.ParentNode.LeftNode = null;
            }
            else
            {
                node.ParentNode.RightNode = null;
            }
        }
        //если нет левого, то правый ставим на место удаляемого 
        else if (node.LeftNode == null)
        {
            if (currentNodeSide == Side.Left)
            {
                node.ParentNode.LeftNode = node.RightNode;
            }
            else
            {
                node.ParentNode.RightNode = node.RightNode;
            }

            node.RightNode.ParentNode = node.ParentNode;
        }
        //если нет правого, то левый ставим на место удаляемого 
        else if (node.RightNode == null)
        {
            if (currentNodeSide == Side.Left)
            {
                node.ParentNode.LeftNode = node.LeftNode;
            }
            else
            {
                node.ParentNode.RightNode = node.LeftNode;
            }

            node.LeftNode.ParentNode = node.ParentNode;
        }
        //если оба дочерних присутствуют, 
        //то правый становится на место удаляемого,
        //а левый вставляется в правый
        else
        {
            switch (currentNodeSide)
            {
                case Side.Left:
                    node.ParentNode.LeftNode = node.RightNode;
                    node.RightNode.ParentNode = node.ParentNode;
                    Add(node.LeftNode, node.RightNode);
                    break;
                case Side.Right:
                    node.ParentNode.RightNode = node.RightNode;
                    node.RightNode.ParentNode = node.ParentNode;
                    Add(node.LeftNode, node.RightNode);
                    break;
                default:
                    var bufLeft = node.LeftNode;
                    var bufRightLeft = node.RightNode.LeftNode;
                    var bufRightRight = node.RightNode.RightNode;
                    node.Data = node.RightNode.Data;
                    node.RightNode = bufRightRight;
                    node.LeftNode = bufRightLeft;
                    Add(bufLeft, node);
                    break;
            }
        }
    }

    public void Remove(T data)
    {
        var foundNode = FindNode(data);
        Remove(foundNode);
    }

    public void PrintTree()
    {
        PrintTree(RootNode);
    }

    /// <summary>
    /// Вывод бинарного дерева начиная с указанного узла
    /// </summary>
    /// <param name="startNode">Узел с которого начинается печать</param>
    /// <param name="indent">Отступ</param>
    /// <param name="side">Сторона</param>
    private void PrintTree(BinaryNode<T> startNode, string indent = "", Side? side = null)
    {
        if (startNode != null)
        {
            var nodeSide = side == null ? "+" : side == Side.Left ? "L" : "R";
            Console.WriteLine($"{indent} [{nodeSide}]- {startNode.Data}");
            indent += new string(' ', 3);
            //рекурсивный вызов для левой и правой веток
            PrintTree(startNode.LeftNode, indent, Side.Left);
            PrintTree(startNode.RightNode, indent, Side.Right);
        }
    }

}
}