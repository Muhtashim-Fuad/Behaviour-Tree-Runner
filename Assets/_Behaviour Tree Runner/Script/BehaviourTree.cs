using System;
using System.Collections;
using UnityEngine;

namespace BehaviourTreePackage
{
    public class BehaviourTree
    {
        private readonly BehaviourTreeNode rootNode = null;
        
        public BehaviourTree(BehaviourTreeNode rootNode)
        {
            this.rootNode = rootNode;
        }
        
        public void Tick()
        {
            rootNode?.Evaluate();
        }
        
    }
}
