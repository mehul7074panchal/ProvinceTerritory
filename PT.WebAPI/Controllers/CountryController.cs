using Microsoft.AspNetCore.Mvc;
using PT.Model;
using PT.Services.Contract;
using PT.Services.Services;
using System;
using System.Linq;
using System.Net;

namespace PT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        #region Private Variable

        private readonly IService<tblCountry> _service;
        private readonly ICountryService _service_Country;

        #endregion

        #region Constructor

        public CountryController()
        {
            _service = new Service<tblCountry>();
            _service_Country = new CountryService();
        }

        #endregion


        #region CountryRegistraction

        /// <summary>
        /// CountryRegistraction by passing deviedetail object.
        /// </summary>
        /// <param name="tblCountry"></param>
        /// <returns>Registred Country Object</returns>
        [HttpPost]
        [Produces(typeof(tblCountry))]
        public IActionResult PostCountry([FromBody]tblCountry CountryDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var matchedCountrylst = _service.SelectAll(new tblCountry() { Name = CountryDetail.Name });

            tblCountry tblCountry = CountryDetail;
            if (matchedCountrylst.Count == 0)
            {


                tblCountry.IsDeleted = false;
                tblCountry.CreatedOnDate = DateTime.Now;
                tblCountry.CreatedBy = 0;

                tblCountry.CountryID = _service.Insert(tblCountry);

                if (tblCountry.CountryID == null)
                {

                    return BadRequest("Country did not registraction.something wrong is going.\n kindly try again later.");
                }


            }
            else
            {

                matchedCountrylst[0].Name = CountryDetail.Name;
               



                return PutCountry(matchedCountrylst[0].CountryID.Value, matchedCountrylst[0]);
            }



            return CreatedAtAction("GetCountry", new { id = CountryDetail.CountryID }, CountryDetail);


        }

        #endregion

        #region GetCountrysInfo

        /// <summary>
        /// Get List of Countrys by object attribute use as filter
        /// </summary>
        /// <param name = "tblCountry" ></ param >
        /// < returns >Lis of Country </ returns >
        [HttpGet]
        [Produces(typeof(tblCountry))]
        public IActionResult GetCountry([FromBody]tblCountry tblCountry)
        {

            if (tblCountry == null)
            {

                tblCountry = new tblCountry();
            }
            var ListCountryDetails = _service.SelectAll(tblCountry);
            var results = new ObjectResult(ListCountryDetails)
            {
                StatusCode = (int)HttpStatusCode.OK
            };




            Request.HttpContext.Response.Headers.Add("X-Total-Count", ListCountryDetails.Count + "");

            return results;


        }



        /// <summary>
        /// Get a CountryDetail by CountryID
        /// </summary>
        /// <param name="id">CountryID</param>
        /// <returns>Objetct of Country</returns>
        [HttpGet("{id}")]
        [Produces(typeof(tblCountry))]
        public IActionResult GetCountry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CountryDetails = _service.SelectAll(new tblCountry() { CountryID = id });
            if (CountryDetails == null)
            {
                return NotFound();
            }

            return Ok(CountryDetails);

        }

        #endregion

        #region UpdateCountry

        [HttpPut("{id}")]
        [Produces(typeof(tblCountry))]
        public IActionResult PutCountry(int id, [FromBody] tblCountry CountryDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CountryDetail.CountryID)
            {
                return BadRequest();
            }

            CountryDetail.UpdatedBy = 0;
            CountryDetail.UpdatedOnDate = DateTime.Now;
            var isUpdated = _service.Update(CountryDetail);

            if (isUpdated != null && isUpdated > 0)
            {

                return Ok(CountryDetail);

            }
            else
            {

                return BadRequest("Kindly try again");
            }




        }

        #endregion

        #region DeleteCountry

        [HttpDelete("{id}")]
        [Produces(typeof(tblCountry))]
        public IActionResult DeleteCountry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Country = _service.SelectAll(new tblCountry() { CountryID = id }).FirstOrDefault();

            if (Country == null)
            {
                return NotFound();
            }

            Country.UpdatedBy = 0;
            Country.UpdatedOnDate = DateTime.Now;
            Country.IsDeleted = true;

            var isUpdated = _service.Update(Country);

            if (isUpdated != null && isUpdated > 0)
            {

                return Ok(Country);

            }
            else
            {

                return BadRequest("Kindly try again");
            }




        }

        #endregion

    }
}