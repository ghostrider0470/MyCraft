using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block {

    public Mesh Mesh;
    
    public Block(Vector3 offset)
    {
        

        Quad[] quads = new Quad[6];
        quads[0] = new Quad(MeshUtils.BlockSide.BOTTOM, new Vector3(0, 0, 0), MeshUtils.BlockType.GRASSTOP);
        quads[1] = new Quad(MeshUtils.BlockSide.TOP, new Vector3(0, 0, 0), MeshUtils.BlockType.GRASSTOP);
        quads[2] = new Quad(MeshUtils.BlockSide.LEFT, new Vector3(0, 0, 0), MeshUtils.BlockType.GRASSTOP);
        quads[3] = new Quad(MeshUtils.BlockSide.RIGHT, new Vector3(0, 0, 0), MeshUtils.BlockType.GRASSTOP);
        quads[4] = new Quad(MeshUtils.BlockSide.FRONT, new Vector3(0, 0, 0), MeshUtils.BlockType.GRASSTOP);
        quads[5] = new Quad(MeshUtils.BlockSide.BACK, new Vector3(0, 0, 0), MeshUtils.BlockType.GRASSTOP);

        Mesh[] sideMeshes = new Mesh[6];
        sideMeshes[0] = quads[0].mesh;
        sideMeshes[1] = quads[1].mesh;
        sideMeshes[2] = quads[2].mesh;
        sideMeshes[3] = quads[3].mesh;
        sideMeshes[4] = quads[4].mesh;
        sideMeshes[5] = quads[5].mesh;

        Mesh = MeshUtils.MergeMeshes(sideMeshes);
        Mesh.name = "Cube_0_0_0";
    }
}
