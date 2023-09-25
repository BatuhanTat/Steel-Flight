using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public GameObject propeller;
    public GameObject propellerBlured;

    private float propellerSpeed = 600.0f;


    //private float timeDelayForBlured = 4f;
    //public bool shouldBlur = true;
    /*     void Start()
        {
            StartCoroutine(PropellerStart());
        } */

    void Update()
    {
        //StartCoroutine(PropellerSpeedIncrease());
        //propeller.SetActive(true);
        propeller.transform.Rotate(Vector3.right * propellerSpeed * Time.deltaTime);

        //propellerBlured.SetActive(true);
        propellerBlured.transform.Rotate(1000 * Time.deltaTime, 0, 0);
    }

    /*     IEnumerator PropellerStart()
        {
            yield return new WaitForSeconds(timeDelayForBlured);
            shouldBlur = true;
        } */

    /*   IEnumerator PropellerSpeedIncrease()
      {
          yield return new WaitForSeconds(1.0f);
          if (propellerSpeed <= 600)
          {
              propellerSpeed += 200;
          }
      } */
}
