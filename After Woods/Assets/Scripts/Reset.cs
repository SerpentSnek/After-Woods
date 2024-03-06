using UnityEngine;

public interface Reset
{
    // Have game object parameter be optional (primarily for Timer)
    void ResetStats(GameObject obj = null);
}
