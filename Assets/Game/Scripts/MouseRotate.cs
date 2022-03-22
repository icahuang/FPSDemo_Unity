using UnityEngine;

// 代替LookX.cs和LookY.cs，在一个脚本里解决掉水平旋转和竖直旋转
public class MouseRotate : MonoBehaviour
{
    // 水平和竖直的灵敏度
    public float horiSen = 8.0f;
    public float vertSen = 8.0f;

    // 得到鼠标在x和y上的偏移量
    float mouseX;
    float mouseY;
    // 记录当前的旋转角度
    float horAngle;
    float verAngle;

    [SerializeField] private GameObject head;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // transform.localEulerAngles返回的角度是相对于父对象的旋转角度，用的是角度制。
        // 直接用transform.rotation返回的是相对于世界坐标的旋转角度，且不是角度制（Q. 返回的单位是？经测试返回值的区间在[-1,1]）
        horAngle = transform.localEulerAngles.y;
        verAngle = head.transform.localEulerAngles.x;
        Debug.Log("horAngle = " + horAngle);
        Debug.Log("verAngle = " + verAngle);
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

        // Debug.Log(Time.deltaTime);
    }
}
