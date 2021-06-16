using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PD3MyLibrary
{

    public class LegController : MonoBehaviour
    {
        [SerializeField]
        private LegNumber legNumber;

        [SerializeField]
        [Range(0, 5)]
        private int ArticulationDepth;

        // 脚の長さ
        [SerializeField]
        public Length _linkLength;// = new Length{l1=1,l2=1,l3=1};

        // 関節角の初期角度
        [SerializeField]
        public RotationAngle3 initialAngle;

        [SerializeField]
        private Stiffness stiffness;

        [SerializeField]
        private Damping damping;

        

        // ArticulationBodyの全階層を扱うリスト
        // List<int> getDof = new List<int>();
        List<int> dofIndices = new List<int>();
        List<float> driveTargetAngles = new List<float>();
        List<float> driveTargetAngularVelocities = new List<float>();

        List<ArticulationBody> ariculationBodyLists = new List<ArticulationBody>();
        ArticulationBody rootLegArtBody = null;

        // Start is called before the first frame update
        void Start()
        {
            rootLegArtBody = GetComponent<ArticulationBody>();
            GetAriculationBodyLists(this.gameObject);

            // 縮小座標データ開始インデックスリストを取得
            dof = rootLegArtBody.GetDofStartIndices(dofIndices);

            // ArticulationBodyのGet*用のリスト初期化
            rootLegArtBody.GetDriveTargets(driveTargetAngles);
            rootLegArtBody.GetDriveTargetVelocities(driveTargetAngularVelocities);

            SetXDriveStiffness(stiffness);
            SetXDriveDamping(damping);
            SetTargetAngles(initialAngle);
            
        }

        void Update()
        {
            /*
                foreach (var i in ariculationBodyLists.Select((Value, Index) => new { Value, Index }))
                {
                    Debug.Log($"index:{i.Index},value:{i.Value.GetInstanceID()}");
                }
            */

            /*
            foreach (var i in getDof.Select((Value, Index) => new { Value, Index }))
            {
                Debug.Log($"index:{i.Index},value:{i.Value}");
            }
            */
        }

        void FixedUpdate()
        {
            // SetTargetAngles(targetAngle);

            rootLegArtBody.SetDriveTargets(driveTargetAngles);
            rootLegArtBody.SetDriveTargetVelocities(driveTargetAngularVelocities);


        }

        public void PrintHello()
        {
            Debug.Log("Hello\n");
        }

        public int SigmaNumber
        {
            get
            {
                return (int)legNumber;
            }
        }

        public Length linkLength
        {
            get
            {
                return _linkLength;
            }
        }

        public int dof { get; private set; }

        public void SetTargetAngles(RotationAngle3 degree)
        {
            // GetDofStartIndicesにより得られたインデックス開始リストにしたがって目標角度リストをセットする
            driveTargetAngles[dofIndices[1]] = degree.theta1 * Mathf.Deg2Rad;
            driveTargetAngles[dofIndices[2]] = degree.theta2 * Mathf.Deg2Rad;
            driveTargetAngles[dofIndices[3]] = degree.theta3 * Mathf.Deg2Rad;
            rootLegArtBody.SetDriveTargets(driveTargetAngles);
            // ていうかさ、「immovable」にチェック入れる入れないでdof変わるのおかしいと思うんですけど（#^ω^）
        }

        public void SetTargetAngularVelocities(RotationAngle3 angularVelocities)
        {
            // GetDofStartIndicesにより得られたインデックス開始リストにしたがって目標角速度リストをセットする
            driveTargetAngularVelocities[dofIndices[1]] = angularVelocities.theta1 * Mathf.Deg2Rad;
            driveTargetAngularVelocities[dofIndices[2]] = angularVelocities.theta2 * Mathf.Deg2Rad;
            driveTargetAngularVelocities[dofIndices[3]] = angularVelocities.theta3 * Mathf.Deg2Rad;
        }

        public void SetXDriveStiffness(Stiffness value)
        {
            float[] stiffnesses = { value.l1, value.l2, value.l3 };
            for (int i = 0; i < 3; i++)
            {
                var hoge = ariculationBodyLists[i + 1].xDrive;
                hoge.stiffness = stiffnesses[i];
                ariculationBodyLists[i + 1].xDrive = hoge;
            }
        }
        
        public void SetXDriveDamping(Damping value)
        {
            float[] dampings = { value.l1, value.l2, value.l3 };
            for (int i = 0; i < 3; i++)
            {
                var hoge = ariculationBodyLists[i + 1].xDrive;
                hoge.damping = dampings[i];
                ariculationBodyLists[i + 1].xDrive = hoge;
            }
        }

        int recursiveCounter = 0;
        private void GetAriculationBodyLists(GameObject rootBody)
        {
            ArticulationBody articulationBody = rootBody.GetComponent<ArticulationBody>();
            ariculationBodyLists.Add(articulationBody);

            // ArticulationBody介して直接GetComponentしたのに自分のid返ってくるから遠回りするはめに
            // しかもgetChildメソッドは、子が存在しない場合は範囲指定外エラーするからtry文書かないと、、、null返してほしいわ
            GameObject childrenBody = null;
            try
            {
                childrenBody = rootBody.transform.GetChild(0).gameObject;
            }
            catch
            {
            }

            if (childrenBody != null)
            {
                recursiveCounter++;
                // 万が一の無限ループ防止
                if (recursiveCounter != 8)
                {
                    GetAriculationBodyLists(childrenBody);
                }
                else
                {
                    Debug.Log("error:infinite loop");
                }
            }
            else
            {
                recursiveCounter = 0;
            }

            return;
            // Debug.Log($"root:{rootBody.GetComponent<ArticulationBody>().GetInstanceID()},,id:{childrenBody.GetComponent<ArticulationBody>().GetInstanceID()}");
            // Debug.Log($"type:{childrenBody.GetType().FullName}");
            // rootBody.transform.GetChild(0);
        }

        public void GetArticulationBodyParameters()
        {

        }
    }

}