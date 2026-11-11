using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeAssistant.Core.Knowledge;
using KnowledgeAssistant.Infrastructure.Documents;

namespace KnowledgeAssistant.Tests.Documents
{
    public sealed class FixedSizeDocumentChunkerTests
    {
        [Fact]
        public void Chunk_WithOverlap_ReturnsExpectedChunks()
        {
            // Arrange
            var document = new KnowledgeDocument(
                Source: "test.md",
                Content: "ABCDEFGHIJ");

            var chunker = new FixedSizeDocumentChunker();

            // Act
            var chunks = chunker.Chunk(
                document,
                chunkSize: 4,
                overlap: 1);

            // Assert
            Assert.Equal(3, chunks.Count);

            Assert.Equal("ABCD", chunks[0].Content);
            Assert.Equal("DEFG", chunks[1].Content);
            Assert.Equal("GHIJ", chunks[2].Content);

            Assert.Equal(0, chunks[0].StartOffset);
            Assert.Equal(3, chunks[1].StartOffset);
            Assert.Equal(6, chunks[2].StartOffset);

            Assert.Equal("test.md", chunks[0].Source);
            Assert.Equal(0, chunks[0].Index);
            Assert.Equal(1, chunks[1].Index);
            Assert.Equal(2, chunks[2].Index);
        }

        [Fact]
        public void Chunk_EmptyDocument_ReturnsNoChunks()
        {
            // Arrange
            var document = new KnowledgeDocument(
                Source: "empty.md",
                Content: string.Empty);

            var chunker = new FixedSizeDocumentChunker();

            // Act
            var chunks = chunker.Chunk(document);

            // Assert
            Assert.Empty(chunks);
        }

        [Fact]
        public void Chunk_ShortFinalChunk_ReturnsRemainingContent()
        {
            // Arrange
            var document = new KnowledgeDocument(
                Source: "test.md",
                Content: "ABCDEFGHI");

            var chunker = new FixedSizeDocumentChunker();

            // Act
            var chunks = chunker.Chunk(
                document,
                chunkSize: 4,
                overlap: 1);

            // Assert
            Assert.Equal(3, chunks.Count);

            Assert.Equal("ABCD", chunks[0].Content);
            Assert.Equal("DEFG", chunks[1].Content);
            Assert.Equal("GHI", chunks[2].Content);

            Assert.Equal(6, chunks[2].StartOffset);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Chunk_InvalidChunkSize_ThrowsArgumentOutOfRangeException(
            int chunkSize)
        {
            // Arrange
            var document = new KnowledgeDocument(
                Source: "test.md",
                Content: "ABCDEFGHIJ");

            var chunker = new FixedSizeDocumentChunker();

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => chunker.Chunk(
                    document,
                    chunkSize: chunkSize,
                    overlap: 0));

            Assert.Equal("chunkSize", exception.ParamName);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(4)]
        [InlineData(5)]
        public void Chunk_InvalidOverlap_ThrowsArgumentOutOfRangeException(
            int overlap)
        {
            // Arrange
            var document = new KnowledgeDocument(
                Source: "test.md",
                Content: "ABCDEFGHIJ");

            var chunker = new FixedSizeDocumentChunker();

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => chunker.Chunk(
                    document,
                    chunkSize: 4,
                    overlap: overlap));

            Assert.Equal("overlap", exception.ParamName);
        }
    }
}