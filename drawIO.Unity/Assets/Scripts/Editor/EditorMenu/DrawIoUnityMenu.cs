using System.Threading.Tasks;
using Data;
using Managers.ProfileStorage;
using UnityEditor;

public class DrawIoUnityMenu
{
    [MenuItem("DrawIO/Profile/Save")]
    private static Task SaveProfile()
    {
        var profile = new ProfileData();
        var storage = new LocalProfileStorage();
        
        return storage.SaveImmediate(profile);
    }
}
