namespace Managers.Profile
{
    public interface IProfileViewService
    {
        string GetNickname();
        void SetNickname(string name);
        int GetSkin();
        void SetSkin(int value);
    }
}