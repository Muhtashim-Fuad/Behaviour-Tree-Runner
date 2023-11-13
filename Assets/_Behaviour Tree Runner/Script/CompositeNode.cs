namespace BehaviourTree
{
    public abstract class CompositeNode : BehaviourTreeNode
    {
        protected readonly BehaviourTreeNode[] childrenNodes;

        public CompositeNode(params BehaviourTreeNode[] childrenNodes)
        {
            this.childrenNodes = childrenNodes;
        }
    }
}