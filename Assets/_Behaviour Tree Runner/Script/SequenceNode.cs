using System;
using UnityEngine;

namespace BehaviourTreePackage
{
    public class SequenceNode : CompositeNode
    {
        public SequenceNode(params BehaviourTreeNode[] childrenNodes) : base(childrenNodes) { }

        public override NodeStatus Evaluate()
        {
            bool anyChildIsRunning = false;
            foreach (BehaviourTreeNode node in childrenNodes)
            {
                switch (node.Evaluate())
                {
                    case NodeStatus.FAILURE:
                        return NodeStatus.FAILURE;

                    case NodeStatus.SUCCESS:
                        continue;

                    case NodeStatus.RUNNING:
                        anyChildIsRunning = true;
                        continue;

                    default:
                        return NodeStatus.SUCCESS;
                }
            }

            return anyChildIsRunning ? NodeStatus.RUNNING : NodeStatus.SUCCESS;
        }
    }
}
