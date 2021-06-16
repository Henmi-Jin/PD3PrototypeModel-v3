using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGenerator
{

    static float prevTime;
    static int currentPoint;

    List<Vector3> controlPoints;
    
    // 静的コンストラクタ
    static PatternGenerator()
    {
        prevTime = Time.time;
        currentPoint = 0;

        TimePeriod = 1;
        NumberOfControlPoints = 0;
    }

    public PatternGenerator()
    {
        controlPoints = new List<Vector3>();
        Phase = 0;
    }

    public static void Cycle()
    {
        if(Time.time - prevTime >= TimePeriod)
        {
            prevTime = Time.time;
            
            currentPoint++;
            if(currentPoint==NumberOfControlPoints)currentPoint = 0;
            
        }
    }

    public void AddControlPoint(Vector3 position)
    {
        controlPoints.Add(position);
        // NumberOfControlPoints++;
    }

    public void SetControlPoint(int index, Vector3 position)
    {
        controlPoints[index] = position;
    }

    public Vector3 GetPoint()
    {
        // Debug.Log($"{controlPoint[currentPoint]:F4}");

        int point = currentPoint + Phase;
        if(point >= NumberOfControlPoints)point -= NumberOfControlPoints; 
        return controlPoints[point];
    }

    public int Phase{get; set;}

    public static float TimePeriod{get; set;}
    public static int NumberOfControlPoints{get; set;}
}

