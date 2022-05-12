namespace AVBMarketing.Test
{
    //Meeting object
    public class Meeting
    {
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public Meeting(DateTime startDate, DateTime endDate) 
        {
            _startDate = startDate;
            _endDate = endDate; 
        }
        public static List<Meeting> GetMeetingsWithOverlaps(List<Meeting> meetings)
        {
            var meetingsWithOverLaps = new List<Meeting>();

            foreach (Meeting meeting in meetings)
            { 
                var otherMeetings = GetOtherMeetings(meetings, meeting);
                
                if (otherMeetings != null)
                {
                    var inBetWeenDates = GetInBetweenDates(otherMeetings, meeting);

                    foreach (var foundfoundmeeting in inBetWeenDates)
                    {
                        if (!meetingsWithOverLaps.Contains(foundfoundmeeting))
                        {
                            meetingsWithOverLaps.Add(foundfoundmeeting);
                        }
                    }
                }

            }
            return meetingsWithOverLaps;
        }

        private static List<Meeting> GetOtherMeetings(List<Meeting> meetings, Meeting currentMeeting)
        {
            var otherMeetings = meetings.Where(m => (m.StartDate != currentMeeting.StartDate && m.EndDate != currentMeeting.EndDate) ).ToList();
            return otherMeetings;
        }


        private static List<Meeting> GetInBetweenDates(List<Meeting> meetings, Meeting currentMeeting, bool isOverlappingTimes = true)
        {
            List<Meeting> inBetweenDates = null;
            //if overlapping time, remove the equal (=) operator
            inBetweenDates = meetings.Where(m => (currentMeeting.StartDate > m.StartDate && currentMeeting.StartDate < m.EndDate) || (currentMeeting.EndDate > m.StartDate && currentMeeting.EndDate < m.EndDate)).ToList();

            if (!isOverlappingTimes)
            {
                inBetweenDates = meetings.Where(m => (currentMeeting.StartDate >= m.StartDate && currentMeeting.StartDate <= m.EndDate) || (currentMeeting.EndDate >= m.StartDate && currentMeeting.EndDate <= m.EndDate)).ToList();
            }

            return inBetweenDates;
        }


    }
}
