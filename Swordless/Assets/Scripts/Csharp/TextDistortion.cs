using UnityEngine;
using TMPro;

public class TextDistortion : MonoBehaviour
{
    public float maxLetterDistortionAmount = 0.1f;
    public float distortionSpeed = 1.0f;

    private TextMeshProUGUI textMeshPro;
    private Vector3[][] originalVertices;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Ensure the text has the appropriate mesh data
        textMeshPro.ForceMeshUpdate();

        // Save the original vertices for each submesh
        originalVertices = new Vector3[textMeshPro.textInfo.meshInfo.Length][];
        for (int i = 0; i < textMeshPro.textInfo.meshInfo.Length; i++)
        {
            originalVertices[i] = textMeshPro.textInfo.meshInfo[i].vertices.Clone() as Vector3[];
        }
    }

    void Update()
    {
        // Get the current mesh info
        TMP_MeshInfo[] meshInfo = textMeshPro.textInfo.meshInfo;

        // Apply distortion to each submesh
        for (int i = 0; i < meshInfo.Length; i++)
        {
            Vector3[] vertices = meshInfo[i].vertices;

            // Apply random distortion to each corner individually
            for (int j = 0; j < vertices.Length; j++)
            {
                float randomDistortion = Random.Range(-maxLetterDistortionAmount, maxLetterDistortionAmount);
                vertices[j].y = originalVertices[i][j].y + Mathf.Sin(Time.time * distortionSpeed) * randomDistortion;
            }

            // Update the text mesh with the distorted vertices
            textMeshPro.textInfo.meshInfo[i].mesh.vertices = vertices;
            textMeshPro.UpdateGeometry(meshInfo[i].mesh, i);
        }
    }
}