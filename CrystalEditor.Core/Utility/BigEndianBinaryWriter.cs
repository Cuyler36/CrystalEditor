using System;
using System.IO;

namespace CrystalEditor.Core.Utility
{
    public class BigEndianBinaryWriter : BinaryWriter
    {
        public BigEndianBinaryWriter(Stream stream) : base(stream) { }

        public override void Write(short value) => base.Write(value.Reverse());
        public override void Write(ushort value) => base.Write(value.Reverse());
        public override void Write(int value) => base.Write(value.Reverse());
        public override void Write(uint value) => base.Write(value.Reverse());
        public override void Write(long value) => base.Write(value.Reverse());
        public override void Write(ulong value) => base.Write(value.Reverse());
    }
}
