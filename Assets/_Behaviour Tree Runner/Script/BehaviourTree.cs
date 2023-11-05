using System;
using System.Collections;
using UnityEngine;

namespace BehaviourTreePackage
{
    public class BehaviourTree
    {
        private readonly BehaviourTreeNode rootNode = null;
        private readonly float tickFrequency;

        private bool isRunning = false;

        public BehaviourTree(BehaviourTreeNode rootNode, float tickFrequency = 0.5f)
        {
            this.rootNode = rootNode;
            this.tickFrequency = tickFrequency;
            isRunning = true;
        }

        public void Pause() => isRunning = false;
        public void Resume() => isRunning = true;

        public async void Initialize()
        {
            while(isRunning)
            {
                await Awaitable.WaitForSecondsAsync(1 / tickFrequency);
                rootNode?.Evaluate();
            }
        }
        
    }
}
