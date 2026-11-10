
using UnityEngine;
using UnityEngine.InputSystem;

public class MagicSymbolPuzzle : MonoBehaviour
{
    private int etape = 0;
    private bool puzzleTermine = false;

    // Références au coffre et aux effets visuels
    public MagicChestOpener magicChest;
    public MagicSymbolFeedback feedback;

    // Premier symbole : Lune
    public void SelectMoon()
    {
        if (puzzleTermine) return;

        if (etape == 0)
        {
            etape = 1;
            feedback?.CorrectMoon();
            Debug.Log("Étape 1/3 : Lune correcte !");
        }
        else
        {
            ResetPuzzle();
        }
    }

    // Deuxième symbole : Étoile
    public void SelectStar()
    {
        if (puzzleTermine) return;

        if (etape == 1)
        {
            etape = 2;
            feedback?.CorrectStar();
            Debug.Log("Étape 2/3 : Étoile correcte !");
        }
        else
        {
            ResetPuzzle();
        }
    }

    // Troisième symbole : Soleil
    public void SelectSun()
    {
        if (puzzleTermine) return;

        if (etape == 2)
        {
            etape = 3;
            puzzleTermine = true;

            feedback?.CorrectSun();

            Debug.Log("PUZZLE RÉUSSI !");
            Debug.Log("Le coffre magique est déverrouillé !");

            // Ouvre le coffre après la réussite
            if (magicChest != null)
            {
                magicChest.OpenChest();
            }
            else
            {
                Debug.LogWarning("MagicChest non assigné !");
            }
        }
        else
        {
            ResetPuzzle();
        }
    }

    // Réinitialise la combinaison en cas d'erreur
    private void ResetPuzzle()
    {
        etape = 0;
        feedback?.WrongSymbol();

        Debug.Log("Mauvais symbole ! Recommence depuis la lune.");
    }

    // Test temporaire sans casque VR
    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            SelectMoon();

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            SelectStar();

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            SelectSun();
    }
}
