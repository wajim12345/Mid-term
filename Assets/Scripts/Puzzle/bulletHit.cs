using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class bulletHit : MonoBehaviour
{
    public int inputDigits;
    [SerializeField] TMP_Text digit_text;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (other.gameObject.layer == 7)
            {
                inputDigits++;
            }
            else if (other.gameObject.layer == 8)
            {
                inputDigits--;
            }

            digit_text.text = Mathf.Abs(inputDigits).ToString();

            if (inputDigits < 0)
            {

                digit_text.color = Color.red;
            }
            else if (inputDigits > 0)
            {
                digit_text.color = Color.blue;
            }
            else
            {
                digit_text.color = Color.white;
            }

            Destroy(other.gameObject);
        }
    }






}
