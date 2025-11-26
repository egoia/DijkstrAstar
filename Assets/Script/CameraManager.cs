using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraManager : MonoBehaviour
{

    public GameObject player;
    public float zoomTime;
    public float desiredSize;
    public float dampingSpeed;

    bool isFollowing=false;
    

    void Update()
    {
        if(isFollowing && player!=null)transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x,player.transform.position.y, -10), dampingSpeed);
    }

        public IEnumerator StartGameCoroutine()
    {
        Camera camComponent = GetComponent<Camera>();
        float startingSize = camComponent.orthographicSize;
        Vector3 startingPos = transform.position;
        float t = Time.time;
        while (t <= zoomTime)
        {   
            transform.position = Vector3.Lerp(startingPos, new Vector3(player.transform.position.x,player.transform.position.y, -10),t/zoomTime);
            camComponent.orthographicSize = Mathf.Lerp(startingSize, desiredSize, t/zoomTime);
            t+= Time.deltaTime;
            yield return null;
        }
        isFollowing = true;
    }
    
}
