using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microgestures;

namespace Microgestures
{
    [AddComponentMenu("RepresentationList", 0)]
    public class RepresentationList: MonoBehaviour
    {
        public GameObject[] representations;
        private int active=0;

        public void Start()
        {
            for (int i = 0; i < representations.Length; i++)
            {
                representations[i] = UnityEngine.Object.Instantiate(representations[i]);
                representations[i].SetActive(false);
            }
            if (representations.Length > 0)
            {
                representations[0].SetActive(true);
            }
        }

        public void Next()
        {
            representations[active].SetActive(false);
            active++;
            if (active >= representations.Length) {
                active = 0;
            }
            representations[active].SetActive(true);
        }
    }
}
