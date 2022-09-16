using UnityEngine;

public class BallController : MonoBehaviour
{
    bool functionCalled = false;
    public bool movementComplete = true;

    //Topun parametreleri
    //t_c = uçuþ süresi -> t = toplam hareket süresi -> Vx,Vy,Vz = x-y-z eksenlerindeki hýzlarý -> gravity = yer çekimi -> teta = fýrlatma açýmýz
    float t_c, teta, Vx, Vy, Vz;
    float gravity = 9.81f;
    float t = 0;
    private void FixedUpdate()
    {
        //Fonksiyonumuzun sürekli olarak çaðýrýlmasý.
        if (functionCalled)
        {
            MoveBall();
            
            //top yere çarptý mý?
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
        //Fýrlatma açýsýný bulduk. 
        float a = Mathf.Sqrt(2 * gravity * hMax);
        a = a / speed;
        teta = Mathf.Asin(a);

        Vy = speed * Mathf.Sin(teta);

        //t_c=Max yüksekliðe ulaþma süresi
        t_c = Mathf.Sqrt(Mathf.Abs(Vy) / gravity);

        //t = toplam hareket süresi
        t = 2 * t_c;

        //Hýz = yer deðiþtirme / zaman
        //X'teki hýz
        Vx = target.x / t;

        //Z'deki hýz
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