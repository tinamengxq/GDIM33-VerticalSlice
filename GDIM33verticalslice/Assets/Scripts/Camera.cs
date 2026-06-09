using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    [SerializeField] private Transform _playerTransform;
    public Vector3 following = new Vector3(0, 1.5f, 0);
    [SerializeField] private Camera _camera;
    private UniversalAdditionalCameraData cameraRenderer;
    [SerializeField] private Transform water;
    [SerializeField] private GameObject waterSurface;
    void Start()
    {
        transform.SetParent(_playerTransform);
        transform.localPosition = following;
        cameraRenderer = _camera.GetUniversalAdditionalCameraData();
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * 150 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 150 * Time.deltaTime;
        if (mouseX > 45 && mouseX < -45)
        {
            mouseX %= 45;
        }
        if (mouseY > 90 && mouseY < -90)
        {
            mouseY %= 90;
        }

        _playerTransform.Rotate(0f, mouseX,0f, Space.Self);
        //_playerTransform.Rotate(-mouseY,0f,0f, Space.World);

        transform.localPosition = following;

        //oxygen
        if (water.position.y > transform.position.y)
        {
            ChangeCamera(1);
            Debug.Log("Active water effect");
            waterSurface.SetActive(false);
        }
        else
        {
            ChangeCamera(0);
            Debug.Log("Deactivate water effect");
            waterSurface.SetActive(true);
        }
    }

    public void ChangeCamera(int renderer)
    {
        cameraRenderer.SetRenderer(renderer);
        Debug.Log(renderer);
    }
}
