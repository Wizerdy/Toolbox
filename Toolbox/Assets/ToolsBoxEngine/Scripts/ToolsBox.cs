﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolsBoxEngine {
    public enum Axis { X, Y, Z }

    #region Nullable vector
    // Nullable Vector
    //public class NVector2 {
    //    public Vector2? vector;

    //    //public static void operator =(NVector2 a, Vector2 b) => a.vector = b;
    //    public static implicit operator Vector2(NVector2 a) => (Vector2)a.vector;
    //    public static implicit operator NVector2(Vector2 a) => new NVector2((Vector2?)a);

    //    public NVector2() {
    //        vector = null;
    //    }

    //    public NVector2(Vector2? vector) {
    //        this.vector = vector;
    //    }

    //    public Vector2 Vector {
    //        get { return (Vector2)vector; }
    //        set { vector = value; }
    //    }

    //    public float x {
    //        get { return Vector.x; }
    //        set { Vector = new Vector2(value, Vector.y); }
    //    }

    //    public float y {
    //        get { return Vector.y; }
    //        set { Vector = new Vector2(Vector.x, value); }
    //    }
    //}
    #endregion

    #region Classes

    public class Nullable<T> where T : struct {
        public T? value;

        public static implicit operator T(Nullable<T> a) => (T)a.value;
        public static implicit operator Nullable<T>(T a) => new Nullable<T>((T?)a);

        public Nullable() {
            value = null;
        }

        public Nullable(T? value) {
            this.value = value;
        }

        public T Value {
            get { return (T)value; }
            set { this.value = value; }
        }
    }

    #endregion

    public static class Tools {

        #region Delegates

        public delegate void BasicDelegate();

        public delegate void BasicDelegate<T>(T arg);

        #endregion

        #region Extensions methods

        public static Vector2 To2D(this Vector3 vector, Axis axisToIgnore = Axis.Z) {
            switch (axisToIgnore) {
                case Axis.X:
                    return new Vector2(vector.y, vector.z);
                case Axis.Y:
                    return new Vector2(vector.x, vector.z);
                case Axis.Z:
                    return new Vector2(vector.x, vector.y);
                default:
                    return new Vector2(vector.x, vector.y);
            }
        }

        public static Vector3 To3D(this Vector2 vector, float value = 0f, Axis axis = Axis.Z) {
            switch (axis) {
                case Axis.X:
                    return new Vector3(value, vector.x, vector.y);
                case Axis.Y:
                    return new Vector3(vector.x, value, vector.y);
                case Axis.Z:
                    return new Vector3(vector.x, vector.y, value);
                default:
                    return new Vector3(vector.x, vector.y, value);
            }
        }

        public static Vector3 Override(this Vector3 vector, float value, Axis axis = Axis.Y) {
            switch (axis) {
                case Axis.X:
                    vector.x = value;
                    break;
                case Axis.Y:
                    vector.y = value;
                    break;
                case Axis.Z:
                    vector.z = value;
                    break;
                default:
                    vector.y = value;
                    break;
            }

            return vector;
        }

        public static Vector2 Override(this Vector2 vector, float value, Axis axis = Axis.Y) {
            switch (axis) {
                case Axis.X:
                    vector.x = value;
                    break;
                case Axis.Y:
                    vector.y = value;
                    break;
                case Axis.Z:
                    Debug.LogWarning("Can't override Vector2 z axis, using default axis : y");
                    vector.y = value;
                    break;
                default:
                    vector.y = value;
                    break;
            }

            return vector;
        }

        public static Vector2Int FloorToInt(this Vector2 vector) {
            return new Vector2Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y));
        }
        
        public static Vector2 Abs(this Vector2 vector) {
            return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }

        public static void Print<T>(this Nullable<T>[] array) where T : struct {
            Debug.Log("----------------");
            for(int i = 0; i < array.Length; i++) {
                Debug.Log(i + ". " + array[i].Value);
            }
        }

        #endregion

        #region Random

        public static int RandWeight(params int[] weight) {
            int totWeight = 0;
            for (int i = 0; i < weight.Length; i++) {
                totWeight += weight[i];
            }

            int rand = UnityEngine.Random.Range(0, totWeight);

            for (int i = 0; i < weight.Length; i++) {
                if (rand <= weight[i]) {
                    return i;
                }
                rand -= weight[i];
            }
            return -1;
        }

        #endregion
    }
}