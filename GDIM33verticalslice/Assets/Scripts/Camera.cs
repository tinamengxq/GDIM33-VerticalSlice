using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    public Vector3 following = new Vector3(0, 1.5f, 0);

    void Start()
    {
        transform.SetParent(_playerTransform);
        transform.localPosition = following;
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
    }
}
