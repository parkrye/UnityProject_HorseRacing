using UnityEngine;

/// <summary>
/// 원형으로 선을 그리는 스크립트
/// </summary>
public class CircleDrawer : MonoBehaviour
{
    LineRenderer circleRenderer;    // 라인 랜더러
    int steps;                      // 점 개수
    float radius;                   // 반지름
    Vector3 target;                 // 원이 그려질 위치

    void Awake()
    {
        circleRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// 설정 메소드. 이후 자동으로 원을 그림
    /// </summary>
    /// <param name="_target">원이 그려질 좌표</param>
    /// <param name="_stpes">몇 개의 점으로 구성된 원인지</param>
    /// <param name="_radius">원의 반지름</param>
    public void Setting(Vector3 _target, int _stpes, float _radius)
    {
        target = _target;
        steps = _stpes;
        radius = _radius;

        DrawCircle();
    }

    /// <summary>
    /// 원형을 그리는 메소드
    /// </summary>
    void DrawCircle()
    {
        float angle = 0f;                       // 각도

        circleRenderer.loop = true;             // 라인 랜더러의 처음과 끝을 이어준다

        circleRenderer.positionCount = steps;   // 점 개수 지정
        float addAngle = 360f / steps;          // 각 각도

        // 각 선분마다
        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            // 삼각함수로 angle의 x, y 위치를 지정
            float x = radius * Trigonometrics.Cos(angle);
            float y = radius * Trigonometrics.Sin(angle);

            // 점 위치 지정
            circleRenderer.SetPosition(currentStep, target + new Vector3(x, 0f, y));

            // 다음 각도를 저장
            angle += addAngle;
        }
    }
}
