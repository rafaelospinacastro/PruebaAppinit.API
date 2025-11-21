using Microsoft.AspNetCore.Mvc;
using PruebaAppinit.Application.DTOs;
using PruebaAppinit.Application.Services;
using PruebaAppinit.Domain.Entities;
using System.Net;

namespace PruebaAppinit.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly GameService _serviceGame;
    private readonly ILogger<GamesController> _logger;
    public GamesController(GameService serviceGame, ILogger<GamesController> logger) 
    {
        _serviceGame = serviceGame;
        _logger = logger;
    } 

    [HttpPost("start")]
    public async Task<IActionResult> StartGame([FromBody] StartGameRequest startGameRequest, CancellationToken cancellationToken)
    {
        
        try
        {
            var objGame = await _serviceGame.StartGameAsync(startGameRequest, cancellationToken);
            if (objGame == null)
            {
                return BadRequest("No se pudo crear el juego");
            }
            else
            {
                return CreatedAtAction(nameof(GetGameStatusById), new { id = objGame.GameId }, objGame);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Se presento un error en el objeto GamesController metodo 'StartGame', fue el siguiente", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("play")]
    public async Task<IActionResult> PlayGame([FromBody] PlayRoundRequest playRoundRequest, CancellationToken cancellationToken)
    {      

        try
        {            
            var objGame = await _serviceGame.PlayRoundAsync(playRoundRequest, cancellationToken);
            if (objGame == null)
            {
                return NotFound("No se encontró la ronda del juego");
            }
            else
            {
                return Ok(objGame);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Se presento un error en el objeto GamesController metodo 'StartGame', fue el siguiente", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "GameStatusById")]
    public async Task<IActionResult> GetGameStatusById(Guid id, CancellationToken cancellationToken)
    {      
        try
        {
            var objGameStatus = await _serviceGame.GetStatusAsync(id, cancellationToken);
                if (objGameStatus == null)
            {
                return NotFound("El codigo del juego no existe");
            }
            else
            {
                return Ok(objGameStatus);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Se presento un error en el objeto GamesController metodo 'StartGame', fue el siguiente", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    
}
