using UnityEngine;

public class CarDataModel : MonoBehaviour
{
    [SerializeField] Transform[] tires;
    public Transform[] Tires {  get { return tires; } }

    [SerializeField] WheelCollider[] wheels;
    public WheelCollider[] Wheels { get {  return wheels; } }

    [SerializeField] GameObject flashLight;
    public GameObject FlashLight { get {  return flashLight; } }

    [SerializeField] CarData carData;
    public CarData CarData { get { return carData; } }

    [SerializeField] WheelFrictionCurve[] wheelFrictionCurve;
    public WheelFrictionCurve[] WheelFrictionCurve { get { return wheelFrictionCurve; } }

    public void Initialize()
    {
        tires = new Transform[4];
        wheels = new WheelCollider[4];
        wheelFrictionCurve = new WheelFrictionCurve[4];

        GameManager.Resource.Instantiate<GameObject>("Component/Wheels", transform);
        flashLight = GameManager.Resource.Instantiate<GameObject>("Component/FlashLights", transform.position, Quaternion.identity, transform);
        carData = GameManager.Resource.Load<CarData>("CarData/CarData");

        Transform[] _transforms = GetComponentsInChildren<Transform>();
        for(int i = 0; i < _transforms.Length; i++)
        {
            switch (_transforms[i].name)
            {
                case "Tire_FL":
                    tires[0] = _transforms[i];
                    break;
                case "Tire_FR":
                    tires[1] = _transforms[i];
                    break;
                case "Tire_RL":
                    tires[2] = _transforms[i];
                    break;
                case "Tire_RR":
                    tires[3] = _transforms[i];
                    break;
                case "Wheel_FL":
                    wheels[0] = _transforms[i].GetComponent<WheelCollider>();
                    break;
                case "Wheel_FR":
                    wheels[1] = _transforms[i].GetComponent<WheelCollider>();
                    break;
                case "Wheel_RL":
                    wheels[2] = _transforms[i].GetComponent<WheelCollider>();
                    break;
                case "Wheel_RR":
                    wheels[3] = _transforms[i].GetComponent<WheelCollider>();
                    break;
                default:
                    break;
            }
        }

        flashLight.transform.Translate(transform.forward * 1.5f, Space.Self);

        for (int i = 0; i < 4; i++)
            wheels[i].transform.position = tires[i].transform.position;

        wheelFrictionCurve[0] = wheels[2].forwardFriction;
        wheelFrictionCurve[1] = wheels[2].sidewaysFriction;
        wheelFrictionCurve[2] = wheels[3].forwardFriction;
        wheelFrictionCurve[3] = wheels[3].sidewaysFriction;
    }
}
