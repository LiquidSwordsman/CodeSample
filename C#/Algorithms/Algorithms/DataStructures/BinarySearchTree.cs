using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DataStructures {
    class BinarySearchTree {
        public class Node{
            int BalanceFactor =0;
            Node Parent;
            Node LeftChild;
            Node RightChild;
            int Value;
  
    	    public  Node(int val, Node leftChild, Node rightChild){
          	    Value = val;
                LeftChild = leftChild;
                RightChild = rightChild;
                if(leftChild != null || rightChild != null){
                    this.BalanceFactor = ((leftChild!= null) ? LeftChild.BalanceFactor : 0) - ((rightChild!= null) ? RightChild.BalanceFactor : 0) + 1;
                }
	        }
        }

//This returns the root node of the assembled tree.
public static Node BuildTree(List<int> data, int start, int end){
  Node leftChild = null;
  Node rightChild = null;
  int mid = (start + end) / 2;
  if (start < end) {
    leftChild = BuildTree(data, start, mid);
    rightChild = BuildTree(data, mid+1, end);
  }
  return new Node(data[mid], leftChild, rightChild);
}

public static void DeleteNode(Node node){}
public static void DeleteNode(int val){}

// TODO: This shouldn't return void.
public static void FindValue(){}
public static void InsertValue(){}
    }
}
