using System.Collections.Generic;

namespace ClienteService.Core.Entities.Api
{
    public class ApiError
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
        public IDictionary<string, string[]> errors { get; set; }
    }
}
