using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PD3MyLibrary
{
    public class RobotController : MonoBehaviour
    {
        // Baseとそれぞれの脚のルートオブジェクト

        private GameObject sigma0Base;
        [SerializeField]
        private GameObject sigma1Leg = null;
        [SerializeField]
        private GameObject sigma2Leg = null;
        [SerializeField]
        private GameObject sigma3Leg = null;
        [SerializeField]
        private GameObject sigma4Leg = null;

        // シャーシと脚のルートオブジェクトの配列
        private GameObject[] sigmaLists = new GameObject[5];

        // 脚のLegControllerクラスのリスト Sigma_1からだから気を付けてね☆
        private LegController[] LegControllerLists = new LegController[4];

        void Start()
        {
            sigma0Base = this.gameObject;
            sigmaLists[0] = sigma0Base;
            sigmaLists[1] = sigma1Leg;
            sigmaLists[2] = sigma2Leg;
            sigmaLists[3] = sigma3Leg;
            sigmaLists[4] = sigma4Leg;

            for (int i = 0; i < 4; i++)
            {
                LegControllerLists[i] = sigmaLists[i+1].GetComponent<LegController>();
            }

            Rigidbody rd = GetComponent<Rigidbody>();
            rd.centerOfMass = new Vector3(0f,-1.20f,0.1f);
        }

        void FixedUpdate()
        {

        }

        void Update()
        {
            
        }

        public void SetTargetAngles(LegNumber value,RotationAngle3 degree)
        {
            LegControllerLists[(int)value].SetTargetAngles(degree);
        }

        public Length GetLinkLength(LegNumber value)
        {
            return LegControllerLists[(int)value]._linkLength;
        }

        public void RemoveConstraints()
        {
            Rigidbody rd = GetComponent<Rigidbody>();
            rd.constraints = RigidbodyConstraints.None;
            Debug.Log("constraints removal");
        }
    }

}