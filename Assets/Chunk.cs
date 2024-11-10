using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Material material;
    void Start()
    {
        MeshFilter mf = this.gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = this.gameObject.AddComponent<MeshRenderer>();
        mr.material = material;
    }

    void Update()
    {
        
    }
}
