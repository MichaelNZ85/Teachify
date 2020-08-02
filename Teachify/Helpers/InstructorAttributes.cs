using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teachify.Helpers
{
    public class InstructorAttributes
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Education { get; set; }
        [JsonProperty(PropertyName = "one_line_title")]
        public string OneLineTitle { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        [JsonProperty(PropertyName = "hourly_rate")]
        public string HourlyRate { get; set; }
        [JsonProperty(PropertyName ="course_domain")]
        public string CourseDomain { get; set; }
        public string City { get; set; }
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }
    }
}
