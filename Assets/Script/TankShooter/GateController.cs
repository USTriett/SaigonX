using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tank"))
        {
            GateSpawner.NumDisabledGate += 1;
            GateSpawner.Collapse(gameObject);
            Scorer.IncreaseScore();
        }
    }
}
