
using UnityEngine;

public class MagicChestOpener : MonoBehaviour
{
    // Point de rotation du couvercle
    public Transform lidPivot;

    // Angle et vitesse d'ouverture
    public float openingAngle = -100f;
    public float openingSpeed = 90f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpening = false;

    private void Start()
    {
        // Enregistre la rotation fermée
        closedRotation = lidPivot.localRotation;

        // Calcule la rotation ouverte
        openRotation = closedRotation *
            Quaternion.Euler(openingAngle, 0f, 0f);
    }

    private void Update()
    {
        if (isOpening)
        {
            // Ouvre progressivement le couvercle
            lidPivot.localRotation = Quaternion.RotateTowards(
                lidPivot.localRotation,
                openRotation,
                openingSpeed * Time.deltaTime
            );
        }
    }

    // Appelée après la réussite du puzzle
    public void OpenChest()
    {
        isOpening = true;
        Debug.Log("Le coffre magique s'ouvre !");

        // Révèle le cristal magique
        if (magicCrystal != null)
        {
            magicCrystal.SetActive(true);
        }
       
        Debug.Log("Le coffre magique s'ouvre !");
    }

    // Récompense révélée après l'ouverture
    public GameObject magicCrystal;
}

