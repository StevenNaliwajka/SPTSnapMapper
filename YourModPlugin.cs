using BepInEx.Configuration;
using BepInEx;

// Basic example to showcase generating a config file using BepInEx to modify a setting.
namespace SPTSnapMaper
{
​     [BepInPlugin("com.example.YourMod", "YourMod", "0.0.1")]
     public class YourModPlugin : BaseUnityPlugin
     {
          // Defines a Boolean Config Entry for ExampleOne
          private const string ConfigTypeACategory = "ConfigTypeA";
          internal static ConfigEntry<bool> ExampleOne;

          internal void Start()
          {
               // On Start Create Config File.
               InitConfiguration();
          }

          private void InitConfiguration()
          {
               // Add Properties for the setting.
               ExampleOne = Config.Bind(
               ConfigTypeACategory, // Config Category, Settings sharing the same name will be grouped.
               "Name Of Setting",   // Setting  Name
               true,                // Default Value
               "Description of setting, it changes this, that and the other."​); // Verbose Desciption
          }
     }
​}