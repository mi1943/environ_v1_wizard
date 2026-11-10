using UnityEngine;

// Gère la détection des potions et leur ordre dans la recette
public class CauldronPotionDetector : MonoBehaviour
{
    // Référence vers la lumière magique du chaudron
    public GameObject magicLight;

    // Lumière utilisée lorsqu'une mauvaise potion est ajoutée
    public GameObject errorLight;

    // Indique l'étape actuelle de la recette
    // 0 = attendre bleu
    // 1 = attendre rouge
    // 2 = attendre orange
    // 3 = recette terminée
    private int etapeRecette = 0;

    // Appelée automatiquement lorsqu'un objet entre dans
    // la zone Trigger du chaudron
    private void OnTriggerEnter(Collider other)
    {
        // Récupère le nom de l'objet détecté
        string nomPotion = other.gameObject.name;

        Debug.Log("Objet détecté : " + nomPotion);

        // Étape 1 : la première potion doit être bleue
        if (etapeRecette == 0 && nomPotion == "Potion_Blue")
        {
            etapeRecette = 1;

            // Feedback de réussite
            errorLight.SetActive(false);
            magicLight.SetActive(true);

            Debug.Log("Étape 1 réussie : potion BLEUE !");
            
            // Fait disparaître la potion après son ajout au chaudron
    other.gameObject.SetActive(false);
        }

        // Étape 2 : la deuxième potion doit être rouge
        else if (etapeRecette == 1 && nomPotion == "Potion_Red")
        {
            etapeRecette = 2;

            errorLight.SetActive(false);
            magicLight.SetActive(true);

            Debug.Log("Étape 2 réussie : potion ROUGE !");

            // Fait disparaître la potion
            other.gameObject.SetActive(false);
        }

        // Étape 3 : la troisième potion doit être orange
        else if (etapeRecette == 2 && nomPotion == "Potion_Orange")
        {
            etapeRecette = 3;

            errorLight.SetActive(false);
            magicLight.SetActive(true);

            // Rend la lumière plus forte pour signaler la réussite finale
            magicLight.GetComponent<Light>().intensity = 5f;

            
            Debug.Log("RECETTE TERMINÉE !");

            // Fait disparaître la dernière potion
            other.gameObject.SetActive(false);
        }

        // Une mauvaise potion a été utilisée
        else if (nomPotion == "Potion_Blue" ||
                 nomPotion == "Potion_Red" ||
                 nomPotion == "Potion_Orange")
        {

            // Réinitialise la recette
            etapeRecette = 0;

            // Désactive la lumière de réussite
            magicLight.SetActive(false);

            // Active la lumière rouge d'erreur
            errorLight.SetActive(true);

            Debug.Log("Mauvaise potion ! Recette réinitialisée.");

            // Attend 2 secondes avant d'éteindre la lumière rouge
            Invoke("EteindreLumiereErreur", 2f);
        }
    }
    // Éteint la lumière d'erreur après 2 secondes
    private void EteindreLumiereErreur()
    {
        errorLight.SetActive(false);
    }
}
