using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PD3MyLibrary
{
    // 角度関係用の変数、原則単位は度
    [System.Serializable]
    public struct RotationAngle3
    {
        public float theta1;
        public float theta2;
        public float theta3;

        public RotationAngle3(float theta1,float theta2,float theta3)
        {
            this.theta1 = theta1;
            this.theta2 = theta2;
            this.theta3 = theta3;
        }
    }

    // 長さ関係用の変数
    [System.Serializable]
    public struct Length
    {
        public float l1;
        public float l2;
        public float l3;
    }

    // 脚の座標番号
    public enum LegNumber
    {
        Sigma1, Sigma2, Sigma3, Sigma4
    }

    // バネ係数用の変数
    [System.Serializable]
    public struct Damping
    {
        public float l1;
        public float l2;
        public float l3;
    }

    // ダンパ係数用の変数
    [System.Serializable]
    public struct Stiffness
    {
        public float l1;
        public float l2;
        public float l3;
    }
}