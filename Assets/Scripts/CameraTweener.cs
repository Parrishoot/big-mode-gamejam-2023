    using UnityEngine;
    using System.Collections;
     
    
    public class CameraTweener
    {
        private Matrix4x4   ortho,
                            perspective,
                            dead;
        public float        fov     = 90f,
                            near    = .01f,
                            far     = 1000f,
                            orthographicSize = 20f;

        public float        dead_fov     = 175f;

        private float       aspect;

        private MatrixBlender matrixBlender;

        public CameraTweener(MatrixBlender matrixBlender) {
            this.matrixBlender = matrixBlender;
            aspect = ((float) Screen.width) / ((float) Screen.height);
            ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
            perspective = Matrix4x4.Perspective(fov, aspect, near, far);
            dead = Matrix4x4.Perspective(dead_fov, aspect, near, far);
        }
     
        public void TweenToPerspectiveMode(PerspectiveMode perspectiveMode, float transitionTime) {
            if(perspectiveMode == PerspectiveMode.TOP_DOWN) {
                matrixBlender.BlendToMatrix(ortho, transitionTime);
            }
            else {
                matrixBlender.BlendToMatrix(perspective, transitionTime);
            }
        }

        public void TweenToDeath(float transitionTime) {
            matrixBlender.BlendToMatrix(dead, transitionTime);
        }
    }
