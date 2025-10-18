using UnityEngine;

public class ShotDelete : MonoBehaviour
{
    public float lifespan = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
