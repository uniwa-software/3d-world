using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Σύρε εδώ από το Inspector το αντικείμενο-στόχο (π.χ. το κάστρο)
    public Transform target;

    public float rotationSpeed = 10f;

    void Update()
    {
        // Έλεγχος για να βεβαιωθούμε ότι έχουμε ορίσει έναν στόχο
        if (target != null)
        {
            // Περιστρέφει την κάμερα ΓΥΡΩ από τη θέση του στόχου
            // transform.RotateAround(point, axis, angle);
            transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.unscaledDeltaTime);

            // (Προαιρετικό αλλά προτείνεται)
            // Αυτή η γραμμή σιγουρεύει ότι η κάμερα πάντα "κοιτάει" κατευθείαν τον στόχο,
            // διορθώνοντας τυχόν μικρές αποκλίσεις.
            transform.LookAt(target);
        }
        else
        {
            // Αν ξεχάσουμε να βάλουμε στόχο, απλά περιστρέφεται γύρω από τον εαυτό της
            Debug.LogWarning("Δεν έχει οριστεί target για την κάμερα του μενού! Η κάμερα περιστρέφεται γύρω από τον εαυτό της.");
            transform.Rotate(Vector3.up, rotationSpeed * Time.unscaledDeltaTime);
        }
    }
}