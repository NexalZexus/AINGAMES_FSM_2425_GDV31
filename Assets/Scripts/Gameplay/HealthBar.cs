using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image hp;
    [SerializeField] private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    public void updateHealthBar(float max, float cur)
    {
        hp.fillAmount = cur/max;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
