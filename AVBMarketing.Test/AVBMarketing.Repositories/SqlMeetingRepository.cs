using AVBMarketing.EF;
using AVBMarketing.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace AVBMarketing.Repositories
{
    public class SqlMeetingRepository : IDataRepository<Meeting>
    {

        readonly AVBMarketingContext _context;
        public SqlMeetingRepository(AVBMarketingContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Meeting>> GetMockData()
        {
            var mockRepo = new MockMeetingRepository();

            return await mockRepo.GetAll();
        }
        public async Task<IEnumerable<Meeting>> AddMockData()
        {
            var mockRepo = new MockMeetingRepository();
            var mockList = await mockRepo.GetAll();
            var meetings = _context.Meetings;
            if (meetings.Count() == 0)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    await _context.Meetings.AddRangeAsync(mockList);
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Meeting] ON");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Meeting] OFF");

                    transaction.Commit();
                }
            }
            return mockList;
        }
        public async Task<IEnumerable<Meeting>> GetAll()
        {
            var meetings = await _context.Meetings.ToListAsync();
            if (meetings.Count() == 0)
            {
                meetings = (List<Meeting>)await GetMockData();
            }


            return meetings;
        }
        public async Task<IEnumerable<Meeting>> GetAll(int skip, int take)
        {
            var meetings = await _context.Meetings.Skip(skip).Take(take).ToListAsync();
            if (meetings.Count() == 0)
            {
                meetings = (List<Meeting>)await GetMockData();
            }

            return meetings;
        }
        public async Task<Meeting> Get(int id)
        {
            var meeting = await _context.Meetings.FirstOrDefaultAsync(t => t.Id == id);

            return meeting;
        }
        public async Task<Meeting> Add(Meeting item)
        {

            var result = await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Meeting> Update(Meeting newEntity)
        {
            var result = _context.Meetings.FirstOrDefault(t => t.Id == newEntity.Id);
            if (result != null)
            {
                result.StartDate = newEntity.StartDate;
                result.EndDate = newEntity.EndDate;
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async void Delete(int id)
        {
            var result = _context.Meetings.FirstOrDefault(t => t.Id == id);

            if (result != null)
            {
                _context.Meetings.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Meeting>> GetMeetingsWithOverlaps(List<Meeting> meetings, bool isOverlappingTimes = true)
        {
            var meetingsWithOverLaps = new List<Meeting>();

            await Task.Run(() => 
            {
                foreach (Meeting meeting in meetings)
                {
                    var otherMeetings = GetOtherMeetings(meetings, meeting).Result;

                    if (otherMeetings != null)
                    {
                        var inBetWeenDates = GetInBetweenDates(otherMeetings, meeting, isOverlappingTimes).Result;

                        foreach (var foundfoundmeeting in inBetWeenDates)
                        {
                            if (!meetingsWithOverLaps.Contains(foundfoundmeeting))
                            {
                                meetingsWithOverLaps.Add(foundfoundmeeting);
                            }
                        }
                    }
                }
            });
            
            return meetingsWithOverLaps;
        }

        private async Task<List<Meeting>> GetOtherMeetings(List<Meeting> meetings, Meeting currentMeeting)
        {
            List<Meeting> otherMeetings = null;
            await Task.Run(() => 
            {
                otherMeetings = meetings.Where(m => (m.StartDate != currentMeeting.StartDate && m.EndDate != currentMeeting.EndDate)).ToList();

            });
            
            return otherMeetings;
        }


        private async Task<List<Meeting>> GetInBetweenDates(List<Meeting> meetings, Meeting currentMeeting, bool isOverlappingTimes = true)
        {
            List<Meeting> inBetweenDates = null;

            await Task.Run(() => 
            {
                //if overlapping time, remove the equal (=) operator
                inBetweenDates = meetings.Where(m => (currentMeeting.StartDate > m.StartDate && currentMeeting.StartDate < m.EndDate) || (currentMeeting.EndDate > m.StartDate && currentMeeting.EndDate < m.EndDate)).ToList();

                if (!isOverlappingTimes)
                {
                    inBetweenDates = meetings.Where(m => (currentMeeting.StartDate >= m.StartDate && currentMeeting.StartDate <= m.EndDate) || (currentMeeting.EndDate >= m.StartDate && currentMeeting.EndDate <= m.EndDate)).ToList();
                }

            });
            
            return inBetweenDates;
        }

    }
}