using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomPasswordGenerator : MonoBehaviour
{
    [SerializeField] GameObject bluePrefab;
    [SerializeField] GameObject redPrefab;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] bulletHit[] input;
    private List<int> digits;

    [SerializeField] UnityEvent validPassword;
    [SerializeField] UnityEvent invalidPassword;

    // Start is called before the first frame update
    void Start()
    {
        digits = new List<int>();
        GeneratePassword();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GeneratePassword()
    {
        for (int i = 0; i < spawnPos.Length; i++)
        {
            int digit = Random.Range(-9, 10);
            digits.Add(digit);
        }
        GenerateHintObjects();
        Debug.Log(digits);

    }

    private void GenerateHintObjects()
    {
        for (int i = 0; i < digits.Count; i ++)
        {
            if (digits[i] == 0)
            {
                Debug.Log($"{i + 1} digit is zero");
            }
            else if (digits[i] > 0)
            {
                //instantiate i number of blue objects at spawnPos[i]
                for (int j = 0; j < digits[i]; j++)
                {
                    int numberOfObjects = digits[i];
                    for (int k = 0; k < numberOfObjects; k ++)
                    {
                        Vector3 spawnPosition = spawnPos[i].position + Vector3.up * k;
                        Instantiate(bluePrefab, spawnPosition, Quaternion.identity);
                    }
                }
                
            }
            else if (digits[i] < 0)
            {
                //instantiate i number of red objects at spawnPos[i]
                for (int j = 0; j > digits[i]; j--)
                {
                    int numberOfObjects = Mathf.Abs(digits[i]);
                    for (int k = 0; k < numberOfObjects; k++)
                    {
                        Vector3 spawnPosition = spawnPos[i].position + Vector3.up * k;
                        Instantiate(redPrefab, spawnPosition, Quaternion.identity);
                    }
                }

                
            }
        }
    }

    public void CheckDigit()
    {
        int validDigit = 0;
        for (int i = 0; i < digits.Count; i++)
        {

            if (digits[i] == input[i].inputDigits)
            {
                validDigit++;
            }
        }

        if (validDigit == 4)
        {
            validPassword?.Invoke();
        }
        else
        {
            invalidPassword?.Invoke();
        }

    }

}
