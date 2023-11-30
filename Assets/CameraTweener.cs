    using UnityEngine;
    using System.Collections;
     
    
    public class CameraTweener
    {
        private Matrix4x4   ortho,
                            perspective;
        public float        fov     = 90f,
                            near    = .3f,
                            far     = 1000f,
                            orthographicSize = 15f;
        private float       aspect;

        private MatrixBlender matrixBlender;

        public CameraTweener(MatrixBlender matrixBlender) {
            this.matrixBlender = matrixBlender;
            aspect = ((float) Screen.width) / ((float) Screen.height);
            ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
            perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        }
     
        public void TweenToPerspectiveMode(PerspectiveMode perspectiveMode, float transitionTime) {
            if(perspectiveMode == PerspectiveMode.TOP_DOWN) {
                matrixBlender.BlendToMatrix(ortho, transitionTime);
            }
            else {
                matrixBlender.BlendToMatrix(perspective, transitionTime);
            }
        }
    }
