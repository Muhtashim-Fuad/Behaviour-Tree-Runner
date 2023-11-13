using System;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

namespace TankProject
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class GuardTank : MonoBehaviour
    {

        [SerializeField] private float initialHealth = 15;
        [SerializeField] private float rechargeRate = 0.1f;
        [SerializeField] private float rechargeTime = 15;
        [SerializeField] private float tickFrequency = 0.1f;
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private Transform cooldownArea;
        
        private float currentHealth = 0;
        private NavMeshAgent tankAgent = null;
        private BehaviourTree.BehaviourTree behaviourTree = null;

        private void OnEnable()
        {
            tankAgent = GetComponent<NavMeshAgent>();
            tankAgent.Warp(transform.position);
            currentHealth = initialHealth;
            AssembleTree();
            behaviourTree?.Initialize();
        }

        private void AssembleTree()
        {
            BehaviourTreeNode rootNode =
                new SelectorNode
                (
                    new SequenceNode
                    (
                        new CheckNode(hasReachedCooldownArea),
                        new LeafNode(Recharge)
                    ),

                    new SequenceNode
                    (
                        new CheckNode(isEmergencyModeOn),
                        new LeafNode(GoToCooldownArea)
                    ),

                    new LeafNode(Patrol)
                );

            behaviourTree = new BehaviourTree.BehaviourTree(rootNode, tickFrequency);
        }

        private bool isEmergencyModeOn() => currentHealth < 0;
        private bool hasReachedCooldownArea()
        {
            return (transform.position - cooldownArea.position)
                .magnitude < 0.5f
                && currentHealth < 0;
        }

        private int currentWaypointIndex = 0;
        private void Patrol()
        {
            currentHealth -= Time.deltaTime;
            tankAgent.isStopped = false;
            
            if (tankAgent.remainingDistance < 0.5f)
            {
                tankAgent?.SetDestination(waypoints[currentWaypointIndex].position);
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }

        private void GoToCooldownArea()
        {
            tankAgent?.SetDestination(cooldownArea.position);
        }

        private float countDown = 0;
        private void Recharge()
        {
            tankAgent.isStopped = true;
            countDown += rechargeRate;
            if (countDown >= rechargeTime)
            {
                countDown = 0;
                currentHealth = initialHealth;
            }
        }
    }
}
