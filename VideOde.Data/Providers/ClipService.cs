using VideOde.Core;

namespace VideOde.Data.Providers
{
    public class ClipService : IClipService
    {
        private VideOdeContext _videOdeContext;

        public ClipService(VideOdeContext videOdeContext)
        {
            _videOdeContext = videOdeContext;
        }

        public IEnumerable<Clip> GetAllClips()
        {
            return _videOdeContext.Clips;
        }

        public IEnumerable<Clip> GetClips(int count)
        {
            return _videOdeContext.Clips.Take(count);
        }

        public int GetClipCount()
        {
            return _videOdeContext.Clips.Count();
        }

        public void AddClip(Clip clipToAdd)
        {
            _videOdeContext.Clips.Add(clipToAdd);
            _videOdeContext.SaveChanges();
        }

        public void AddClipRange(IEnumerable<Clip> clipsToAdd)
        {
            _videOdeContext.Clips.AddRange(clipsToAdd);
            _videOdeContext.SaveChanges();
        }

        public void RemoveClip(Clip clipToRemove)
        {
            _videOdeContext.Clips.Remove(clipToRemove);
            _videOdeContext.SaveChanges();
        }

        public Clip GetClipByTitle(string name)
        {
            return _videOdeContext.Clips.SingleOrDefault(c => c.Title == name);
        }

        public Clip GetClipById(int id)
        {
            return _videOdeContext.Clips.SingleOrDefault(c => c.Id == id);
        }
    }
}
