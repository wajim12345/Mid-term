using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretController : MonoBehaviour
{
    private TurretState currentState;
    public Transform turretEye;
    public float checkPlayerDistance;
    public float checkRadius = 0.4f;
    private LineRenderer laser;
    public Transform laserOrigin;
    
    

    public Transform player;
    private void Awake()
    {
        laser = GetComponent<LineRenderer>();
        ResetLaserLength();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = new TurretIdleState(this);
        currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate();
    }
    public void ChangeState(TurretState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }
    public void LaserColor(Color c1)
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0f), new GradientColorKey(c1, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f,1.0f), new GradientAlphaKey(1.0f, 1.0f) });
        laser.colorGradient = gradient;
    }

    public void SetLaserLength(Vector3 point)
    {

        laser.SetPosition(1, point);

    }

    public void ResetLaserLength()
    {
        laser.SetPosition(0, laserOrigin.position);
        laser.SetPosition(1, new Vector3(laserOrigin.position.x, laserOrigin.position.y, laserOrigin.position.z + checkPlayerDistance));
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(turretEye.position, checkRadius);
        Gizmos.DrawWireSphere(turretEye.position + turretEye.forward * checkPlayerDistance, checkRadius);
        Gizmos.DrawLine(turretEye.position, turretEye.position + turretEye.forward * checkPlayerDistance);
    }*/
}
