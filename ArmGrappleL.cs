using UnityEngine;

public class ArmGrappleL : MonoBehaviour
{
    [SerializeField] private ArmTracking ArmTracking;

    void Update()
    {
        // Envoi du bras gauche en grappin
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ArmTracking.GrappleLArm();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ArmTracking.StopGrappleLArm();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ancrer le bras sur le point de collision
        // lorsque le joueur veut faire grappin avec celui-ci
        if (collision.CompareTag("Grabbable") && ArmTracking.LArmGrapple)
        {
            ArmTracking.AnchorLArm();
            print("Left Armmmmmm");
        }
    }
}
