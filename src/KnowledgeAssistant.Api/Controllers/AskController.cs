using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KnowledgeAssistant.Api.Models;
using KnowledgeAssistant.Core.AI;

namespace KnowledgeAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AskController : ControllerBase
    {
        private readonly IChatService _chatService;

        public AskController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<ActionResult<AskResponse>> Ask(AskRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
            {
                return BadRequest("Question is required.");
            }

            var answer = await _chatService.GetResponseAsync(
                request.Question,
                cancellationToken);

            return Ok(new AskResponse(answer));
        }
    }
}