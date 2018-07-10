using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NerdDinner2.Models
{
    public class DinnerRepository
    {

        private NerdDinnersDataContext db = new NerdDinnersDataContext();

        //
        // Query Methods

        public IQueryable<Dinner> FindAllDinners()
        {
            return db.Dinners;
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in db.Dinners
                where dinner.EventDate > DateTime.Now
                orderby dinner.EventDate
                select dinner;
        }

        public Dinner GetDinner(int id)
        {
            return db.Dinners.SingleOrDefault(d => d.DinnerID == id);
        }

        //
        // Insert/Delete Methods

        public void Add(Dinner dinner)
        {
            db.Dinners.Add(dinner);
            Save();
        }

        public void Delete(Dinner dinner)
        {
            if (dinner.RSVPs != null)
            {
                foreach (var rsvp in dinner.RSVPs)
                {
                    db.RSVPs.Remove(rsvp);
                }
            }

            db.Dinners.Remove(dinner);
            Save();
        }

        //
        // Persistence

        public void Save()
        {
            db.SaveChanges();
        }

        public int getLength()
        {
            return db.Dinners.ToList().Count;
        }

        public int GetUnusedDinnerID()
        {
            List<int> usedDinnerIDs = new List<int>();
            int newDinnerID = 1;

            foreach(var dinner in db.Dinners)
            {
                usedDinnerIDs.Add(dinner.DinnerID);
            }

            while (usedDinnerIDs.Contains(newDinnerID))
            {
                newDinnerID++;
            }

            return newDinnerID;
        }
    }

}