#region Using statements

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

#endregion

namespace Bitgem.Editor
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Core.FlagEnumAttribute))]
    public class EnumFlagsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            _property.intValue = EditorGUI.MaskField(_position, _label, _property.intValue, _property.enumNames);
        }
    }
#endif
}
