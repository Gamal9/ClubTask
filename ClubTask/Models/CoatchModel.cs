using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClubTask.Models
{
    public class CoatchModel
    {
        public int ID { get; set; }
        public string CoachName { get; set; }
        public Nullable<int> Age { get; set; }
        public string PhoneNumber { get; set; }
        public string TrainingName { get; set; }
        public Nullable<int> TrainingID { get; set; }
    }
}