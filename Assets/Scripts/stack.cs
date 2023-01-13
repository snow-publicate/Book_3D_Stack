using UnityEngine;

public class stack : MonoBehaviour
{
    private float clickTime; //마우스 왼쪽 클릭(터치) 중인 시간
    public float minClickTime = 0.6f; //마우스 왼쪽 클릭(터치) 최소 시간
    private bool isClick; //마우스 왼쪽 클릭(터치) 중인지 여부 판단 

    void Update()
    {
        //마우스 왼쪽 클릭(터치) 여부에 따라 isClick 변수값 변환
        if (Input.GetMouseButtonDown(0))
            isClick = true;
        else if (Input.GetMouseButtonUp(0))
            isClick = false;

        //마우스 왼쪽 클릭(터치) 중이라면
        if (isClick)
        {
            //마우스 왼쪽 클릭(터치)시간 측정
            clickTime += Time.deltaTime;
        }
        //마우스 왼쪽 클릭(터치) 중이 아니라면
        else
        {
            //마우스 왼쪽 클릭(터치)시간 초기화
            clickTime = 0;
        }
    }

    void OnMouseOver() //마우스 커서를 올려둘 때 반응하는 함수
    {
        float x = gameObject.transform.position.x; 
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;
        Quaternion q = Quaternion.identity;

        //마우스 오른쪽 클릭을 할 때 + 객체의 높이가 0 초과일 때만 작동 + 객체의 명칭에 (Clone)이 더 붙은 객체가 없을 때 작동
        if ((Input.GetMouseButtonDown(1) || clickTime > minClickTime) && y > 0f && !GameObject.Find(gameObject.name+"(Clone)"))
        {
            Destroy(gameObject);
        }
        //마우스 왼쪽 클릭(또는 터치)을 할 때 + 객체의 높이가 4 미만일 때 + 객체의 명칭에 (Clone)이 더 붙은 객체가 없을 때 작동
        else if (Input.GetMouseButtonUp(0) && y < 4f && !GameObject.Find(gameObject.name+"(Clone)"))
        {
            //마우스 클릭(또는 터치)을 한 객체의 clone을 y축 방향으로 1칸 높은 곳에 생성
            //나머지 속성은 모두 원본의 것을 그대로 유지
            GameObject clone = Instantiate(gameObject, new Vector3(x, y + 1f, z), q);
            clone.tag = "clone"; //생성된 clone객체에 "clone" 태그 등록
        }
    }
}

