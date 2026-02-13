using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microgestures;

namespace Microgestures
{
    [AddComponentMenu("SimulRepresentationList", 0)]
    public class SimulRepresentationList: MonoBehaviour
    {
        public GameObject[] simulRepresentations;
        private int active=0;

        public void Start()
        {
            for (int i = 0; i < simulRepresentations.Length; i++)
            {
                simulRepresentations[i] = UnityEngine.Object.Instantiate(simulRepresentations[i], this.transform);
                simulRepresentations[i].SetActive(false);
            }
            if (simulRepresentations.Length > 0)
            {
                simulRepresentations[0].SetActive(true);
            }
        }

        public void Next()
        {
            simulRepresentations[active].SetActive(false);
            active++;
            if (active >= simulRepresentations.Length) {
                active = 0;
            }
            simulRepresentations[active].SetActive(true);
        }

        // public void setActive(bool state)
        // {
        //     for (int i = 0; i < simulRepresentations.Length; i++)
        //     {
        //         simulRepresentations[i].SetActive(state);
        //     }
        // }
    }
}
