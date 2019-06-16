using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    public List<Transform> Segments = new List<Transform>();

    public float mindistance = 0.25f;

    public int beginSize;

    public float speed = 1;
    public float rotationspeed = 50;

    public GameObject segmentPrefab;

    private float dis;
    private Transform curSegment;
    private Transform prevSegment;


    void Start()
    {
        for(int i = 0; i < beginSize - 1; i++)
        {
            AddSegment();
        }
    }


    void Update()
    {
        Move();

        if(Input.GetKey(KeyCode.T))
        {
            AddSegment();
        }
    }

    public void Move()
    {
        float curspeed = speed;

        if (Input.GetKey(KeyCode.Y))
        {
            curspeed *= 2;
        }

        Segments[0].Translate(Segments[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

        //if (Input.GetAxis("Horizontal") != 0)
        //{
        //    Segments[0].Rotate(Vector3.up * rotationspeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        //}

        //if (Input.GetAxis("Vertical") != 0)
        //{
        //    Segments[0].Rotate(Vector3.right * rotationspeed * Time.deltaTime * Input.GetAxis("Vertical"));
        //}


        for (int i = 1; i < Segments.Count; i++)
        {
            curSegment = Segments[i];
            prevSegment = Segments[i - 1];

            dis = Vector3.Distance(prevSegment.position, curSegment.position);

            Vector3 newpos = prevSegment.position;

            newpos.y = Segments[0].position.y;

            float t = Time.deltaTime * dis / mindistance * curspeed;

            if (t > 0.5f)
            { t = 0.5f; }
            curSegment.position = Vector3.Slerp(curSegment.position, newpos, t);
            curSegment.rotation = Quaternion.Slerp(curSegment.rotation, prevSegment.rotation, t);
        }

    }

    public void AddSegment()
    {
        Transform newsegment = (Instantiate(segmentPrefab, Segments[Segments.Count - 1].position, Segments[Segments.Count - 1].rotation) as GameObject).transform;

        newsegment.SetParent(transform);

        Segments.Add(newsegment);
    }
}
