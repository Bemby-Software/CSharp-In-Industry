namespace Site.Core.DTO.Common
{
    public class ErrorDto
    {
        public string Code { get; set; }

        public string Reason { get; set; }

        public bool IsUserFriendly { get; set; }
    }
}   