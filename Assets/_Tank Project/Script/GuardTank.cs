using System;
using UnityEngine;
using BehaviourTreePackage;
using UnityEngine.AI;

namespace TankProject
{
    public class GuardTank : MonoBehaviour
    {

        [SerializeField] private float moveSpeed = 15;
        [SerializeField] private float turnSpeed = 15;
        [SerializeField] private float initialHealth = 15;
        [SerializeField] private float rechargeRate = 0.1f;
        [SerializeField] private float countDownThreshold = 15;
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private Transform cooldownArea;

        private int currentWaypointIndex = 0;
        private float currentHealth = 0;
        private NavMeshAgent tankAgent = null;
        private BehaviourTree behaviourTree = null;

        private void OnEnable()
        {
            tankAgent = GetComponent<NavMeshAgent>();
            tankAgent.Warp(transform.position);
            tankAgent.speed = moveSpeed;
            tankAgent.angularSpeed = turnSpeed;
            currentHealth = initialHealth;
            AssembleTree();
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

            behaviourTree = new BehaviourTree(rootNode);
        }

        private bool isEmergencyModeOn() => currentHealth < 0;
        private bool hasReachedCooldownArea()
        {
            return (transform.position - cooldownArea.position).magnitude < 0.5f
                && currentHealth < 0;
        }
    
        private void Patrol()
        {
            countDown = 0;
            currentHealth -= Time.deltaTime;
            tankAgent.isStopped = false;
            tankAgent?.SetDestination(waypoints[currentWaypointIndex].position);

            if (tankAgent.remainingDistance < 0.5f)
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
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
            if (countDown >= countDownThreshold)
                currentHealth = initialHealth;
        }

        private void Update() => behaviourTree?.Tick();
        
    }
}
