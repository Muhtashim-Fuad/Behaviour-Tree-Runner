using System;
using UnityEngine;

namespace BehaviourTreePackage
{
    public enum NodeStatus { RUNNING, SUCCESS, FAILURE }
    public abstract class BehaviourTreeNode
    {
        public abstract NodeStatus Evaluate();
    }
}