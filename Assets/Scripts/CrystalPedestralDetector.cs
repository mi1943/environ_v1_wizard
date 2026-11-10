
using UnityEngine;

// Détecte le cristal et déclenche les effets de victoire
public class CrystalPedestalDetector : MonoBehaviour
{
    // Cristal à déposer sur le socle
    public GameObject magicCrystal;

    // Lumière activée après la réussite
    public GameObject victoryLight;

    // Son joué après la réussite
    public AudioSource victorySound;

    // Empêche plusieurs déclenchements
    private bool missionTerminee = false;

    private void OnTriggerEnter(Collider other)
    {
        // Ignore les détections après la victoire
        if (missionTerminee)
            return;

        // Vérifie que le cristal est entré dans la zone
        if (other.gameObject == magicCrystal)
        {
            missionTerminee = true;

            // Allume la lumière magique
            if (victoryLight != null)
            {
                victoryLight.SetActive(true);
            }

            // Joue le son une seule fois
            if (victorySound != null)
            {
                victorySound.Play();
            }

            Debug.Log("Cristal magique détecté !");
            Debug.Log("MISSION ACCOMPLIE !");
        }
    }
}



