using System;
using UnityEngine;

namespace BehaviourTreePackage
{
    public class SelectorNode : CompositeNode
    {
        public SelectorNode(params BehaviourTreeNode[] childrenNodes) : base(childrenNodes) { }

        public override NodeStatus Evaluate()
        {
            foreach (BehaviourTreeNode node in childrenNodes)
            {
                switch (node.Evaluate())
                {
                    case NodeStatus.FAILURE:
                        continue;
                    case NodeStatus.SUCCESS:
                        return NodeStatus.SUCCESS;
                    case NodeStatus.RUNNING:
                        return NodeStatus.RUNNING;
                    default:
                        continue;
                }
            }

            return NodeStatus.FAILURE;
        }
    }
}
