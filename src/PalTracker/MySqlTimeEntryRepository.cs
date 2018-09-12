using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private readonly TimeEntryContext _context;

        public   MySqlTimeEntryRepository(TimeEntryContext timeEntryContext)
        {
            _context  = timeEntryContext;
            
        }
        public TimeEntry  Create(TimeEntry timeEntry){
            var recordToCreate =  timeEntry.ToRecord();
            _context.TimeEntryRecords.Add(recordToCreate);
            _context.SaveChanges();
            return  Find(recordToCreate.Id.Value);
        }
        public void Delete(long id){
            _context.TimeEntryRecords.Remove(FindRecord(id));
            _context.SaveChanges();
        }
        public bool   Contains(long id) => _context.TimeEntryRecords.Any(t => t.Id == id);

        public TimeEntry  Find(long id) =>  FindRecord(id).ToEntity();

        public IEnumerable<TimeEntry> List(){
            return _context.TimeEntryRecords.Select(t => t.ToEntity());
        }
         public TimeEntry Update(long id, TimeEntry timeEntry){
             var RecordToUpdate =  timeEntry.ToRecord();
             RecordToUpdate.Id = id;
             _context.Update(RecordToUpdate);
             _context.SaveChanges();
             return Find(id);
        }
        public  TimeEntryRecord  FindRecord(long id ) =>  _context.TimeEntryRecords.AsNoTracking().SingleOrDefault(x => x.Id == id);
        
    }

}
