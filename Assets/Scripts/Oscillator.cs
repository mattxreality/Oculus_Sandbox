using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // sets movement values for x,y,z. Default 10f
    [SerializeField] Vector3 scaleVector = new Vector3(2f, 2f, 2f);

    // time it takes to complete one full cycle
    [SerializeField] float period = 2f;
    [SerializeField] float randomizeFactor = 2;

    [Range(0, 1)] [SerializeField] float scaleFactor;

    Vector3 startingScale; // must be stored for absolute movement
    Vector3 randomizeStartScale;

    float randomNumber;
    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(-randomizeFactor, randomizeFactor);
        randomizeStartScale = new Vector3(randomNumber, randomNumber, randomNumber);
        startingScale = transform.localScale + randomizeStartScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            Debug.Log("cycle Not a Number (NaN)");
            return;
        }

        //Grows continually from zero
        float cycles = Time.time / (period+randomNumber);
        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);
        scaleFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = scaleVector * scaleFactor;
        // set position of the Game Object this script is applied to
        transform.localScale = startingScale + offset;


        // this is one way of doing it. Is not smooth and goes all the way to zero.
        //PingPongScaler();
    }

    private void PingPongScaler()
    {
        // Set the x position to loop between 0 and 3
        float pingPong = Mathf.PingPong(Time.time, 3);
        transform.localScale = new Vector3(pingPong, pingPong, pingPong);
    }
}
