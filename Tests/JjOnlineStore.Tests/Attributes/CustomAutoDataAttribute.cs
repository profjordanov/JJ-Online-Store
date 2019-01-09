using AutoFixture;
using AutoFixture.Xunit2;
using System;
using JjOnlineStore.Data.Entities;

namespace JjOnlineStore.Tests.Attributes
{
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute()
            : base(() => new Fixture().Customize(new Customization()))
        {
        }

        private class Customization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }
        }
    }
}