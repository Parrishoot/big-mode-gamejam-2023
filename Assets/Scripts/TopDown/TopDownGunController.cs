using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownGunController : PlayerGunController
{
    [SerializeField]
    private Vector2 mouseSensitivity = new Vector2(2f, 2F);

    private Vector3 startingPosition;

    void Awake() {
        startingPosition = transform.localPosition;
    }

    protected override void Update()
    {
        base.Update();

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity.x * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity.y * Time.deltaTime;

        transform.localPosition += new Vector3(inputX, inputY, 0);
    }

    public override void Reset() {
        base.Reset();
        transform.localPosition = startingPosition;
    }
}
