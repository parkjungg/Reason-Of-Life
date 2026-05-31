using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SceneTransition : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap entranceTilemap;

    [Header("Scene")] [SerializeField] private string targetSceneName = "Ex_Town";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"트리거 감지: {other.name}, 태그: {other.tag}");
        if (!other.CompareTag("Player")) return;
        
        Vector3Int cell = entranceTilemap.WorldToCell(other.transform.position);
        Debug.Log($"셀 위치: {cell}, 타일 있음: {entranceTilemap.HasTile(cell)}");

        
        SceneManager.LoadScene(targetSceneName);
    }
}
