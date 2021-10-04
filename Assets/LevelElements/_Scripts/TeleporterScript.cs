using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleporterScript : MonoBehaviour
{
    public TeleporterScript teleportTo;
    public bool teleporterActive = true;
    [ReadOnly] public bool teleporterOccupied = false;

    public CinemachineVirtualCamera followCamera;
    public float dollyPos;

    public Vector3 spawnOffset = Vector3.zero;
    public float playerLifeChange = 1.0f;

    public void Spawn(PlayerScript player)
    {
        teleporterOccupied = true;

        player.transform.position = transform.position + spawnOffset;
        player.movementController.ballRigidbody.angularVelocity = Vector3.zero;
        player.movementController.ballRigidbody.velocity = Vector3.zero;
        player.movementController.steeringReference = followCamera.transform;

        followCamera.ForceCameraPosition(player.transform.position, Quaternion.identity);
        followCamera.LookAt = player.transform;
        followCamera.Follow = player.transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + spawnOffset, 0.5f);
        Gizmos.color = teleporterActive ? Color.green : Color.grey;

        if (teleportTo) {
            Vector3 here = transform.position + spawnOffset,
                there = teleportTo.transform.position + teleportTo.spawnOffset;
            Gizmos.DrawLine(here, Vector3.MoveTowards(
                here, there, Vector3.Distance(here, there)));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (teleporterOccupied) return;

        Debug.Log("Teleporter Engaged!");
        teleporterOccupied = true;

        PlayerScript player = other.GetComponentInParent<PlayerScript>();
        if (!player) return;

        player.Life += playerLifeChange;
        if (teleportTo) teleportTo.Spawn(player);
    }

    private void OnTriggerExit(Collider other) {
        teleporterOccupied = false;
    }
}
