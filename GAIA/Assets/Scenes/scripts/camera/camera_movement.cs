using UnityEngine;
 
public class camera_movement : MonoBehaviour
{

    // the target the camera should follow (usually the player)
    private GameObject[] targets;

    private Transform target;
    // the camera distance (z position)
    public float distance = -10f;

    private int hosts = 0;
    // the height the camera should be above the target (AKA player)
    public float height = 0f;

    // damping is the amount of time the camera should take to go to the target
    public float damping = 5.0f;
    
    void Update()
    {

        // get the position of the target (AKA player)
        if (target != null)
        {
            Vector3 wantedPosition = target.TransformPoint(0, height, distance);
            // set the camera to go to the wanted position in a certain amount of time
            transform.position = Vector3.Lerp(transform.position, wantedPosition, (Time.deltaTime * damping));
        }else findnewhost();

      
    }

    void findnewhost()
    {
        targets  = GameObject.FindGameObjectsWithTag("Otter");
        if (targets != null)
        {
            target = targets[hosts].transform;
            hosts++;
        }

    }
}
