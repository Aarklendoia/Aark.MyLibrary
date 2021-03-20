using System.Collections.Generic;

namespace Aark.Epub.Schema
{
    public class EpubSpine : List<EpubSpineItemRef>
    {
        public string Id { get; set; }
        public PageProgressionDirection? PageProgressionDirection { get; set; }
        public string Toc { get; set; }
    }
}
