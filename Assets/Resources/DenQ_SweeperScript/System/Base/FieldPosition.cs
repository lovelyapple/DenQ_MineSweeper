using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;

namespace DenQData
{
    public class FieldPosition
    {
        public uint posX;
        //{set { posX = 0; } get { return posX; }  }
        public uint posZ;
        //{set { posZ = 0; } get { return posZ; }  }
        public override string ToString()
        {
            return ("FieldPos( X " + posX + " Z " + posZ + " )");
        }
        public FieldPosition()
        {
        }
        public FieldPosition(uint x, uint z)
        {

            this.posX = (uint)Mathf.Max(x, 0);
            this.posZ = (uint)Mathf.Max(z, 0);
        }
        //Override Operations
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            FieldPosition p = obj as FieldPosition;

            if ((object)p == null)
            {
                return false;
            }

            return (this.posX == p.posX) && (this.posZ == p.posZ);
        }
        public bool Equals(FieldPosition p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return (this.posX == p.posX) && (this.posZ == p.posZ);
        }
        public override int GetHashCode()
        {
            return (posX * posZ).GetHashCode();
        }
        public static bool operator !=(FieldPosition x, FieldPosition y)
        {
            return !(x == y);
        }
        public static bool operator ==(FieldPosition x, FieldPosition y)
        {
            if ((object)x == null || (object)y == null)
            {
                return false;
            }

            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            return (x.posX == y.posX) && (x.posZ == y.posZ);
        }
        //helperfunctions
        ///Get the vector3 worldPsotion of this FieldPostion 
        public Vector3 GetWorldPostion()
        {
            return PosToWorld(this);
        }
        ///Get the uint PosCode of this FIeldPosition
        public uint GetPositionCode()
        {
            return PosToCode(this);
        }
        //HelpFunction Functions
        ///convert FieldPostion to WordPosition
        public static Vector3 PosToWorld(FieldPosition p)
        {
            return new Vector3(p.posX * ClientSettings.FieldBlockSize, 0.0f, p.posZ * ClientSettings.FieldBlockSize);
        }
        ///convert FieldPostion to Code
        public static uint PosToCode(FieldPosition p)
        {
            return (uint)(p.posZ * ClientSettings.MaxFieldSize + p.posX + ClientSettings.TopDigit);
        }
        ///Convert Code To Fieldposition
        public static FieldPosition CodeToPos(uint code)
        {
            var z = (uint)(code % ClientSettings.TopDigit / ClientSettings.MaxFieldSize);
            return new FieldPosition(z, code % ClientSettings.MaxFieldSize);
        }
        ///Convert WorldPostion to FieldPostion
        public static FieldPosition WorldToPos(Vector3 world)
        {
            return new FieldPosition((uint)(world.x / ClientSettings.FieldBlockSize), (uint)(world.z / ClientSettings.FieldBlockSize));
        }

        public static uint CoordinateToCode(uint x, uint z)
        {
            return z * ClientSettings.MaxFieldSize + x + ClientSettings.TopDigit;
        }
        public static FieldPosition CoordinateToPos(uint x, uint z)
        {
            var Code = CoordinateToCode(x, z);
            return CodeToPos(Code);
        }
    }

}