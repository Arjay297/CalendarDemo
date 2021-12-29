using CalendarDemo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CalendarDemo.Services
{
    internal static class CalendarEventService
    {
        static SQLiteAsyncConnection db;


        /// <summary>
        /// This initialize the sqlite database
        /// </summary>
        /// <returns></returns>
        public static async Task Init()
        {
            if (db != null)
                return;

            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "TheOneData.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<CalendarEvents>();
        }
       

        /// <summary>
        /// This method add event to the database
        /// </summary>
        /// <param name="title">The title of the event</param>
        /// <param name="eventInformation">Information about the event</param>
        /// <param name="eventDate">The date of the event</param>
        /// <returns></returns>
        public static async Task AddEvents(string title, string eventInformation, DateTime eventDate)
        { 
            await Init();
            CalendarEvents calendarEvent = new CalendarEvents()
            { 
                Title= title,
                EventInformation= eventInformation,
                EventDate= eventDate
            };
            int id = await db.InsertAsync(calendarEvent);
        }

        /// <summary>
        /// This method removes the event to the database
        /// </summary>
        /// <param name="id">Event primary key</param>
        /// <returns></returns>
        public static async Task RemoveEvent(int id)
        {
            await Init();

            await db.DeleteAsync(id);
        }


        /// <summary>
        /// Gets all the event stored in the database
        /// </summary>
        /// <returns>List of events a user have made</returns>
        public static async Task<List<CalendarEvents>> GetAllEvents()
        {
            await Init();
            List<CalendarEvents> e = await db.Table<CalendarEvents>().ToListAsync();
            return await db.Table<CalendarEvents>().ToListAsync();
        }

        /// <summary>
        /// Gets a specific event by its primary Key
        /// </summary>
        /// <param name="id">Event Primary Key</param>
        /// <returns>Returns a single event</returns>
        public static async Task<CalendarEvents> GetEventById(int id)
        {
            await Init();
            return await db.Table<CalendarEvents>().Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public static async Task<CalendarEvents> GetEventByDate(string date)
        { 
            DateTime selectedDate = Convert.ToDateTime(date);
            return await db.Table<CalendarEvents>().Where(c => c.EventDate == selectedDate).FirstOrDefaultAsync();
        }
    }
}
