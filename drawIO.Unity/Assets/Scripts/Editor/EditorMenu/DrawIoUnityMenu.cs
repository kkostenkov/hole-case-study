using System.Threading.Tasks;
using Data;
using Managers.ProfileStorage;
using UnityEditor;
using UnityEngine;

public class DrawIoUnityMenu
{
    [MenuItem("DrawIO/Profile/Save")]
    private static Task SaveProfile()
    {
        var profile = new ProfileData();
        var storage = new LocalProfileStorage();
        
        return storage.SaveImmediate(profile);
    }
    
    [MenuItem("DrawIO/Profile/Load")]
    private static async Task LoadProfile()
    {
        var storage = new LocalProfileStorage();
        var data = await storage.LoadProfileAsync();
        Debug.Log(data.ToString());
    }
}
