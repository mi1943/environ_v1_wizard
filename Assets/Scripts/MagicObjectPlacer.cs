
using UnityEngine;

public class MagicObjectPlacer : MonoBehaviour
{
    [System.Serializable]
    public class Replacement
    {
        public GameObject oldObject;
        public GameObject newModel;
    }

    public Replacement[] objectsToReplace;

    [ContextMenu("Replace All Visuals")]
    public void ReplaceAll()
    {
        if (Application.isPlaying) return;

        foreach (var item in objectsToReplace)
        {
            if (item.oldObject == null || item.newModel == null)
                continue;

            Transform target = item.oldObject.transform;

            if (target.Find("NewVisual") != null)
                continue;

            Renderer oldRenderer =
                item.oldObject.GetComponent<Renderer>();

            if (oldRenderer == null) continue;

            Bounds oldBounds = oldRenderer.bounds;

            GameObject model = Instantiate(item.newModel);
            model.name = "NewVisual";
            model.transform.SetParent(target, true);

            model.transform.position = oldBounds.center;
            model.transform.rotation = target.rotation;

            Renderer[] renderers =
                model.GetComponentsInChildren<Renderer>();

            if (renderers.Length == 0)
            {
                DestroyImmediate(model);
                continue;
            }

            Bounds modelBounds = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
                modelBounds.Encapsulate(renderers[i].bounds);

            Vector3 size = modelBounds.size;

            float scale = Mathf.Min(
                oldBounds.size.x / Mathf.Max(size.x, 0.0001f),
                oldBounds.size.y / Mathf.Max(size.y, 0.0001f),
                oldBounds.size.z / Mathf.Max(size.z, 0.0001f)
            );

            model.transform.localScale *= scale;

            // Recalculer le centre après redimensionnement
            renderers = model.GetComponentsInChildren<Renderer>();
            modelBounds = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
                modelBounds.Encapsulate(renderers[i].bounds);

            model.transform.position +=
                oldBounds.center - modelBounds.center;

            foreach (Collider col in
                     model.GetComponentsInChildren<Collider>())
                col.enabled = false;

            oldRenderer.enabled = false;

            Debug.Log("Remplacé : " + target.name);
        }
    }
}


