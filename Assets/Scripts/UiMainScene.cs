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

    public void PoisonTowerClicked()
    {
        MainManager.Instance.ShowBuildingGridAndPlaceTower(TowerType.PoisonTower);
    }

    public void SpeedTowerClicked()
    {
        MainManager.Instance.ShowBuildingGridAndPlaceTower(TowerType.SpeedTower);
    }

    public void SettingsClicked()
    {
        MainManager.Instance.PauseGameAndShowSettings();
    }
}
