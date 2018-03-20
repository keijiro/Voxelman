using UnityEngine;

public class MeshBaker : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer _source;
    [SerializeField] MeshCollider _target;

    Mesh _mesh;

    void Start()
    {
        _mesh = new Mesh();
    }

    void Update()
    {
        transform.position = _source.transform.position;
        transform.rotation = _source.transform.rotation;

        _source.BakeMesh(_mesh);
        _target.sharedMesh = _mesh;
    }
}
