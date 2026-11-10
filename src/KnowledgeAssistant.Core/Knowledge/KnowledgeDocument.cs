using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAssistant.Core.Knowledge
{
    public sealed record KnowledgeDocument(string Source, string Content);
}