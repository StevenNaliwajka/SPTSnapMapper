using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



namespace SPTSnapMaper.DesignPatterns
{

     //Somehow not included in the base Freecam mod and to my understanding Singletons are not provided 
     //   By default in unity. I recreated a solution to enable building.
     public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
     {
          private static T _instance;

          public static T Instance
          {
               get
               {
                    if (_instance == null)
                    {
                         _instance = FindObjectOfType<T>();

                         if (_instance == null)
                         {
                              var singletonObject = new GameObject(typeof(T).Name);
                              _instance = singletonObject.AddComponent<T>();
                              DontDestroyOnLoad(singletonObject);
                         }
                    }

                    return _instance;
               }
          }

          protected virtual void Awake()
          {
               if (_instance == null)
               {
                    _instance = this as T;
                    DontDestroyOnLoad(gameObject);
               }
               else
               {
                    Destroy(gameObject);
               }
          }
     }
}
