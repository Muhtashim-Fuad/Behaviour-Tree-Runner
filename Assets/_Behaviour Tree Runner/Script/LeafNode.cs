using System;

namespace BehaviourTree
{
    public class LeafNode : BehaviourTreeNode
    {
        private readonly Action nodeAction;

        public LeafNode(Action nodeAction)
        {
            this.nodeAction = nodeAction;
        }

        public override NodeStatus Evaluate()
        {
            if (nodeAction != null)
            {
                nodeAction.Invoke();
                return NodeStatus.SUCCESS;
            }

            return NodeStatus.FAILURE;
        }
    }
}
