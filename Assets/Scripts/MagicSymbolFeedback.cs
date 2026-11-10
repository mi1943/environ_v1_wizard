
using UnityEngine;
using System.Collections;

public class MagicSymbolFeedback : MonoBehaviour
{
    // Références aux trois symboles du livre magique
    public Renderer moonRenderer;
    public Renderer starRenderer;
    public Renderer sunRenderer;

    // Couleurs utilisées pour indiquer une bonne ou mauvaise réponse
    public Color correctColor = Color.green;
    public Color errorColor = Color.red;

    // Matériaux permettant de modifier la couleur des symboles
    private Material moonMaterial;
    private Material starMaterial;
    private Material sunMaterial;

    // Couleurs initiales à restaurer après une erreur
    private Color moonOriginal;
    private Color starOriginal;
    private Color sunOriginal;

    private void Start()
    {
        // Récupère les matériaux des trois symboles
        moonMaterial = moonRenderer.material;
        starMaterial = starRenderer.material;
        sunMaterial = sunRenderer.material;

        // Enregistre leurs couleurs d'origine
        moonOriginal = moonMaterial.color;
        starOriginal = starMaterial.color;
        sunOriginal = sunMaterial.color;
    }

    // Affiche le symbole de la lune en vert
    public void CorrectMoon()
    {
        moonMaterial.color = correctColor;
    }

    // Affiche le symbole de l'étoile en vert
    public void CorrectStar()
    {
        starMaterial.color = correctColor;
    }

    // Affiche le symbole du soleil en vert
    public void CorrectSun()
    {
        sunMaterial.color = correctColor;
    }

    // Déclenche un retour visuel lorsqu'un mauvais symbole est choisi
    public void WrongSymbol()
    {
        // Arrête un éventuel retour visuel précédent
        StopAllCoroutines();

        // Lance l'affichage temporaire de la couleur rouge
        StartCoroutine(ShowError());
    }

    private IEnumerator ShowError()
    {
        // Affiche tous les symboles en rouge
        moonMaterial.color = errorColor;
        starMaterial.color = errorColor;
        sunMaterial.color = errorColor;

        // Attend une seconde avant de rétablir les couleurs
        yield return new WaitForSeconds(1f);

        // Restaure les couleurs initiales des symboles
        moonMaterial.color = moonOriginal;
        starMaterial.color = starOriginal;
        sunMaterial.color = sunOriginal;
    }
}
