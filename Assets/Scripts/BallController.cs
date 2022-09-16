using UnityEngine;

public class BallController : MonoBehaviour
{
    bool functionCalled = false;
    public bool movementComplete = true;

    //Topun parametreleri
    //t_c = u�u� s�resi -> t = toplam hareket s�resi -> Vx,Vy,Vz = x-y-z eksenlerindeki h�zlar� -> gravity = yer �ekimi -> teta = f�rlatma a��m�z
    float t_c, teta, Vx, Vy, Vz;
    float gravity = 9.81f;
    float t = 0;
    private void FixedUpdate()
    {
        //Fonksiyonumuzun s�rekli olarak �a��r�lmas�.
        if (functionCalled)
        {
            MoveBall();
            
            //top yere �arpt� m�?
            if(transform.position.y <= 1)
            {
                //Threshold
                if (-1 <= Vy && Vy <= 1)
                {
                    functionCalled = false;
                    movementComplete = true;
                    transform.position = Vector3.up;
                    Reset();
                }
                else
                {
                    Vy += 2;
                    Vy = Mathf.Abs(Vy);
                }
            }
        }
    }
    public void Throw(float speed, float hMax, Vector3 target)
    {
        //F�rlatma a��s�n� bulduk. 
        float a = Mathf.Sqrt(2 * gravity * hMax);
        a = a / speed;
        teta = Mathf.Asin(a);

        Vy = speed * Mathf.Sin(teta);

        //t_c=Max y�ksekli�e ula�ma s�resi
        t_c = Mathf.Sqrt(Mathf.Abs(Vy) / gravity);

        //t = toplam hareket s�resi
        t = 2 * t_c;

        //H�z = yer de�i�tirme / zaman
        //X'teki h�z
        Vx = target.x / t;

        //Z'deki h�z
        Vz = target.z / t;

        functionCalled = true;
    }

    void MoveBall()
    {
        transform.position += Vector3.up * (Vy -= gravity * Time.deltaTime)  * Time.deltaTime;
        transform.position += Vector3.right * Vx * Time.deltaTime;
        transform.position += Vector3.forward * Vz * Time.deltaTime;

        movementComplete = false;
    }

    private void Reset()
    {
        Thrower th = FindObjectOfType<Thrower>();
        th.activeBallCount--;
        transform.position = Vector3.zero;
        th.pool.Return(this);
    }
}