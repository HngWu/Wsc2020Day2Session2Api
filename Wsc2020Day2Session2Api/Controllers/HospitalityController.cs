using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using Wsc2020Day2Session2Api.Models;

namespace Wsc2020Day2Session2Api.Controllers
{
    public class HospitalityController : Controller
    {
        Wsc2020Day2Session2Context context = new Wsc2020Day2Session2Context();


        public class tempCompetitor
        {
            public string Id { get; set; } = null!;

            public string FullName { get; set; } = null!;

            public string Email { get; set; } = null!;

            public string Password { get; set; } = null!;
        }

        public class tempAnnouncement
        {
            public int Id { get; set; }

            public string Announcementdate { get; set; }

            public string AnnouncementTitle { get; set; } = null!;

            public string AnnouncementDescription { get; set; } = null!;
        }

        [HttpPost]
        public IActionResult AddCompetitor(tempCompetitor competitor)
        {

            try
            {
                var newCompetitor = new Competitor
                {
                    Id = competitor.Id,
                    FullName = competitor.FullName,
                    Email = competitor.Email,
                    Password = competitor.Password,
                };

                context.Competitors.Add(newCompetitor);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(404);
            }
        }

        [HttpPost]
        public IActionResult addAnnouncement(tempAnnouncement announcement)
        {

            try
            {
                var newAnnouncement = new Announcement
                {
                    Announcementdate = DateTime.Parse(announcement.Announcementdate),
                    AnnouncementTitle = announcement.AnnouncementTitle,
                    AnnouncementDescription = announcement.AnnouncementDescription,
                };
                context.Announcements.Add(newAnnouncement);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(404);
            }
        }

        [HttpPost]
        public IActionResult Login()
        {
            return Ok();

        }


        [HttpGet]
        public IActionResult getAccouncements()
        {
            try
            {
                var announcements = context.Announcements.OrderBy(x => x.Announcementdate).ToList();
                return Ok(announcements);
            }
            catch (Exception)
            {

                return NotFound() ;
            }
            
        }
        [HttpGet]
        public IActionResult getCompetitor(string competitorId)
        {

            try
            {
                var competitor = context.Competitors.Where(x => x.Id == competitorId).FirstOrDefault();

                if (competitor != null)
                {
                    return Ok(competitor);

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                NotFound();
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult checkIn(string id)
        {
            try
            {
                var ischeckedIn = context.CheckIns.Where(x => x.CompetitorId == id).Any();

                if (ischeckedIn)
                {
                    return StatusCode(201);

                }
                else
                {
                    var checkIn = new CheckIn
                    {
                        CompetitorId = id,
                    };
                    return Ok(checkIn);
                }
            }
            catch (Exception)
            {

                NotFound();
            }
            return BadRequest();
           

        }



    }
}
