using UnityEngine;

public class UiMainScene : MonoBehaviour
{
    public void CannonTowerClicked()
    {
        MainManager.Instance.ShowBuildingGridAndPlaceTower();
    }
}
