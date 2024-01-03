using HemFixBack.Models;

namespace HemFixBack.Services
{
    public interface IGardenTaskService
    {
        public GardenTask Create(GardenTask task);
        public GardenTask Get(string id);
        public List<GardenTask> List();
        public GardenTask Update(GardenTask task);
        public bool Delete(string id);
    }
}
