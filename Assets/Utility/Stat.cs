using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//a class to keep track of modifiers to an int or float base value. Useful for stats that can be buffed or debuffed
public class Stat<T>
{
    readonly WrapperStat<IArithmatic> wrappedStat;
    readonly TypeCode type;
    public Stat(T firstValue)
    {
        type = Type.GetTypeCode(firstValue.GetType());
        wrappedStat = new WrapperStat<IArithmatic>(toIArithmatic(firstValue));
    }

    public T baseValue { get { return (T)(wrappedStat.baseValue); } }
    public T computedValue { get { return (T)(wrappedStat.computedValue); } }

    public int Add(T toAdd) { return wrappedStat.Add(toIArithmatic(toAdd)); }
    public int Multiply(T toMultiply) { return wrappedStat.Multiply(toIArithmatic(toMultiply)); }
    public int Divide(T toDivide) { return wrappedStat.Divide(toIArithmatic(toDivide)); }
    public bool Remove(int GUID) { return wrappedStat.Remove(GUID); }

    public static explicit operator T(Stat<T> castTarget)
    {
        return castTarget.computedValue;
    }

    IArithmatic toIArithmatic(T castTarget)
    {
        switch (type)
        {
            case TypeCode.Int32: //ints
                return new ArithmaticInt(castTarget as int? ?? 0);
            case TypeCode.Single: //floats
                return new ArithmaticFloat(castTarget as float? ?? 0);
            default:
                throw new ArgumentException("Stat only supports types int and float");
        }
    }

    class WrapperStat<Twrapped> where Twrapped : IArithmatic
    {
        List<StatModifier<Twrapped>> adders; //list of (ID, addr)
        List<StatModifier<Twrapped>> multipliers; //list of (ID, multipl)
        List<StatModifier<Twrapped>> dividers; //list of (ID, dividr)

        Twrapped _baseValue;
        public Twrapped baseValue { get { return _baseValue; } }
        Twrapped _computedValue;
        public Twrapped computedValue { get { return _computedValue; } }

        public WrapperStat(Twrapped firstValue)
        {
            _baseValue = firstValue;
            adders = new List<StatModifier<Twrapped>>();
            multipliers = new List<StatModifier<Twrapped>>();
            dividers = new List<StatModifier<Twrapped>>();
        }

        public int Add(Twrapped toAdd)
        {
            StatModifier<Twrapped> newAdder = new StatModifier<Twrapped>(toAdd);
            adders.Add(newAdder);
            return newAdder.ID();
        }

        public int Multiply(Twrapped toMultiply)
        {
            StatModifier<Twrapped> newMultiplier = new StatModifier<Twrapped>(toMultiply);
            multipliers.Add(newMultiplier);
            return newMultiplier.ID();
        }

        public int Divide(Twrapped toDivide)
        {
            StatModifier<Twrapped> newDivider = new StatModifier<Twrapped>(toDivide);
            dividers.Add(newDivider);
            return newDivider.ID();
        }

        public bool Remove(int GUID)
        {
            for (int i = 0; i < multipliers.Count; i++)
            {
                if (multipliers[i].ID() == GUID)
                {
                    multipliers.RemoveAt(i);
                    return true;
                }
            }
            for (int i = 0; i < adders.Count; i++)
            {
                if (adders[i].ID() == GUID)
                {
                    adders.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        private void updateComputedValue()
        {
            Twrapped result = _baseValue;
            foreach (StatModifier<Twrapped> modifier in adders)
            {
                result.Add(modifier.value()); //I have to use wrapper classes and interfaces because float and int don't share an arithmatic interface. interfaces can't do operator overloads

                //If I really wanted to use +, *, /, etc. in this situation, I could create an extension method that overloads the operators
            }
            foreach (StatModifier<Twrapped> modifier in multipliers)
            {
                result.Multiply(modifier.value());
            }
            foreach (StatModifier<Twrapped> modifier in dividers)
            {
                result.Divide(modifier.value());
            }
            _computedValue = result;
        }

        //casting
        public static explicit operator Twrapped(WrapperStat<Twrapped> castTarget)
        {
            return castTarget._computedValue;
        }

        protected class StatModifier<Tstat>
        {
            Tstat _value;

            static int maxGUID = 0;
            static int nextGUID() { return maxGUID++; }

            int _ID;
            public int ID() { return _ID; }

            public StatModifier(Tstat value)
            {
                this._ID = nextGUID();
                _value = value;
            }

            public Tstat value() { return _value; }
        }

        //type-checking workarounds and wrappers
    }

    interface IArithmatic
    {
        IArithmatic Add(IArithmatic other);
        IArithmatic Multiply(IArithmatic other);
        IArithmatic Divide(IArithmatic other);
    }

    class ArithmaticInt : IArithmatic
    {
        private readonly int wrappedInt;
        public ArithmaticInt(int i) { wrappedInt = i; }
        public static implicit operator ArithmaticInt(int i)
        {
            return new ArithmaticInt(i);
        }
        public static implicit operator int(ArithmaticInt i)
        {
            return i.wrappedInt;
        }
        public IArithmatic Add(IArithmatic other)
        {
            return (ArithmaticInt)(wrappedInt + (int)(ArithmaticInt)other);
        }
        public IArithmatic Multiply(IArithmatic other)
        {
            return (ArithmaticInt)(wrappedInt * (int)(ArithmaticInt)other);
        }
        public IArithmatic Divide(IArithmatic other)
        {
            return (ArithmaticInt)(wrappedInt / (int)(ArithmaticInt)other);
        }
    }

    class ArithmaticFloat : IArithmatic
    {
        private readonly float wrappedFloat;
        public ArithmaticFloat(float i) { wrappedFloat = i; }
        public static implicit operator ArithmaticFloat(float i)
        {
            return new ArithmaticFloat(i);
        }
        public static implicit operator float(ArithmaticFloat i)
        {
            return i.wrappedFloat;
        }
        public IArithmatic Add(IArithmatic other)
        {
            return (ArithmaticFloat)(wrappedFloat + (float)(ArithmaticFloat)other);
        }
        public IArithmatic Multiply(IArithmatic other)
        {
            return (ArithmaticFloat)(wrappedFloat * (float)(ArithmaticFloat)other);
        }
        public IArithmatic Divide(IArithmatic other)
        {
            return (ArithmaticFloat)(wrappedFloat / (float)(ArithmaticFloat)other);
        }
    }
}