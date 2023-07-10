using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] CarDataModel carDataModel;
    [SerializeField] Rigidbody rb;

    [SerializeField] float fowardValue, sideValue;
    [SerializeField] bool onHandBrake, onUseItem, onChangeItem, onFlashLight;

    [SerializeField] float downForce, brakeStiffness;

    [SerializeField] Vector3[] wheelPositions;
    [SerializeField] Quaternion[] wheelRotations;

    private void Awake()
    {
        carDataModel.Initialize();
        Initialize();
    }

    public void Initialize()
    {
        carDataModel = GetComponentInChildren<CarDataModel>();
        rb = GetComponent<Rigidbody>();

        wheelPositions = new Vector3[4];
        wheelRotations = new Quaternion[4];

        onFlashLight = true;
    }

    void FixedUpdate()
    {
        WheelPositioner();
        HandBrake();
        CarDriver();
        SteerVehicle();
        AddDownForce();
    }

    void WheelPositioner()
    {
        for (int i = 0; i < 4; i++)
        {
            carDataModel.Wheels[i].GetWorldPose(out wheelPositions[i], out wheelRotations[i]);
            carDataModel.Tires[i].transform.position = wheelPositions[i];
            carDataModel.Tires[i].transform.rotation = wheelRotations[i];
        }
    }

    void CarDriver()
    {
        for (int i = 0; i < 4; i++)
        {
            carDataModel.Wheels[i].motorTorque = fowardValue * carDataModel.CarData.Accelerate;
            if(fowardValue == 0f)
                carDataModel.Wheels[i].brakeTorque = carDataModel.CarData.Accelerate;
            else
                carDataModel.Wheels[i].brakeTorque = 0f;
        }
    }

    void SteerVehicle()
    {
        if (sideValue > 0.5f)
        {
            carDataModel.Wheels[0].steerAngle = 20.695f * sideValue * carDataModel.CarData.Handling;
            carDataModel.Wheels[1].steerAngle = 25.906f * sideValue * carDataModel.CarData.Handling;
        }
        else if (sideValue < -0.5f)
        {
            carDataModel.Wheels[0].steerAngle = 25.906f * sideValue * carDataModel.CarData.Handling;
            carDataModel.Wheels[1].steerAngle = 20.695f * sideValue * carDataModel.CarData.Handling;
        }
        else
        {
            carDataModel.Wheels[0].steerAngle = 0;
            carDataModel.Wheels[1].steerAngle = 0;
        }
    }

    void AddDownForce()
    {
        rb.AddForce(-transform.up * rb.velocity.magnitude * downForce);
    }

    void HandBrake()
    {
        if (onHandBrake)
        {
            carDataModel.Wheels[2].forwardFriction = carDataModel.WheelFrictionCurve[0];
            carDataModel.Wheels[2].sidewaysFriction = carDataModel.WheelFrictionCurve[1];
            carDataModel.Wheels[3].forwardFriction = carDataModel.WheelFrictionCurve[2];
            carDataModel.Wheels[3].sidewaysFriction = carDataModel.WheelFrictionCurve[3];
        }
    }

    // 이하 입력 메소드
    void OnMove(InputValue inputValue)
    {
        fowardValue = inputValue.Get<Vector2>().y;
        sideValue = inputValue.Get<Vector2>().x;
    }

    void OnShift(InputValue inputValue)
    {
        onHandBrake = inputValue.isPressed;

        if (onHandBrake)
        {
            for (int i = 0; i < 4; i++)
                carDataModel.WheelFrictionCurve[i].stiffness = brakeStiffness;
        }
        else
        {
            for (int i = 0; i < 4; i++)
                carDataModel.WheelFrictionCurve[i].stiffness = 1f;
        }
    }

    void OnControl(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            onUseItem = inputValue.isPressed;
        }
    }

    void OnAlternative(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            onChangeItem = true;
        }
    }

    void OnFlash(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            onFlashLight = !onFlashLight;
            carDataModel.FlashLight.SetActive(onFlashLight);
        }
    }
}
