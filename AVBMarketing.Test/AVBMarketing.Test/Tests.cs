using AVBMarketing.Repositories;
using System.Globalization;

namespace AVBMarketing.Test
{
    public class Tests
    {
        public void Test1()
        {
            var dateFormat = "MM/dd/yyyy HH:mm";
            var schedules = new string[,]{
            { "01/01/2016 09:00","01/01/2016 11:00"},
            { "01/11/2016 10:00","01/11/2016 13:30"},
            { "01/11/2016 13:30","01/11/2016 16:00"},
            { "01/05/2016 09:00","01/05/2016 11:00"},
            { "12/29/2015 09:00","01/01/2016 10:00"},
            };

            var totalSchedules = schedules.GetLength(0);
            var mettings = new List<Meeting>();

            for (int i = 0; i < totalSchedules; i++)
            {
                var fromDate = DateTime.ParseExact(schedules[i, 0], dateFormat, CultureInfo.InvariantCulture);
                var toDate = DateTime.ParseExact(schedules[i, 1], dateFormat, CultureInfo.InvariantCulture);
                var meeting = new Meeting(fromDate, toDate);
                mettings.Add(meeting);
            }

            var overlaps = Meeting.GetMeetingsWithOverlaps(mettings);

            foreach (var overlap in overlaps)
            { 
                Console.WriteLine(overlap.StartDate.ToString("MMM ddd, yyyy hh:ss") + "-" + overlap.EndDate.ToString("MMM ddd, yyyy hh:ss"));
            }
        }
        
    }
}
