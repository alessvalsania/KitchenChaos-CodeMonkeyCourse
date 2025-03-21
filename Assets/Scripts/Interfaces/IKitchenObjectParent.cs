
using UnityEngine;
public interface IKitchenObjectParent
{
    public Transform GetHoldingPointObject();
    public void SetKitchenObject(KitchenObject newKitchenObject);
    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();
    public bool HasKitchenObject();




}