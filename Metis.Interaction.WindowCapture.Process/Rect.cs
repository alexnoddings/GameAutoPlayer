using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Metis.Interaction.WindowCapture.Process
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Struct is not exposed publically.")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Struct is based on a native struct.")]
    internal struct Rect : IEquatable<Rect>
    {
        public Rect(Rect rectangle)
            : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        { }

        public Rect(int left, int top, int right, int bottom)
        {
            X = left;
            Y = top;
            Right = right;
            Bottom = bottom;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Left
        {
            get => X;
            set => X = value;
        }

        public int Top
        {
            get => Y;
            set => Y = value;
        }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public int Height
        {
            get => Bottom - Y;
            set => Bottom = value + Y;
        }

        public int Width
        {
            get => Right - X;
            set => Right = value + X;
        }

        public Point Location
        {
            get => new Point(Left, Top);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Size Size
        {
            get => new Size(Width, Height);
            set
            {
                Right = value.Width + X;
                Bottom = value.Height + Y;
            }
        }

        public static implicit operator Rectangle(Rect rectangle) =>
            new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);

        public static implicit operator Rect(Rectangle rectangle) =>
            new Rect(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

        public static bool operator ==(Rect rectangle1, Rect rectangle2) =>
             rectangle1.Equals(rectangle2);

        public static bool operator !=(Rect rectangle1, Rect rectangle2) =>
            !rectangle1.Equals(rectangle2);

        /// <inheritdoc/>
        public override string ToString() =>
            $"{{Left: {X}; Top: {Y}; Right: {Right}; Bottom: {Bottom}}}";

        /// <inheritdoc/>
        public override int GetHashCode() =>
            ToString().GetHashCode(StringComparison.Ordinal);

        public bool Equals(Rect other) =>
            other.Left == X
                   && other.Top == Y
                   && other.Right == Right
                   && other.Bottom == Bottom;

        /// <inheritdoc/>
        public override bool Equals(object obj) =>
            obj switch
            {
                Rect rectangle => Equals(rectangle),
                Rectangle rectangle => Equals(rectangle),
                _ => false
            };
    }
}
