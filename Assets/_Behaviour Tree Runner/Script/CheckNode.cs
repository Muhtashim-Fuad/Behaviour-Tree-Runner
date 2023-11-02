using System;

namespace BehaviourTreePackage
{
    public class CheckNode : BehaviourTreeNode
    {
        private readonly Func<bool> conditionToCheck;
        public CheckNode(Func<bool> conditionToCheck)
        {
            this.conditionToCheck = conditionToCheck;
        }

        public override NodeStatus Evaluate()
        {
            return this.conditionToCheck() ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
        }
    }
}
