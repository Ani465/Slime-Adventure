using UnityEngine;

public class CustomizePlayer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer playerMeshRenderer; // Player's Skinned Mesh Renderer
    [SerializeField] private Material[] bodyMaterials; // Materials for the body
    [SerializeField] private Material[] faceMaterials; // Materials for the face

    private int _currentBodyMaterialIndex; // Tracks the body material
    private int _currentFaceMaterialIndex; // Tracks the face material

    public void ChangeBodyMaterial()
    {
        if (bodyMaterials.Length == 0 || playerMeshRenderer == null)
        {
            Debug.LogWarning("Please assign the body materials and Skinned Mesh Renderer in the inspector.");
            return;
        }

        _currentBodyMaterialIndex = (_currentBodyMaterialIndex + 1) % bodyMaterials.Length;

        var updatedMaterials = playerMeshRenderer.materials;
        updatedMaterials[0] = bodyMaterials[_currentBodyMaterialIndex]; // Assuming body material is at index 0
        playerMeshRenderer.materials = updatedMaterials;
    }

    public void ChangeFaceMaterial()
    {
        if (faceMaterials.Length == 0 || playerMeshRenderer == null)
        {
            Debug.LogWarning("Please assign the face materials and Skinned Mesh Renderer in the inspector.");
            return;
        }

        _currentFaceMaterialIndex = (_currentFaceMaterialIndex + 1) % faceMaterials.Length;

        var updatedMaterials = playerMeshRenderer.materials;
        updatedMaterials[1] = faceMaterials[_currentFaceMaterialIndex]; // Assuming face material is at index 1
        playerMeshRenderer.materials = updatedMaterials;
    }
}