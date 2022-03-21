using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    // 水平和竖直的灵敏度
    public float horiSen = 8.0f;
    public float vertSen = 8.0f;


    float mouseX;
    float mouseY;
    float horAngle;
    float verAngle;

    [SerializeField] private GameObject head;

    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        horAngle += mouseX * horiSen * Time.deltaTime * 10;

        //verAngle -= mouseY * vertSen * Time.deltaTime * 10;
        // 将垂直视角锁死在 -90f~90f
        verAngle = Mathf.Clamp(verAngle - mouseY * vertSen * Time.deltaTime * 10, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(0, horAngle, 0);
        head.transform.localRotation = Quaternion.Euler(verAngle, 0, 0);
    }
}
