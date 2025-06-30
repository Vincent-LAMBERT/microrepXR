using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microgestures;

namespace Microgestures
{
    [AddComponentMenu("MicroRepToolKit", 0)]
    public class MicroRepToolKit: MonoBehaviour
    {
        public GameObject[] representations;
        private int active=0;

        public void Start()
        {
            for (int i = 0; i < representations.Length; i++)
            {
                representations[i] = UnityEngine.Object.Instantiate(representations[i]);
            }
            representations[0].GetComponent<Representation>().setActive();
        }

        public void Next()
        {
            representations[active].GetComponent<Representation>().setInactive();
            active++;
            if (active >= representations.Length) {
                active = 0;
            }
            representations[active].GetComponent<Representation>().setActive();
        }
    }
}
