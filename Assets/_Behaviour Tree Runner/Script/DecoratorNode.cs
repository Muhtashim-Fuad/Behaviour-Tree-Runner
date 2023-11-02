using System;
using UnityEngine;

namespace BehaviourTreePackage
{
    public abstract class DecoratorNode : BehaviourTreeNode
    {
        protected readonly BehaviourTreeNode childNode;

        public DecoratorNode(BehaviourTreeNode childNode)
        {
            this.childNode = childNode;
        }
        
    }
}