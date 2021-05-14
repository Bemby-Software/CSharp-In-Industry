using System.Collections.Generic;

namespace Site.Core.Apis.GitHub.DTO
{
    public class IssueDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public IEnumerable<LabelDto> Labels { get; set; }
    }
}