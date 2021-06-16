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

    public void PosionTowerClicked()
    {
        MainManager.Instance.ShowBuildingGridAndPlaceTower(TowerType.PosionTower);
    }

    public void SettingsClicked()
    {
        MainManager.Instance.PauseGameAndShowSettings();
    }
}
