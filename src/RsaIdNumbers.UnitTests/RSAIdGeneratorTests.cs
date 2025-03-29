using System;
using Xunit;
using RsaIdNumbers;

namespace RsaIdNumbers.UnitTests
{
    public class RSAIdGeneratorTests
    {
        [Fact]
        public void GenerateValidSAID_ShouldReturnValidId()
        {
            // Act
            string id = RSAIdGenerator.GenerateValidSAID();

            // Assert
            Assert.True(RSAIdValidator.IsValidSAID(id), $"Generated ID {id} is not valid.");
        }

        [Fact]
        public void GenerateValidSAID_WithMaxBirthDate_ShouldReturnValidId()
        {
            // Arrange
            DateTime maxBirthDate = new DateTime(2000, 12, 31);

            // Act
            string id = RSAIdGenerator.GenerateValidSAID(maxBirthDate);

            // Assert
            Assert.True(RSAIdValidator.IsValidSAID(id), $"Generated ID {id} is not valid.");
        }

        [Fact]
        public void GenerateValidSAID_WithSpecificParameters_ShouldReturnValidId()
        {
            // Arrange
            DateTime birthDate = new DateTime(1990, 5, 15);
            bool isFemale = true;
            bool isCitizen = true;

            // Act
            string id = RSAIdGenerator.GenerateValidSAID(birthDate, isFemale, isCitizen);

            // Assert
            Assert.True(RSAIdValidator.IsValidSAID(id), $"Generated ID {id} is not valid.");
        }

        [Fact]
        public void GenerateValidSAID_ShouldGenerateUniqueIds()
        {
            // Act
            string id1 = RSAIdGenerator.GenerateValidSAID();
            string id2 = RSAIdGenerator.GenerateValidSAID();

            // Assert
            Assert.NotEqual(id1, id2);
        }
    }
}