namespace AVBMarketing.EF.Models
{
    //Meeting object
    public class Meeting
    {
        public virtual int Id { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
    }
}