using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RadialMenu : MonoBehaviour
{
    public GameObject[] Buttons;

    public void Render()
    {
        GetComponentsInChildren<Transform>().Skip(1).ToList().ForEach(c => Destroy(c));

        for (int i = 0; i< Buttons.Length; i++)
        {
            GameObject go = Instantiate(Buttons[i]);
            go.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / Buttons.Length) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            go.transform.localPosition = new Vector3(xPos, yPos, 0) * 100f;
        }
    }
}
