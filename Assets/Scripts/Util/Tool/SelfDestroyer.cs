using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    [SerializeField] float time;

    void OnEnable()
    {
        GameManager.Resource.Destroy(gameObject, time);
    }
}
