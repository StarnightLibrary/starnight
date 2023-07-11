namespace Starnight.Internal.Tests;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Starnight.Internal.Converters;

using Xunit;

public class JsonSerializerTests
{
    private const String testStringAllPresent =
        "{\"non_optional_property\":4,\"optional_property\":3,\"optional_array\":null}";

    private const String testStringNonePresent = "{\"non_optional_property\":4}";

    private const String testNullBooleanPresent = "{\"null_property\":null,\"real_property\":true}";

    private const String testNullBooleanMissing = "{\"real_property\":true}";

    private readonly OptionalContainingModel testModelAllPresent = new()
    {
        NonOptionalProperty = 4,
        OptionalProperty = 3,
        OptionalArray = null
    };

    private readonly OptionalContainingModel testModelNonePresent = new()
    {
        NonOptionalProperty = 4
    };

    [Fact]
    public void SerializeOptionalAllPresent()
    {
        String serialized = JsonSerializer.Serialize
        (
            this.testModelAllPresent,
            StarnightInternalConstants.DefaultSerializerOptions
        );

        Assert.Equal(testStringAllPresent, serialized);
    }

    [Fact]
    public void SerializeOptionalNonePresent()
    {
        String serialized = JsonSerializer.Serialize
        (
            this.testModelNonePresent,
            StarnightInternalConstants.DefaultSerializerOptions
        );

        Assert.Equal(testStringNonePresent, serialized);
    }

    [Fact]
    public void DeserializeOptionalAllPresent()
    {
        OptionalContainingModel deserialized = JsonSerializer.Deserialize<OptionalContainingModel>
        (
            testStringAllPresent,
            StarnightInternalConstants.DefaultSerializerOptions
        )!;

        Assert.Equal(this.testModelAllPresent, deserialized);
    }

    [Fact]
    public void DeserializeOptionalNonePresent()
    {
        OptionalContainingModel deserialized = JsonSerializer.Deserialize<OptionalContainingModel>
        (
            testStringNonePresent,
            StarnightInternalConstants.DefaultSerializerOptions
        )!;

        Assert.False(deserialized.OptionalProperty.HasValue);
        Assert.False(deserialized.OptionalArray.HasValue);

        Assert.Equal(this.testModelNonePresent, deserialized);
    }

    [Fact]
    public void DeserializeNullBooleanPresent()
    {
        NullBooleanModel deserialized = JsonSerializer.Deserialize<NullBooleanModel>
        (
            testNullBooleanPresent,
            StarnightInternalConstants.DefaultSerializerOptions
        )!;

        Assert.True(deserialized.NullProperty);
    }

    [Fact]
    public void DeserializeNullBooleanMissing()
    {
        NullBooleanModel deserialized = JsonSerializer.Deserialize<NullBooleanModel>
        (
            testNullBooleanMissing,
            StarnightInternalConstants.DefaultSerializerOptions
        )!;

        Assert.False(deserialized.NullProperty);
    }
}

internal sealed record class OptionalContainingModel
{
    public Int32 NonOptionalProperty { get; init; }

    public Optional<Int32> OptionalProperty { get; init; }

    public Optional<Int32[]?> OptionalArray { get; init; }
}

internal sealed record class NullBooleanModel
{
    [JsonConverter(typeof(NullBooleanJsonConverter))]
    public Boolean NullProperty { get; init; }

    public Boolean RealProperty { get; init; }
}
