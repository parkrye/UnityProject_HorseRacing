using UnityEngine;

[CreateAssetMenu (fileName ="CarData", menuName = "Data/CarData")]
public class CarData : ScriptableObject
{
    [SerializeField] float accelerate, weight, handling;
    public float Accelerate { get {  return accelerate; } }
    public float Weight { get { return weight; } }
    public float Handling { get { return handling; } }
}
