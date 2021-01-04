using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KinematicCharacterController
{

    public enum direction
    {
        forward, backward
    }

    public class MoveTest : MonoBehaviour, IMoverController
    {
        public PhysicsMover Mover;

        public Transform beginMove;
        public Transform endMove;


        float counter = 0;
        bool forward = true;

        public float timeStarted;
        public float slidingMaxTime = 3;

        Transform currentTarget;

        public direction dir;

        private void Start()
        {
            Mover.MoverController = this;
            currentTarget = endMove;
            StartLerp(direction.forward);
        }






        public void UpdateMovement(out Vector3 goalPosition, out Quaternion goalRotation, float deltaTime)
        {
            Vector3 tempoVec = Vector3.zero;
            counter += deltaTime;
            switch (dir)
            {
                case direction.forward:

                    currentTarget = endMove;

                    if (counter >= slidingMaxTime)
                    {
                        StartLerp(direction.backward);
                        counter = 0;
                    }

                    break;

                case direction.backward:

                    currentTarget = beginMove;

                    if (counter >= slidingMaxTime)
                    {
                        StartLerp(direction.forward);
                        counter = 0;
                    }
                    break;

            }
            


            //tempoVec = Vector3.Lerp(beginMove.position, endMove.position, num);
            tempoVec = ReLerp(transform.position, currentTarget.position, timeStarted, slidingMaxTime);
            


            goalPosition = tempoVec;

            goalRotation = Quaternion.identity;
        }


        private void StartLerp(direction dir)
        {
            this.dir = dir;
            timeStarted = Time.time;
        }

        public Vector3 ReLerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerptime = 1)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;

            float percentageComplete = timeSinceStarted / lerptime;

            return Vector3.Lerp(start, end, percentageComplete);

        }




    }


}

