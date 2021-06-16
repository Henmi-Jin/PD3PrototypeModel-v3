using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PD3MyLibrary
{

    public class MainController : MonoBehaviour
    {


        /*
        // 実験用の目標位置
        [SerializeField]
        Vector3 targetPosition1;//0.03413
        [SerializeField]
        Vector3 targetPosition4;
        */
        [SerializeField]
        Vector3 targetPosition = new Vector3(-0.15f, -0.03413f, -0.04f);

        public bool start=true;


        [SerializeField]
        private RobotController robot;

        // RobotController robotController;

        // 制御周期用
        private float prevTime;

        // 逆運動学計算機の生成
        IKCalculator sigma1IK;
        IKCalculator sigma2IK;
        IKCalculator sigma3IK;
        IKCalculator sigma4IK;



        // パターン生成器の生成
        PatternGenerator sigma1PG;
        PatternGenerator sigma2PG;
        PatternGenerator sigma3PG;
        PatternGenerator sigma4PG;

        bool erorr = false;
        bool hogeFlag = false;

        // Start is called before the first frame update
        void Start()
        {
            erorr = false;
            if (robot != null) { erorr = true; }
            else { return; }

            prevTime = Time.time;

            start = true;

            PatternGenerator.NumberOfControlPoints = 22;
            // PatternGenerator.TimePeriod = 0.12f;最初の値
            PatternGenerator.TimePeriod = 0.05f;

            float yPos = 0.05f;
            float xBias = 0.025f;
            float zBias = -0.02f;
            

            sigma1PG = new PatternGenerator();
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.04f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.14f+xBias, -yPos, -0.03f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0.02f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0.01f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0.01f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0.02f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.14f+xBias, -yPos, 0.03f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f + xBias, -yPos, 0.04f + zBias));

              sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.04f + zBias));
              sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.04f+zBias));
              sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.04f+zBias));
              
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.03f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.02f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.01f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.01f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.02f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f + xBias, -yPos, -0.03f + zBias));

              sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.04f+zBias));
              sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.04f+zBias));
              sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.04f+zBias));
              
            sigma2PG = new PatternGenerator();
            sigma2PG.Phase = 11;
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.04f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.14f+xBias, -yPos, -0.03f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0.02f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0.01f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0.01f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0.02f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.14f+xBias, -yPos, 0.03f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f + xBias, -yPos, 0.04f + zBias));

              sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.04f+zBias));
              sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.04f+zBias));
              sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.04f+zBias));
              
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.03f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.02f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.01f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.01f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.02f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f + xBias, -yPos, -0.03f + zBias));

              sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.04f+zBias));
              sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.04f+zBias));
              sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.04f+zBias));

            sigma3PG = new PatternGenerator();
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.04f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.14f+xBias, yPos, -0.03f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0.02f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0.01f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0.01f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0.02f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.14f+xBias, yPos, 0.03f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f + xBias, yPos, 0.04f + zBias));

              sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.04f+zBias));
              sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.04f+zBias));
              sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.04f+zBias));
              
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.03f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.02f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.01f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.01f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.02f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f + xBias, yPos, -0.03f + zBias));

              sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.04f+zBias));
              sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.04f+zBias));
              sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.04f+zBias));

            sigma4PG = new PatternGenerator();
            sigma4PG.Phase = 11;
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.04f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.14f+xBias, yPos, -0.03f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0.02f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0.01f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0.01f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0.02f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.14f+xBias, yPos, 0.03f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f + xBias, yPos, 0.04f + zBias));

              sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.04f+zBias));
              sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.04f+zBias));
              sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.04f+zBias));
              
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.03f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.02f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.01f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.01f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.02f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f + xBias, yPos, -0.03f + zBias));

              sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.04f+zBias));
              sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.04f+zBias));
              sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.04f+zBias));
              
            /*
            sigma1PG = new PatternGenerator();
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.020f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.14f+xBias, -yPos, -0.015f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0.010f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0.005f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0.005f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0.010f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.14f+xBias, -yPos, 0.015f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.020f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.015f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.010f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.005f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.005f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.010f+zBias));
            sigma1PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.015f+zBias));

            sigma2PG = new PatternGenerator();
            sigma2PG.Phase = 8;
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.020f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.14f+xBias, -yPos, -0.015f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0.010f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0.005f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, -0f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0.005f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.13f+xBias, -yPos, 0.010f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.14f+xBias, -yPos, 0.015f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.020f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.015f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.010f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0.005f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, 0f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.005f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.010f+zBias));
            sigma2PG.AddControlPoint(new Vector3(-0.15f+xBias, -yPos, -0.015f+zBias));

            sigma3PG = new PatternGenerator();
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.020f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.14f+xBias, yPos, -0.015f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0.010f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0.005f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0.005f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0.010f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.14f+xBias, yPos, 0.015f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.020f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.015f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.010f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.005f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.005f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.010f+zBias));
            sigma3PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.015f+zBias));

            sigma4PG = new PatternGenerator();
            sigma4PG.Phase = 8;
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.020f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.14f+xBias, yPos, -0.015f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0.010f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0.005f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, -0f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0.005f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.13f+xBias, yPos, 0.010f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.14f+xBias, yPos, 0.015f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.020f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.015f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.010f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0.005f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, 0f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.005f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.010f+zBias));
            sigma4PG.AddControlPoint(new Vector3(-0.15f+xBias, yPos, -0.015f+zBias));
 */
            sigma1IK = new IKCalculator(robot.GetLinkLength(LegNumber.Sigma1), LegNumber.Sigma1);
            sigma2IK = new IKCalculator(robot.GetLinkLength(LegNumber.Sigma2), LegNumber.Sigma2);
            sigma3IK = new IKCalculator(robot.GetLinkLength(LegNumber.Sigma3), LegNumber.Sigma3);
            sigma4IK = new IKCalculator(robot.GetLinkLength(LegNumber.Sigma4), LegNumber.Sigma4);
            
            
        }

        void FixedUpdate()
        {
            if (erorr != true) { return; }

            if (Time.time - prevTime >= 0.01f)
            {
                prevTime = Time.time;
                // robot.SetTargetAngles(LegNumber.Sigma1,new RotationAngle3(0f,60f,-120f));
                Debug.Log("hoge");

                if (start == true)
                {
                    var hoge1 = sigma1IK.IKcalculation(sigma1PG.GetPoint());
                    Debug.Log($"y:{hoge1.theta1}");
                    robot.SetTargetAngles(LegNumber.Sigma1, hoge1);
                    var hoge2 = sigma2IK.IKcalculation(sigma2PG.GetPoint());
                    robot.SetTargetAngles(LegNumber.Sigma2, hoge2);
                    var hoge3 = sigma3IK.IKcalculation(sigma3PG.GetPoint());
                    robot.SetTargetAngles(LegNumber.Sigma3, hoge3);
                    var hoge4 = sigma4IK.IKcalculation(sigma4PG.GetPoint());
                    robot.SetTargetAngles(LegNumber.Sigma4, hoge4);
                    // start = false;
                }
                // sigma1Leg.GetComponent<LegController>().SetTargetAngles(hoge1);
                /*
                                var hoge2 = sigma2IK.GetAngleDeg(sigma2PG.GetPoint());
                                sigma2Leg.GetComponent<LegController>().targetAngle = hoge2;

                                var hoge3 = sigma3IK.GetAngleDeg(sigma3PG.GetPoint());
                                sigma3Leg.GetComponent<LegController>().targetAngle = hoge3;

                                var hoge4 = sigma4IK.GetAngleDeg(sigma4PG.GetPoint());
                                sigma4Leg.GetComponent<LegController>().targetAngle = hoge4;
                */


                PatternGenerator.Cycle();
            }
        }

        void Update()
        {


            if(Input.GetKeyDown(KeyCode.Space))
            {
                robot.RemoveConstraints();
            }
            /*
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                run = true;
            }*/
            // erorr    }
        }

    }
}