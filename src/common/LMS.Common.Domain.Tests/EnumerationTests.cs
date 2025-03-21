﻿using System.Dynamic;

namespace LMS.Common.Domain.Tests;

public class EnumerationTests
{
    [Fact]
    public void TwoInstancesWithSameId_ShouldBe_Equal()
    {
        //Arrange
        TestEnumeration enum1 = TestEnumeration.Test1;
        TestEnumeration enum2 = TestEnumeration.Test1;

        //Assert
        Assert.True(enum1.Equals(enum2));
        Assert.True(enum1 == enum2);
    }

    [Fact]
    public void Enumerations_ShouldReturn_AllAvailableInstancesOfDerivedType()
    {
        //Arrange
        int expectedCount = 2;

        //Act
        IEnumerable<TestEnumeration> testEnumerations = Enumeration.GetAll<TestEnumeration>();

        //Assert
        Assert.Equal(expectedCount, testEnumerations.Count());

    }

    [Fact]
    public void FromId_ShouldReturn_CorrectInstanceOfDerivedType()
    {
        //Arrange
        int inputId = 1;
        TestEnumeration expected = TestEnumeration.Test1;

        //Act
        TestEnumeration actual = Enumeration.FromId<TestEnumeration>(inputId);

        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FromId_ShouldThrow_InvalidOperationException_ForIncorrectId()
    {
        //Arrange
        TestEnumeration expected;

        //Act
        Action action = () => { expected = Enumeration.FromId<TestEnumeration>(4); };

        //Assert
        Assert.Throws<InvalidOperationException>(action);

    }

    [Fact]
    public void FromName_ShouldReturn_CorrectInstanceOfDerivedType()
    {
        //Arrange
        string inputName = "Test1";
        TestEnumeration expected = TestEnumeration.Test1;

        //Act
        TestEnumeration actual = Enumeration.FromName<TestEnumeration>(inputName);

        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FromName_ShouldThrow_InvalidOperationException_ForIncorrectName()
    {
        //Arrange
        string inputName = "someValue";
        TestEnumeration enumeration;

        //Act
        Action action = () => { enumeration = Enumeration.FromName<TestEnumeration>(inputName); };

        //Assert
        Assert.Throws<InvalidOperationException>(action);
    }

    [Fact]
    public void Enumerations_ShouldBeComparable_UsingOperators()
    {
        TestEnumeration testEnumeration1 = TestEnumeration.Test1;
        TestEnumeration testEnumeration2 = TestEnumeration.Test2;

        Assert.True(testEnumeration2 > testEnumeration1);
        Assert.True(testEnumeration2 >= testEnumeration1);
        Assert.True(testEnumeration1 < testEnumeration2);
        Assert.True(testEnumeration1 <= testEnumeration2);
        Assert.True(testEnumeration1 != testEnumeration2);

    }

    [Fact]
    public void Enumerations_ShouldBeComparable_UsingCompareToFunction()
    {
        //Arrange
        TestEnumeration testEnumeration1 = TestEnumeration.Test1;
        TestEnumeration testEnumeration2 = TestEnumeration.Test1;

        //Assert
        Assert.Equal(0, testEnumeration1.CompareTo(testEnumeration2));
    }

    [Fact]
    public void Enumerations_ShouldFailEqualityComparison_BetweenDisparateTypes()
    {
        //Arrange
        TestEnumeration testEnumeration = TestEnumeration.Test1;
        ExpandoObject dynamicObject = new();

        //Assert
        Assert.False(testEnumeration.Equals(dynamicObject));
    }
}
