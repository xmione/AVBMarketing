using AVBMarketing.EF.Models;
using System.Globalization;

namespace AVBMarketing.Repositories
{
    public class MockMeetingRepository : IDataRepository<Meeting>
    {
        private List<Meeting> _list;
        public MockMeetingRepository()
        {
            _list = new List<Meeting>();


            var dateFormat = "MM/dd/yyyy HH:mm";
            var schedules = new string[,]{
            { "01/01/2016 09:00","01/01/2016 11:00"},
            { "01/11/2016 10:00","01/11/2016 13:30"},
            { "01/11/2016 13:30","01/11/2016 16:00"},
            { "01/05/2016 09:00","01/05/2016 11:00"},
            { "12/29/2015 09:00","01/01/2016 10:00"},
            };

            var totalSchedules = schedules.GetLength(0);
            

            for (int i = 0; i < totalSchedules; i++)
            {
                var fromDate = DateTime.ParseExact(schedules[i, 0], dateFormat, CultureInfo.InvariantCulture);
                var toDate = DateTime.ParseExact(schedules[i, 1], dateFormat, CultureInfo.InvariantCulture);
                
                var meeting = new Meeting();
                meeting.Id = i+1;
                meeting.StartDate = fromDate;
                meeting.EndDate = toDate;

                _list.Add(meeting);
            }
        }
        public async Task<Meeting> Add(Meeting item)
        {
            await Task.Run(() =>
            {
                item.Id = _list.Max(e => e.Id) + 1;
                _list.Add(item);
            });

            return item;
        }

        public void Delete(int id)
        {
            Meeting item = _list.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                //found, delete
                _list.Remove(item);
            }

        }

        public async Task<IEnumerable<Meeting>> GetAll()
        {
            List<Meeting> list = null;
            await Task.Run(() =>
            {
                list = _list;
            });

            return list;
        }

        public async Task<Meeting> Get(int id)
        {
            Meeting item = null;
            await Task.Run(() =>
            {
                item = _list.FirstOrDefault(t => t.Id == id);
            });

            return item;
        }

        public async Task<Meeting> Update(Meeting newItem)
        {
            Meeting item = null;
            await Task.Run(() =>
            {
                item = _list.FirstOrDefault(t => t.Id == newItem.Id);
                if (item != null)
                {
                    //found, update
                    item.StartDate = newItem.StartDate;
                    item.EndDate = newItem.EndDate;
                }
            });

            return item;
        }

        public Task<Meeting> Update(Meeting sourceEntity, Meeting targtEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Meeting entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Meeting>> AddMockData()
        {
            throw new NotImplementedException();
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
            var otherMeetings = meetings.Where(m => (m.StartDate != currentMeeting.StartDate && m.EndDate != currentMeeting.EndDate)).ToList();
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
