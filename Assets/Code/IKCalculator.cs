using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PD3MyLibrary
{
    public class IKCalculator
    {

        private float l1, l2, l3;
        private LegNumber leg;
        // RotationAngle3 theta;
        public IKCalculator(Length length, LegNumber leg)
        {
            l1 = length.l1;
            l2 = length.l2;
            l3 = length.l3;
            this.leg = leg;
            // theta.theta1 = theta.theta2 = theta.theta3 = 0f;
        }

        public RotationAngle3 IKcalculation(Vector3 target)
        {
            RotationAngle3 returnAngle = new RotationAngle3() { theta1 = 0f, theta2 = 0f, theta3 = 0f };
            if (leg == LegNumber.Sigma1 || leg == LegNumber.Sigma2)
            {
                // theta3の導出
                float theta3 = -Mathf.Acos((target.x * target.x + target.y * target.y + target.z * target.z - l1 * l1 - l2 * l2 - l3 * l3) / (2 * l2 * l3));
                returnAngle.theta3 = theta3 * Mathf.Rad2Deg;

                // theta2の導出

                float reciprocalOfDet2 = 1 / (-l2 * l2 - l3 * l3 - 2 * l2 * l3 * Mathf.Cos(theta3));
                float aPlus = Mathf.Sqrt(target.x * target.x + target.y * target.y - l1 * l1);

                float cos2 = reciprocalOfDet2 * ((-l2 - l3 * Mathf.Cos(theta3)) * aPlus + (l3 * Mathf.Sin(theta3)) * target.z);
                float sin2 = reciprocalOfDet2 * ((l3 * Mathf.Sin(theta3)) * aPlus + (l2 + l3 * Mathf.Cos(theta3)) * target.z);
                float theta2 = Mathf.Atan2(sin2, cos2);
                returnAngle.theta2 = theta2 * Mathf.Rad2Deg;

                // theta1の導出
                float a = l3 * Mathf.Cos(theta2 + theta3) + l2 * Mathf.Cos(theta2);
                float reciprocalOfDet3 = 1 / (a * a + l1 * l1);

                float cos1 = reciprocalOfDet3 * (-a * target.x - l1 * target.y);
                float sin1 = reciprocalOfDet3 * (l1 * target.x - a * target.y);
                float theta1 = Mathf.Atan2(sin1, cos1);
                returnAngle.theta1 = theta1 * Mathf.Rad2Deg;

            }
            else if (leg == LegNumber.Sigma3 || leg == LegNumber.Sigma4)
            {
                
                // theta3の導出
                float theta3 = Mathf.Acos((target.x * target.x + target.y * target.y + target.z * target.z - l1 * l1 - l2 * l2 - l3 * l3) / (2 * l2 * l3));
                returnAngle.theta3 = theta3 * Mathf.Rad2Deg;

                // theta2の導出

                float reciprocalOfDet2 = 1 / (l2 * l2 + l3 * l3 + 2 * l2 * l3 * Mathf.Cos(theta3));
                float aPlus = Mathf.Sqrt(target.x * target.x + target.y * target.y - l1 * l1);

                float cos2 = reciprocalOfDet2 * ((l2 + l3 * Mathf.Cos(theta3)) * aPlus + (l3 * Mathf.Sin(theta3)) * target.z);
                float sin2 = reciprocalOfDet2 * ((l3 * Mathf.Sin(theta3)) * -aPlus + (l2 + l3 * Mathf.Cos(theta3)) * target.z);
                float theta2 = Mathf.Atan2(sin2, cos2);
                returnAngle.theta2 = theta2 * Mathf.Rad2Deg;

                // theta1の導出
                float a = l3 * Mathf.Cos(theta2 + theta3) + l2 * Mathf.Cos(theta2);
                float reciprocalOfDet3 = 1 / (a * a + l1 * l1);

                float cos1 = reciprocalOfDet3 * (-a * target.x + l1 * target.y);
                float sin1 = reciprocalOfDet3 * (-l1 * target.x - a * target.y);
                float theta1 = Mathf.Atan2(sin1, cos1);
                returnAngle.theta1 = theta1 * Mathf.Rad2Deg;
            }
            else { };


            return returnAngle;
        }

    }
    
}