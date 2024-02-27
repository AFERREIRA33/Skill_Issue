using UnityEngine;

public class MoveTheCube : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector2 move;
    float delta;
    // Start is called before the first frame update
    void Start()
    {
        delta = Time.deltaTime;
        move = new Vector2(speed * delta, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            delta = Time.deltaTime;
            gameObject.transform.Translate(move);
            //gameObject.transform.Rotate(new Vector3(0, 0, speed));
            if (gameObject.transform.position.x > 10)
            {
                move.x = -speed * delta;
            } else if (gameObject.transform.position.x < -10)
            {
                move.x = speed * delta;
            }
        }
    }
}
