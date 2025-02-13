using System.Text.Json;


namespace LightGun.UIControl
{
    internal class Master
    {

        private LightGunCompoment.LightGun lightGunP1;
        private LightGunCompoment.LightGun lightGunP2;

        private Settings settings;
        string jsonFilePath = ".\\setting.json";
        string jsonString;

        public Settings Settings {get {return settings;}}

        public MainTab mainTab;
        public ButtonAssignmentTab buttonAssignmentTab;
        public CalibrationTab calibrationTab;
        public Master()
        {
            //Load setting
            jsonString = File.ReadAllText(jsonFilePath);
            settings = JsonSerializer.Deserialize<Settings>(jsonString);

            lightGunP1 = new LightGunCompoment.LightGun(0,settings);
            lightGunP2 = new LightGunCompoment.LightGun(1,settings);
            ////////
            mainTab = new MainTab(lightGunP1,lightGunP2);
            buttonAssignmentTab = new ButtonAssignmentTab( lightGunP1, lightGunP2);
            calibrationTab = new CalibrationTab(lightGunP1, lightGunP2);
        }

        public void SaveSetting(object sender, EventArgs e)
        {
            // Serialize the object back to a JSON string
            string updatedJsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            // Write the JSON string to a file
            File.WriteAllText(jsonFilePath, updatedJsonString);

        }
    }
}
