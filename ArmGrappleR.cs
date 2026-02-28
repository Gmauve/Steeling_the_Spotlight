using UnityEngine;

public class ArmGrappleR : MonoBehaviour
{
    [SerializeField] private ArmTracking ArmTracking;

    void Update()
    {
        // Envoi du bras droit en grappin
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ArmTracking.GrappleRArm();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ArmTracking.StopGrappleRArm();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ancrer le bras sur le point de collision
        // lorsque le joueur veut faire grappin avec celui-ci
        if (collision.CompareTag("Grabbable") && ArmTracking.RArmGrapple)
        {
            ArmTracking.AnchorRArm();
            print("Right Armmmmmm");
        }
    }
}
