using UnityEngine;

public class UiMainScene : MonoBehaviour
{
    public void CannonTowerClicked()
    {
        Debug.Log("Cannon tower clicked");
        MainManager.Instance.ShowBuildingGridAndPlaceTower();
    }

}
