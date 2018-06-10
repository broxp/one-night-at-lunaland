using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteChunkData))]
public class SpriteChunkDataEditor : Editor
{
    private SpriteChunkData data;

    void OnEnable()
    {
        data = target as SpriteChunkData;
    }

    public override void OnInspectorGUI()
    {
        OnNotSame(data.EditorOnlyTextureToSplit,
            EditorGUILayout.ObjectField("Texture", data.EditorOnlyTextureToSplit, typeof(Texture2D),
                false) as Texture2D,
            t => data.EditorOnlyTextureToSplit = t);

        OnNotSame(data.EditorOnlyChunkWidth,
            EditorGUILayout.DelayedIntField("Chunk Width", data.EditorOnlyChunkWidth),
            t => data.EditorOnlyChunkWidth = t);

        OnNotSame(data.EditorOnlyChunkHeight,
            EditorGUILayout.DelayedIntField("Chunk Height", data.EditorOnlyChunkHeight),
            t => data.EditorOnlyChunkHeight = t);

        EditorGUILayout.LabelField("Chunk Count Horizontaly: ", data.ChunkCountHorizontaly.ToString());
        EditorGUILayout.LabelField("Chunk Count Verticaly: ", data.ChunkCountVerticaly.ToString());

        EditorUtility.SetDirty(data);
    }

    private void OnNotSame<T>(T t1, T t2, Action<T> onNotSame)
    {
        if (!EqualityComparer<T>.Default.Equals(t1, t2))
        {
            onNotSame(t2);
            EditorUtility.SetDirty(data);
        }
    }
}