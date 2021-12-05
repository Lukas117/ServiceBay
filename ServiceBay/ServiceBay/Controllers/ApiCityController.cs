﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceBay.Contracts;
using ServiceBay.Data;
using ServiceBay.Dto;
using ServiceBay.Models;

namespace ServiceBay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCityController : ControllerBase
    {
        private readonly ICityRepository _cityRepo;
        private readonly IMapper _mapper;

        public ApiCityController(ICityRepository cityRepo, IMapper mapper)
        {
            _cityRepo = cityRepo;
            _mapper = mapper;
        }

        // GET: api/ApiCity
        [HttpGet]
        public async Task<IEnumerable<City>> GetCity()
        {
            return await _cityRepo.GetCities();
        }

        // GET: api/ApiCity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(string zipcode)
        {
            var city = await _cityRepo.GetCity(zipcode);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/ApiCity/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCity(string id, City city)
        //{
        //    if (id != city.Zipcode)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(city).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CityExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/ApiCity
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CityForCreationDto>> PostCity(CityForCreationDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            await _cityRepo.CreateCity(city);
            return CreatedAtAction("GetCity", new { zipcode = city.Zipcode }, city);
        }

        // DELETE: api/ApiCity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(string zipcode)
        {
            var city = await _cityRepo.DeleteCity(zipcode);
            if (city == 0) { return NotFound(); }
            return NoContent();
        }
    }
}