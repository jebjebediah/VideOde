using VideOde.Core;

namespace VideOde.Data.Providers
{
    public interface IClipService
    {
        IEnumerable<Clip> GetAllClips();
        IEnumerable<Clip> GetClips(int count);
        Clip GetClipByTitle(string name);
        Clip GetClipById(int id);
        int GetClipCount();
        void AddClip(Clip clipToAdd);
        void RemoveClip(Clip clipToRemove);
        void AddClipRange(IEnumerable<Clip> clipsList);
    }
}
