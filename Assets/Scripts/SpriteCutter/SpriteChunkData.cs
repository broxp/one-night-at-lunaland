using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

/**
 * Class which holds data for texture chunks that compose one bigger image.
 */
[CreateAssetMenu(fileName = "SpriteChunkData", menuName = "Sprite Chunk Data", order = 1)]
public class SpriteChunkData : ScriptableObject
{
    //Unity is not able to persist a 2d array but arrays of arrays.
    [SerializeField] private int _chunkCountHorizontaly, _chunkCountVerticaly;
    [SerializeField] private int _chunkWidth = 1024, _chunkHeight = 1024;
    [SerializeField] private Texture2D[] _textureChunks = new Texture2D[0];
    [SerializeField] private Sprite[] _spriteChunks = new Sprite[0];

    public int ChunkCountHorizontaly
    {
        get { return _chunkCountHorizontaly; }
    }

    public int ChunkWidth
    {
        get { return _chunkWidth; }
    }

    public int ChunkCountVerticaly
    {
        get { return _chunkCountVerticaly; }
    }
    
    public int ChunkHeight
    {
        get { return _chunkHeight; }
    }

    public Sprite this[int x, int y]
    {
        get { return _spriteChunks[GetIndexFor(x, y)]; }
    }

    private int GetIndexFor(int x, int y)
    {
        return x + y * _chunkCountHorizontaly;
    }

    //Needed only for offline work, not in the final build
#if UNITY_EDITOR
    [SerializeField] private Texture2D _textureToSplit;

    public Texture2D EditorOnlyTextureToSplit
    {
        get { return _textureToSplit; }
        set
        {
            if (_textureToSplit == value) return;
            _textureToSplit = value;
            UpdadeChunks();
        }
    }

    public int EditorOnlyChunkWidth
    {
        get { return _chunkWidth; }
        set
        {
            if (_chunkWidth == value || _chunkWidth < 512) return;
            _chunkWidth = value;
            UpdadeChunks();
        }
    }

    public int EditorOnlyChunkHeight
    {
        get { return _chunkHeight; }
        set
        {
            if (_chunkHeight == value || _chunkHeight < 512) return;
            _chunkHeight = value;
            UpdadeChunks();
        }
    }

    private void UpdadeChunks()
    {
        DestroyAllChunks();

        if (null == _textureToSplit)
        {
            _chunkCountHorizontaly = _chunkCountVerticaly = 0;
            _textureChunks = new Texture2D[0];
            _spriteChunks = new Sprite[0];
        }
        else
        {
            _chunkCountHorizontaly = (_textureToSplit.width - 1) / _chunkWidth + 1;
            _chunkCountVerticaly = (_textureToSplit.height - 1) / _chunkHeight + 1;
            _textureChunks = new Texture2D[_chunkCountHorizontaly * _chunkCountVerticaly];
            _spriteChunks = new Sprite[_chunkCountHorizontaly * _chunkCountVerticaly];
            
            for (var x = 0; x < _chunkCountHorizontaly; ++x)
            for (var y = 0; y < _chunkCountVerticaly; ++y)
            {
                string name = "__"+_textureToSplit.name + " (" + x + ", " + y + ")";
                int index = GetIndexFor(x, y);
                EditorUtility.DisplayProgressBar("Create texture chunks", "Created texture: "+name+" as "+index+"/" +_textureChunks.Length, (float)index / _textureChunks.Length);
                
                int chunkX = _chunkWidth * x, chunkY = _chunkHeight * y;

                var chunkWidth = Math.Min(_textureToSplit.width - chunkX, _chunkWidth+2);
                var chunkHeight = Math.Min(_textureToSplit.height - chunkY, _chunkHeight+2);
                var texXY = new Texture2D(chunkWidth, chunkHeight, _textureToSplit.format, false);
                texXY.name = name;
                //Graphics.CopyTexture can not be used because we dont want a gpu side copy.
                var pixels = _textureToSplit.GetPixels(chunkX, chunkY, chunkWidth, chunkHeight);
                texXY.SetPixels(0, 0, chunkWidth, chunkHeight, pixels);
                texXY.Apply();
                _textureChunks[index] = texXY;
                
                Sprite s = Sprite.Create(texXY, new Rect(0,0, texXY.width, texXY.height), Vector2.zero);
                _spriteChunks[index] = s;
            }
        }
        SaveAllChunks();
    }

    private void DestroyAllChunks()
    {
        foreach (var sprite in _spriteChunks)
        {
            DestroyImmediate(sprite, true);
        }
        foreach (var texture in _textureChunks)
        {
            DestroyImmediate(texture, true);
        }
        AssetDatabase.Refresh();
    }

    private void SaveAllChunks()
    {
        foreach (var texture in _textureChunks)
        {
            AssetDatabase.AddObjectToAsset(texture, this);
            EditorUtility.DisplayProgressBar("Create texture chunks", "Save new textures...", 100);
        }
        foreach (var sprite in _spriteChunks)
        {
            AssetDatabase.AddObjectToAsset(sprite, this);
            EditorUtility.DisplayProgressBar("Create texture chunks", "Save new sprites...", 100);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
    }
#endif
}