using UnityEditor;
using Box;
using UnityEngine;
using static Box.BoxConfiguration;

[CustomEditor(typeof(BoxConfiguration))]
[CanEditMultipleObjects]
public class MyEditorClass : Editor
{
    private BoxConfiguration _boxConfig;

    public void OnEnable()
    {
        _boxConfig = (BoxConfiguration)target;
    }

    public override void OnInspectorGUI()
    {
        
        _boxConfig._position = EditorGUILayout.Vector3Field("Initial position:",_boxConfig._position);

        EditorGUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Box Prefab:");
        _boxConfig._prefab = (BoxMediator)EditorGUILayout.ObjectField(_boxConfig._prefab, typeof(BoxMediator), false);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        _boxConfig._typeGame = (TypeGame)EditorGUILayout.EnumPopup("Select game:", _boxConfig._typeGame);

        switch (_boxConfig._typeGame)
            {
                case TypeGame.Landfill:
                    _boxConfig._boxEasyDetailsOrganic = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsOrganic, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsOrganic = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsOrganic, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsYellow = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsYellow, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsYellow = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsYellow, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsBlue= (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsBlue, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsGreen = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsGreen, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsGrey = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsGrey, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsGrey = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsGrey, typeof(BoxConfigurationDetails), false);
                    break;
                case TypeGame.Park:
                    _boxConfig._boxEasyDetailsBiodegradable = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsBiodegradable, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsBiodegradable = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsBiodegradable, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsNonBiodegradable = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsNonBiodegradable, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsNonBiodegradable = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsNonBiodegradable, typeof(BoxConfigurationDetails), false);
                    break;
                case TypeGame.Recycle:
                    _boxConfig._boxEasyDetailsVegetal = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsVegetal, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsVegetal = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsVegetal, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsAnimal = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsAnimal, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsAnimal = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsAnimal, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsSintetic = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsSintetic, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsSintetic = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsSintetic, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsUniversal = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsUniversal, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsUniversal = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsUniversal, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsFossil = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsFossil, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsFossil = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsFossil, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxEasyDetailsMineral = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxEasyDetailsMineral, typeof(BoxConfigurationDetails), false);
                    _boxConfig._boxHardDetailsMineral = (BoxConfigurationDetails)EditorGUILayout.ObjectField(_boxConfig._boxHardDetailsMineral, typeof(BoxConfigurationDetails), false);
                break;
                default:
                    Debug.LogError("Unrecognized Option");
                    break;
            }

        EditorUtility.SetDirty(_boxConfig);
    }
}
    
