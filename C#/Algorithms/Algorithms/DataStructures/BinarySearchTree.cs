using System;
using System.Collections.Generic;

namespace Algorithms {
    static class BinarySearchTree {

        public class Node {
            public int BalanceFactor = 0;
            public Node Parent = null;
            public Node LeftChild;
            public Node RightChild;
            public int Value;

            public Node(int val, Node leftChild, Node rightChild) {
                Value = val;
                LeftChild = leftChild;
                RightChild = rightChild;
                if (leftChild != null || rightChild != null)
                    this.BalanceFactor = ((leftChild != null) ? LeftChild.BalanceFactor : 0) - ((rightChild != null) ? RightChild.BalanceFactor : 0) + 1;
            }

            public void PrintPretty(string indent, bool last) {
                Console.Write(indent);
                if (last) {
                    Console.Write("\\-");
                    indent += "  ";
                }
                else {
                    Console.Write("|-");
                    indent += "| ";
                }
                Console.WriteLine(Value);
                LeftChild.PrintPretty(indent, (LeftChild == null) || (RightChild == null));
            }
        }

        public static Node BuildTree(List<int> data, int start, int end) {
            Node leftChild = null;
            Node rightChild = null;
            int mid = (start + end) / 2;
            if (start < end) {
                leftChild = BuildTree(data, start, mid);
                rightChild = BuildTree(data, mid + 1, end);
            }
            var parent = new Node(data[mid], leftChild, rightChild);
            leftChild.Parent = parent;
            rightChild.Parent = parent;
            return parent;
        }

        public static void DeleteNode(int valueToDelete, Node startingNode) {
            Node nodeToDelete = FindNode(valueToDelete, startingNode);
            Node selectedLeaf = null;

            // Find the leaf to replace the deleted node.
            if (nodeToDelete.RightChild != null) {
                Node cursorNode = nodeToDelete.RightChild;
                while (true) {
                    if (cursorNode.LeftChild != null)
                        cursorNode = cursorNode.LeftChild;
                    else {
                        selectedLeaf = cursorNode;
                        break;
                    }
                }
            }
            else
                selectedLeaf = nodeToDelete.LeftChild;

            // Change the reference to nodeToDelete in its parent's child attribute 
            if (nodeToDelete.Value < nodeToDelete.Parent.Value)
                nodeToDelete.Parent.LeftChild = selectedLeaf;
            else
                nodeToDelete.Parent.RightChild = selectedLeaf;
            nodeToDelete.Parent = null;

            // Remove the reference to selectedLeaf from its original parent
            if (selectedLeaf.Value < selectedLeaf.Parent.Value)
                selectedLeaf.Parent.LeftChild = null;
            else
                selectedLeaf.Parent.RightChild = null;

            // Give selected leaf a new parent
            selectedLeaf.Parent = nodeToDelete.Parent;

            // Selected leaf is assigned new children
            selectedLeaf.LeftChild = nodeToDelete.LeftChild;
            selectedLeaf.RightChild = nodeToDelete.RightChild;

            // if nodeToDelete has children, reassign their parent references
            if (nodeToDelete.LeftChild != null)
                nodeToDelete.LeftChild.Parent = selectedLeaf;
            if (nodeToDelete.RightChild != null)
                nodeToDelete.RightChild.Parent = selectedLeaf;

            // Finish isolating nodeToDelete so it can get picked up by Garbage collection.
            nodeToDelete.LeftChild = null;
            nodeToDelete.RightChild = null;
        }

        public static Node FindNode(int valueBeingSearchedFor, Node currentNode) {
            if (currentNode == null)
                return null;
            else if (valueBeingSearchedFor == currentNode.Value)
                return currentNode;
            else if (valueBeingSearchedFor < currentNode.Value)
                return FindNode(valueBeingSearchedFor, currentNode.LeftChild);
            else
                return FindNode(valueBeingSearchedFor, currentNode.RightChild);
        }

        public static void InsertValue(int valueToInsert, Node currentNode) {
            while (true) {
                if (valueToInsert < currentNode.Value) {
                    if (currentNode.LeftChild != null)
                        currentNode = currentNode.LeftChild;
                    else
                        currentNode.LeftChild = new Node(valueToInsert, null, null);
                    break;
                }
                else {
                    if (currentNode.RightChild != null)
                        currentNode = currentNode.RightChild;
                    else
                        currentNode.RightChild = new Node(valueToInsert, null, null);
                    break;
                }
            }
        }
    }
}