using UnityEngine;

public class UiMainScene : MonoBehaviour
{
    public void CannonTowerClicked()
    {
        MainManager.Instance.ShowBuildingGridAndPlaceTower(TowerType.CannonTower);
    }

    public void BasicTowerClicked()
    {
        MainManager.Instance.ShowBuildingGridAndPlaceTower(TowerType.BasicTower);
    }

    public void SettingsClicked()
    {
        MainManager.Instance.PauseGameAndShowSettings();
    }
}
