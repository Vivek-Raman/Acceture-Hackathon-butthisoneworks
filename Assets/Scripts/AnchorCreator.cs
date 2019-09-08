using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

// TODO: make TP Area mat seethrough
public class AnchorCreator : MonoBehaviour
{
    [SerializeField] private GameObject AnchorPrefab = null;
    [SerializeField] private List<Transform> ToAnchor = null;
    [SerializeField] private Transform Parent = null;

    Rigidbody rb = null;
    //MeshCollider mc = null;
    Interactable ia = null;
    LockToPoint lp = null;
    Interact ic = null;

    private void Start()
    {
        foreach (Transform part in ToAnchor)
        {
            // checks for required components and adds them

            // sets layer to IgnoreAllCollisions
            part.gameObject.layer = 8;

            // mesh collider on convex + trigger
            //if (!part.TryGetComponent<MeshCollider>(out mc))
            //{
            //    mc = part.gameObject.AddComponent<MeshCollider>();
            //}
            //mc.convex = true;

            // rigidbody with gravity off
            if (!part.TryGetComponent<Rigidbody>(out rb))
            {
                rb = part.gameObject.AddComponent<Rigidbody>();
            }
            rb.useGravity = false;

            // Interactable
            if (!part.TryGetComponent<Interactable>(out ia))
            {
                ia = part.gameObject.AddComponent<Interactable>();
            }

            // lock to point
            if (!part.TryGetComponent<LockToPoint>(out lp))
            {
                lp = part.gameObject.AddComponent<LockToPoint>();
            }

            // interact
            if (!part.TryGetComponent<Interact>(out ic))
            {
                ic = part.gameObject.AddComponent<Interact>();
            }
            ic.enabled = false;

            GameObject anchor = Instantiate(AnchorPrefab, part.position, part.rotation, Parent);
            anchor.name = part.name + " Anchor";
            LockToPoint chain = part.GetComponent<LockToPoint>();
            chain.SetSnapToObject(anchor.transform);
        }

        Debug.Log("Anchors set for objects in " + this.name);
    }
}
