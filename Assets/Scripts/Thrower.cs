using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] AssignValues uiValues;
    [SerializeField] BallController ball;
    [SerializeField] ReadInput readInput;
    [SerializeField] Camera cam;
    int poolSize = 16;
    public int ballCount = 16;
    public int activeBallCount = 0;
    Vector3 target;
    BallController poolBall;
    //BallController ballSC;
    public GenericPool<BallController> pool;
    private void Start()
    {
        pool = new GenericPool<BallController>(CreateBall,TurnOffBall,poolSize);
    }
    private void Update()
    {
        //M2 ile pooldan top çektik
        if (Input.GetMouseButtonDown(1) && !readInput.IsCanvasActive)
        {
            if(activeBallCount <= 16)
            {
                poolBall= pool.Get();
                poolBall.transform.position = this.transform.position;
                poolBall.gameObject.SetActive(true);
                activeBallCount++; 
            }
            else
            {
                if (ballCount <= 24)
                {
                    Instantiate(ball, this.transform);
                    ballCount++;
                }
            }
        }
        //M1 ile düþeceði yeri seçtik.
        if (Input.GetMouseButtonDown(0) && !readInput.IsCanvasActive && uiValues.GetSpeed() != 0 && uiValues.GetHeight() != 0)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                target = hit.point;
            }
            ThrowTheBall(poolBall);
         }
    }
    void TurnOffBall(BallController ball)
    {
        ball.gameObject.SetActive(false);
    }
    BallController CreateBall()
    {
        return Instantiate(ball);
    }
    void ThrowTheBall(BallController ballSC)
    {
        if (ballSC.movementComplete)
        {
            ballSC.Throw(uiValues.GetSpeed(), uiValues.GetHeight(), target);
        }
    }
}
