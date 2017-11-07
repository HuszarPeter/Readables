using System;
using System.Linq;
using AppKit;

namespace Readables.Extensions
{
    public static class NSLayoutConstraintExtensions
    {
        public static void ActivateAll(this NSLayoutConstraint[] constraints) {
            constraints
                .ToList()
                .ForEach(x => x.Active = true);
        }
    }
}
