using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectsSO : ScriptableObject
{
    // We made it public because as a rule, we are never going to write in a SO
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
