using System;
using Foundation;

namespace Readables.Model
{
    public class OutLine: NSObject
    {
        public string Text;

        public Item[] Items;

    }

    public class Item: NSObject
    {
        public string Text;
    }
}
