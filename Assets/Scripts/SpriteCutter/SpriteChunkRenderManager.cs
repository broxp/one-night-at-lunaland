using UnityEngine;

[ExecuteInEditMode]
public class SpriteChunkRenderManager : MonoBehaviour
{
	public SpriteChunkData data;

	void Start()
	{
		if (null != data)
		for (var x = 0; x < data.ChunkCountHorizontaly; ++x)
			for (var y = 0; y < data.ChunkCountVerticaly; ++y)
			{
				Sprite s = data[x, y];
				
				GameObject ga = new GameObject("Texture Chunk: " + s.name);
				ga.transform.parent = transform;
				ga.transform.localPosition = new Vector3(x * data.ChunkWidth / s.pixelsPerUnit, y * data.ChunkHeight / s.pixelsPerUnit, 0);
				ga.transform.localScale = Vector3.one;
				ga.hideFlags = HideFlags.DontSave | HideFlags.HideInHierarchy;

				var sr = ga.AddComponent<SpriteRenderer>();
				sr.sprite = s;
			}
	}
	
}
