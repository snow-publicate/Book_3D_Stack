using UnityEngine;

public class camera_control_m : MonoBehaviour
{
    public GameObject workingCamera; //움직일 카메라객체 변수 선언 - 유니티 에디터 접근 허용
    public Transform target; //주목할 기준 객체 변수 선언 - 유니티 에디터 접근 허용
    public static float speedMod = 10.0f; //카메라 이동속도 변수 선언(축 별로 수식을 통한 속도 조정) - 유니티 에디터 접근 허용
    
    private static Vector3 point; //주목할 기준 객체의 위치 변수 선언
    private Vector3 OriginalPosition; //카메라 최초 위치 저장 변수 선언
    private Quaternion OriginalRotation; //카메라 최초 각도 저장 변수 선언
    private float OriginalFOV; //카메라 최초 시각범위 저장 변수 선언

    private bool onLeftButton = false; //카메라 왼쪽 회전 버튼 작동 여부 확인용 변수 선언(초기값은 false 할당)
    private bool onRightButton = false; //카메라 오른쪽 회전 버튼 작동 여부 확인용 변수 선언(초기값은 false 할당)
    private bool onDownButton = false; //카메라 아래쪽 회전 버튼 작동 여부 확인용 변수 선언(초기값은 false 할당)
    private bool onUpButton = false; //카메라 위쪽 회전 버튼 작동 여부 확인용 변수 선언(초기값은 false 할당)
    private bool onForwardButton = false; //카메라 전진 버튼 작동 여부 확인용 변수 선언(초기값은 false 할당)
    private bool onBackwardButton = false; //카메라 후진 버튼 작동 여부 확인용 변수 선언(초기값은 false 할당)

    void Start()
    {
        OriginalPosition = transform.position; //카메라 최초 위치 저장
        OriginalRotation = transform.rotation; //카메라 최초 각도 저장
        OriginalFOV = GetComponent<Camera>().fieldOfView; //카메라 최초 시각범위 저장
    }

    void Update()
    {
        //주목할 기준 객체의 위치 저장
        point = new Vector3(target.transform.position.x,target.transform.position.y+3f,target.transform.position.z);
        transform.LookAt(point); //카메라가 바라볼 대상 적용

        //각 버튼별 작동 여부가 true이면 하단의 함수를 실행하는 기능 모음
        if(onLeftButton) 
        {
            TouchLeft();
        }
        if(onRightButton)
        {
            TouchRight();
        }
        if(onDownButton)
        {
            TouchDown();
        }
        if(onUpButton)
        {
            TouchUp();
        }
        if(onForwardButton)
        {
            TouchForward();
        }
        if(onBackwardButton)
        {
            TouchBackward();
        }                                
    }

    //각 버튼별 작동 여부 true/false 전환 기능 모음
    public void TouchLeft_PointerDown()
    {
        onLeftButton = true;
    }
    public void TouchLeft_PointerUp()
    {
        onLeftButton = false;
    }

    public void TouchRight_PointerDown()
    {
        onRightButton = true;
    }
    public void TouchRight_PointerUp()
    {
        onRightButton = false;
    }

    public void TouchDown_PointerDown()
    {
        onDownButton = true;
    }
    public void TouchDown_PointerUp()
    {
        onDownButton = false;
    }

    public void TouchUp_PointerDown()
    {
        onUpButton = true;
    }
    public void TouchUp_PointerUp()
    {
        onUpButton = false;
    }

    public void TouchForward_PointerDown()
    {
        onForwardButton = true;
    }
    public void TouchForward_PointerUp()
    {
        onForwardButton = false;
    }

    public void TouchBackward_PointerDown()
    {
        onBackwardButton = true;
    }
    public void TouchBackward_PointerUp()
    {
        onBackwardButton = false;
    }

    //카메라의 위치, 각도를 최초 상태로 복구
    public void TurnBack() 
    {
        GetComponent<Camera>().fieldOfView = OriginalFOV;
        transform.position = OriginalPosition;
        transform.rotation = OriginalRotation;
    }

    //그 외 카메라 위치, 각도 조정 함수 모음
    private void TouchLeft() //누르면 카메라를 대상 기준 시계방향으로 회전
    {
        transform.RotateAround(point, Vector3.down, Time.deltaTime * speedMod * 20f);
    }

    private void TouchRight() //누르면 카메라를 대상 기준 반시계방향으로 회전
    {
        transform.RotateAround(point, Vector3.up, Time.deltaTime * speedMod * 20f);
    }

    private void TouchDown() //누르면 카메라를 대상 기준 하단방향으로 회전
    {
        if (GetComponent<Camera>().fieldOfView < 85.0f)
        {
            if (transform.position.y > 2.4f)
            {
                transform.Translate(Vector3.down * Time.deltaTime * speedMod * 2f);
            }
        }
    }    

    private void TouchUp() //누르면 카메라를 대상 기준 상단방향으로 회전
    {
        if (GetComponent<Camera>().fieldOfView > 45.0f)
        {
            if (transform.position.y < 8f)
            {
                transform.Translate(Vector3.up * Time.deltaTime * speedMod * 2f);
            }
        }
    }

    private void TouchForward() //누르면 카메라를 대상 방향으로 전진
    {
        if (GetComponent<Camera>().fieldOfView > 60.0f)
        {
            GetComponent<Camera>().fieldOfView -= speedMod / 20;
        }
    }        

    private void TouchBackward() //누르면 카메라를 대상 방향에서 후진
    {
        if (GetComponent<Camera>().fieldOfView < 75.0f)
        {
            GetComponent<Camera>().fieldOfView += speedMod / 20;
        }
    }       
}

