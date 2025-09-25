using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microgestures;

namespace Microgestures
{
    [AddComponentMenu("SimultaneousRep", 0)]
    public class SimultaneousRep: MonoBehaviour
    {
        public GameObject[] representations;

        public void Start()
        {
            for (int i = 0; i < representations.Length; i++)
            {
                representations[i] = UnityEngine.Object.Instantiate(representations[i]);
                representations[i].GetComponent<Representation>().setActive();
            }
        }
    }
}
