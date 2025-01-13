using BepInEx.Logging;
using System;
using System.Reflection;
using UnityEngine;

namespace SPTSnapMaper.DataAqusition
{
     public class ScreenshotCaller : MonoBehaviour
     {
          private Camera targetCamera;
          private Vector2 screenshotDimensions = new Vector2(1920f, 1080f); // Default resolution.

          // Use BepInEx logger
          private static ManualLogSource LogSource;

          private void Awake()
          {
               LogSource = BepInEx.Logging.Logger.CreateLogSource("ScreenshotCaller");
               LogSource.LogInfo("ScreenshotCaller initialized!");
          }

          private void Update()
          {
               // Log each update for testing (can be removed later).
               LogSource.LogDebug("Update is running.");

               // Trigger screenshot with the "P" key.
               if (Input.GetKeyDown(KeyCode.P))
               {
                    LogSource.LogDebug("Key P pressed. Attempting to take a screenshot.");
                    TakeScreenshot();
               }
          }

          private void TakeScreenshot()
          {
               if (targetCamera == null)
               {
                    LogSource.LogError("Target Camera is not assigned!");
                    return;
               }

               try
               {
                    // Create an instance of EditorScreenshoter
                    var editorScreenshoter = new EditorScreenshoter();

                    // Use Reflection to set private fields
                    SetPrivateField(editorScreenshoter, "_camera", targetCamera);
                    SetPrivateField(editorScreenshoter, "_textureDimensions", screenshotDimensions);

                    // Call the MakeScreenshotAndSave method
                    var method = typeof(EditorScreenshoter).GetMethod("MakeScreenshotAndSave", BindingFlags.Instance | BindingFlags.Public);
                    if (method != null)
                    {
                         method.Invoke(editorScreenshoter, null);
                         LogSource.LogInfo("Screenshot taken successfully.");
                    }
                    else
                    {
                         LogSource.LogError("MakeScreenshotAndSave method not found!");
                    }
               }
               catch (Exception ex)
               {
                    LogSource.LogError($"Error taking screenshot: {ex.Message}\n{ex.StackTrace}");
               }
          }

          private void SetPrivateField(object target, string fieldName, object value)
          {
               try
               {
                    var field = target.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                         field.SetValue(target, value);
                         LogSource.LogDebug($"Field '{fieldName}' set successfully.");
                    }
                    else
                    {
                         LogSource.LogError($"Field '{fieldName}' not found on {target.GetType().Name}.");
                    }
               }
               catch (Exception ex)
               {
                    LogSource.LogError($"Error setting field '{fieldName}': {ex.Message}\n{ex.StackTrace}");
               }
          }
     }
}
