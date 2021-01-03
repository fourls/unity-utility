using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace UnityEngine.Tilemaps
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Delay Animated Tile", menuName = "Tiles/Delay Animated Tile")]
    public class DelayAnimatedTile : TileBase
    {
        public Sprite m_defaultSprite;
        public int m_delayFrames = 0;
        public Sprite[] m_AnimatedSprites;
        public float m_Speed = 1f;
        public Tile.ColliderType m_TileColliderType;

        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.identity;
            tileData.color = Color.white;
            if (m_AnimatedSprites != null && m_AnimatedSprites.Length > 0)
            {
                tileData.sprite = m_AnimatedSprites[m_AnimatedSprites.Length - 1];
                tileData.colliderType = m_TileColliderType;
            }
        }

        public override bool GetTileAnimationData(Vector3Int location, ITilemap tileMap, ref TileAnimationData tileAnimationData) {
            if (m_AnimatedSprites.Length > 0) {
                Sprite[] animationFrames = m_AnimatedSprites;
                animationFrames = animationFrames.Concat(Enumerable.Repeat(m_defaultSprite,m_delayFrames)).ToArray();

                tileAnimationData.animatedSprites = animationFrames;
                tileAnimationData.animationSpeed = m_Speed;
                tileAnimationData.animationStartTime = Random.Range(0,animationFrames.Length * m_Speed);
                return true;
            }
            return false;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(DelayAnimatedTile))]
    public class DelayAnimatedTileEditor : Editor
    {
        private DelayAnimatedTile tile { get { return (target as DelayAnimatedTile); } }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            tile.m_defaultSprite = (Sprite)EditorGUILayout.ObjectField("Default Sprite", tile.m_defaultSprite, typeof(Sprite),false,null);
            tile.m_delayFrames = EditorGUILayout.IntField("Delay Frames", tile.m_delayFrames);

            int count = EditorGUILayout.DelayedIntField("Number of Animated Sprites", tile.m_AnimatedSprites != null ? tile.m_AnimatedSprites.Length : 0);
            if (count < 0)
                count = 0;
                
            if (tile.m_AnimatedSprites == null || tile.m_AnimatedSprites.Length != count)
            {
                Array.Resize<Sprite>(ref tile.m_AnimatedSprites, count);
            }

            if (count == 0)
                return;

            EditorGUILayout.LabelField("Place sprites shown based on the order of animation.");
            EditorGUILayout.Space();

            for (int i = 0; i < count; i++)
            {
                tile.m_AnimatedSprites[i] = (Sprite) EditorGUILayout.ObjectField("Sprite " + (i+1), tile.m_AnimatedSprites[i], typeof(Sprite), false, null);
            }
            
            float speed = EditorGUILayout.FloatField("Speed", tile.m_Speed);
            if (speed < 0.0f)
                speed = 0.0f;
            
            tile.m_Speed = speed;

            tile.m_TileColliderType=(Tile.ColliderType) EditorGUILayout.EnumPopup("Collider Type", tile.m_TileColliderType);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);
        }
    }
#endif
}
