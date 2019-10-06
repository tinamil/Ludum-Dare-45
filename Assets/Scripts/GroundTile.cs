using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.Tilemaps
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Ground Tile", menuName = "Tiles/Ground Tile")]
    public class GroundTile : Tile
    {
        [SerializeField]
        public int speed;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GroundTile))]
    public class GroundTileEditor : Editor
    {
        private GroundTile tile { get { return (target as GroundTile); } }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            tile.speed = EditorGUILayout.IntField("Speed", tile.speed);
            float oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 210;

            tile.sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", tile.sprite, typeof(Sprite), false);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);

            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }
#endif
}