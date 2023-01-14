using UnityEngine;

public class camera_control : MonoBehaviour
{
    public GameObject workingCamera; //움직일 카메라객체 변수 선언 - 유니티 에디터 접근 허용
    public Transform target; //주목할 기준 객체 변수 선언 - 유니티 에디터 접근 허용
    public static float speedMod = 10.0f; //카메라 이동속도 변수 선언(축 별로 수식을 통한 속도 조정) - 유니티 에디터 접근 허용
    
    private static Vector3 point; //주목할 기준 객체의 위치 변수 선언
    private Vector3 OriginalPosition; //카메라 최초 위치 저장 변수 선언
    private Quaternion OriginalRotation; //카메라 최초 각도 저장 변수 선언
    private float OriginalFOV; //카메라 최초 시각범위 저장 변수 선언

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

        //키보드 A키나 왼쪽 화살표키를 누르면 카메라를 대상 기준 시계방향으로 회전
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.RotateAround(point, Vector3.down, Time.deltaTime * speedMod * 20f);
        }

        //키보드 D키나 오른쪽 화살표키를 누르면 카메라를 대상 기준 반시계방향으로 회전
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(point, Vector3.up, Time.deltaTime * speedMod * 20f);
        }

        //키보드 S키나 아래쪽 화살표키를 누르면 카메라를 대상 기준 하단방향으로 회전
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && GetComponent<Camera>().fieldOfView < 85.0f)
        {
            if (transform.position.y > 2.4f)
            {
                transform.Translate(Vector3.down * Time.deltaTime * speedMod * 2f);
            }
        }

        //키보드 W키나 위쪽 화살표키를 누르면 카메라를 대상 기준 상단방향으로 회전
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && GetComponent<Camera>().fieldOfView > 45.0f)
        {
            if (transform.position.y < 8f)
            {
                transform.Translate(Vector3.up * Time.deltaTime * speedMod * 2f);
            }
        }

        //키보드 Q키나 콤마키를 누르면 카메라를 대상 방향으로 전진
        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Comma))&& GetComponent<Camera>().fieldOfView > 60.0f)
        {
            GetComponent<Camera>().fieldOfView -= speedMod / 20;
        }

        //키보드 E키나 마침표키를 누르면 카메라를 대상 방향에서 후진
        if ((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Period)) && GetComponent<Camera>().fieldOfView < 75.0f)
        {
            GetComponent<Camera>().fieldOfView += speedMod / 20;
        }

        //키보드 B키를 누르면 카메라를 최초 상태로 복구
        if (Input.GetKey(KeyCode.B))
        {
            GetComponent<Camera>().fieldOfView = OriginalFOV;
            transform.position = OriginalPosition;
            transform.rotation = OriginalRotation;
        }
    }
}

