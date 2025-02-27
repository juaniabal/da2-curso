﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ObligatorioDA2.Application.Contracts.WeatherForecasts;
using ObligatorioDA2.Application.Contracts.WeatherForecasts.Dtos;
using ObligatorioDA2.Domain.Exceptions;

namespace ObligatorioDA2.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IForecastService _forecastService;

        public WeatherForecastController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecastOutputDto>> ReadAll()
        {
            return Ok(_forecastService.ReadAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<WeatherForecastOutputDto> Read(int id)
        {
            WeatherForecastOutputDto forecast = _forecastService.Read(id);
            if (forecast == null)
            {
                return NotFound();
            }
            return Ok(forecast);
        }

        [HttpPost]
        public ActionResult<WeatherForecastOutputDto> Create([FromBody] WeatherForecastInputDto forecast)
        {
            WeatherForecastOutputDto createdForecast = _forecastService.Create(forecast);
            return Ok(createdForecast);
        }

        [HttpPut("{id:int}")]
        public ActionResult<WeatherForecastOutputDto> Update(int id, [FromBody] WeatherForecastInputDto forecast)
        {
            WeatherForecastOutputDto updatedForecast = _forecastService.Update(id, forecast);
            return Ok(updatedForecast);
        }

        [HttpDelete]
        public ActionResult Delete(int forecastId)
        {
            try
            {
                _forecastService.Delete(forecastId);
                return Ok();
            }
            catch (GuardClauseException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
