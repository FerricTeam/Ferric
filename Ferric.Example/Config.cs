#pragma warning disable SA1401 // Fields should be private
#pragma warning disable SA1600 // Elements should be documented

namespace Example
{
    using Ferric.API.Features;

    /// <inheritdoc />
    public class Config : Ferric.API.Features.Config
    {
        public int IntValue = 1;
        public string TextValue = "value";
        public bool BoolValue = false;
        public float FloatValue = 3f;

        public Documented<bool> DocumentedFloatValue = new()
        {
            Description = "This is a description",
            Value = true,
        };
    }
}