using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountUpdateScript : MonoBehaviour
{
    public Text text;

    public void UpdateBulletCounter(int? bulletCount, int? bulletMax)
    {
        if (bulletCount > 0)
        {
            text.text = $"Bullets: {bulletCount}/{bulletMax}";
        }
        else
        {
//            _ = DisplayReloadTimeRemaining();
        }
    }

    public async Task DisplayReloadTimeRemaining()
    {
        for (float reloadCounter = 2f;
            reloadCounter > 0;
            reloadCounter -= 0.1f)
        {
            text.text = $"Reloading... {reloadCounter}";
            await Task.Delay(100);
        }
    }
}
