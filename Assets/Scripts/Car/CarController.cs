using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [SerializeField] CarDataModel carDataModel;
    [SerializeField] Rigidbody rb;

    [SerializeField] float fowardValue, sideValue;
    [SerializeField] bool onHandBrake, onUseItem, onChangeItem, onFlashLight;

    [SerializeField] float motorTorque, brakeTorque, steerAngleL, steerAngleR, downForce;
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

        StartCoroutine(URoutine());
        StartCoroutine(FURoutine());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    // 이하 연산 메소드
    IEnumerator URoutine()
    {
        while (true)
        {
            WheelPositioner();
            DriverCacluator();
            SteerCalculator();
            DownForceCalculator();
            yield return null;
        }
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

    void DriverCacluator()
    {
        if (motorTorque <= carDataModel.CarData.HighSpeed && motorTorque >= -carDataModel.CarData.HighSpeed)
            motorTorque += fowardValue * carDataModel.CarData.Accelerate * Time.deltaTime;
        else
            motorTorque = fowardValue * carDataModel.CarData.HighSpeed;

        if ((fowardValue < 0.5f && fowardValue > -0.5f) || (fowardValue < -0.5f && motorTorque > 0f) || (fowardValue > 0.5f && motorTorque < 0f))
            motorTorque = 0f;

        if (fowardValue == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                brakeTorque = fowardValue * carDataModel.CarData.Accelerate;
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                brakeTorque = 0;
            }
        }
    }

    void SteerCalculator()
    {
        if (sideValue > 0.5f)
        {
            steerAngleL = 28.191f * sideValue * carDataModel.CarData.Handling;
            steerAngleR = 34.215f * sideValue * carDataModel.CarData.Handling;
        }
        else if (sideValue < -0.5f)
        {
            steerAngleL = 34.215f * sideValue * carDataModel.CarData.Handling;
            steerAngleR = 28.191f * sideValue * carDataModel.CarData.Handling;
        }
        else
        {
            steerAngleL = 0;
            steerAngleR = 0;
        }
    }

    void DownForceCalculator()
    {
        downForce = rb.velocity.sqrMagnitude;
    }

    // 이하 물리 관여 메소드
    IEnumerator FURoutine()
    {
        while (true)
        {
            HandBrake();
            CarDriver();
            SteerVehicle();
            AddDownForce();
            yield return new WaitForFixedUpdate();
        }
    }

    void CarDriver()
    {
        for (int i = 0; i < 4; i++)
        {
            carDataModel.Wheels[i].motorTorque = motorTorque;
            carDataModel.Wheels[i].brakeTorque = brakeTorque;
        }
    }

    void SteerVehicle()
    {
        carDataModel.Wheels[0].steerAngle = steerAngleL;
        carDataModel.Wheels[1].steerAngle = steerAngleR;
    }

    void AddDownForce()
    {
        rb.AddForce(-transform.up * downForce);
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
                carDataModel.WheelFrictionCurve[i].stiffness = 0.4f;
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
