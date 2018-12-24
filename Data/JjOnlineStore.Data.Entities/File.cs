using JjOnlineStore.Data.Entities.Base;

namespace JjOnlineStore.Data.Entities
{
    public class File : BaseDeletableModel<long>
    {
        public string Path { get; set; }

        public string Extension { get; set; }
    }
}