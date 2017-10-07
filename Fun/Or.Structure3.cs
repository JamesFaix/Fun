﻿using System;

namespace Fun
{
    public class Or<T1, T2, T3>
        : IEquatable<Or<T1, T2, T3>>
    {
        private readonly int _option;

        private readonly T1 _item1;

        private readonly T2 _item2;

        private readonly T3 _item3;

        internal Or(int option, T1 item1, T2 item2, T3 item3)
        {
            if (option < 1 || option > 3)
                throw new ArgumentOutOfRangeException(nameof(option), GetInvalidOptionErrorMessage(option));

            _option = option;
            _item1 = item1;
            _item2 = item2;
            _item3 = item3;
        }

        public int Option => _option;

        public T1 Item1 =>
            _option != 1
                ? throw new InvalidOperationException(GetInvalidItemErrorMessage(1))
                : _item1;

        public T2 Item2 =>
            _option != 2
                ? throw new InvalidOperationException(GetInvalidItemErrorMessage(2))
                : _item2;

        public T3 Item3 =>
            _option != 3
                ? throw new InvalidOperationException(GetInvalidItemErrorMessage(3))
                : _item3;

        #region Equality

        public bool Equals(Or<T1, T2, T3> other)
        {
            if (Equals(other, null)
                || _option != other._option)
            {
                return false;
            }

            switch (_option)
            {
                case 1:
                    return Equals(_item1, other._item1);
                case 2:
                    return Equals(_item2, other._item2);
                case 3:
                    return Equals(_item3, other._item3);
                default:
                    throw new InvalidOperationException(GetInvalidOptionErrorMessage(_option));
            }
        }

        public override bool Equals(object obj) =>
            Equals(obj as Or<T1, T2, T3>);

        public override int GetHashCode()
        {
            switch (_option)
            {
                case 1:
                    return _item1.GetHashCode();
                case 2:
                    return _item2.GetHashCode();
                case 3:
                    return _item3.GetHashCode();
                default:
                    throw new InvalidOperationException(GetInvalidOptionErrorMessage(_option));
            }
        }

        public static bool operator ==(Or<T1, T2, T3> a, Or<T1, T2, T3> b) =>
            Equals(a, null)
                ? Equals(b, null)
                : a.Equals(b);

        public static bool operator !=(Or<T1, T2, T3> a, Or<T1, T2, T3> b) =>
            !(a == b);

        #endregion

        public override string ToString()
        {
            switch (_option)
            {
                case 1:
                    return $"{_option}({_item1})";
                case 2:
                    return $"{_option}({_item2})";
                case 3:
                    return $"{_option}({_item3})";
                default:
                    throw new InvalidOperationException(GetInvalidOptionErrorMessage(_option));
            }
        }

        private static string GetInvalidItemErrorMessage(int number) =>
            $"Cannot get Item{number} from {nameof(Or<T1, T2, T3>)} unless {nameof(Option)} is {number}.";

        private static string GetInvalidOptionErrorMessage(int number) =>
            $"{nameof(Or<T1, T2, T3>)} cannot have an {nameof(Option)} of {number}.";
    }
}
