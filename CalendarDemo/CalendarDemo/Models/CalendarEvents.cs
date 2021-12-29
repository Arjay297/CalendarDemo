using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarDemo.Models
{
    public class CalendarEvents
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string EventInformation { get; set; }
        public DateTime EventDate { get; set; }
    }
}
