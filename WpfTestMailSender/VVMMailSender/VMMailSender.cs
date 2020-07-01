using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfTestMailSender.VVMLogWindows;

namespace WpfTestMailSender
{
    class VMMailSender : INotifyPropertyChanged
    {
        private string log;
        public string Log
        {
            get { return log; }
            set
            {
                log = value;
                OpenLogWindow();
                OnPropertyChanged("Log");
            }
        }

        private void OpenLogWindow()
        {
            VMLogWindows vMLogWindows = new VMLogWindows(Log);
        }

        private Letter letter;
        public Letter Letter
        {
            get { return letter; }
            set
            {
                letter = value;
                OnPropertyChanged("Letter");
            }
        }
        private Settings settings;
        public Settings Settings { get { return settings; }
            set 
            {
                settings = value;
                OnPropertyChanged("Settings");
            } }

        public VMMailSender()
        {
            Letter = new Letter();
            Settings = new Settings();
        }

        #region Commands
        #region Send
        private MyCommands send;
        public MyCommands Send{
            get 
            {
                return send ?? (send = new MyCommands(SandLetter));
            } 
        }
        private void SandLetter(Object obj)
        {
            try
            {
                if (obj is System.Windows.Controls.PasswordBox)
                {
                    Settings.Password = ((System.Windows.Controls.PasswordBox)obj).Password;
                }
                EmailSendServiceClass.Send(Letter,Settings);
            }
            catch (Exception ex)
            {
                Log = ex.Message + Environment.NewLine + ex.InnerException;
            }
            
        }
        #endregion
        #region Save
        private MyCommands save;
        public MyCommands Save
        {
            get
            {
                return save ?? (save = new MyCommands(SaveMail));
            }
        }
        private void SaveMail(Object obj)
        {
            if (!(obj is string))
            {
                MessageBox.Show("Неверный тип параметра сохранения");
                return;
            }
            
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "json files (*.json)|*.json";
                saveFileDialog.DefaultExt = "json";
                string json;
                JsonSerializerSettings jsonSerializerSettings = 
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((string)obj == "Letter")
                    {
                        json = JsonConvert.SerializeObject(Letter, jsonSerializerSettings);
                    }
                    else if((string)obj == "Settings")
                    {
                        json = JsonConvert.SerializeObject(Settings, jsonSerializerSettings);
                    }
                    else
                    {
                        json = JsonConvert.SerializeObject(this, jsonSerializerSettings);
                    }                    
                    StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                    streamWriter.Write(json);
                    streamWriter.Close();
                }
            }
        }
        #endregion
        #region Load
        private MyCommands load;
        public MyCommands Load
        {
            get
            {
                return load ?? (load = new MyCommands(LoadMail));
            }
        }
        private void LoadMail(Object obj)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "json files (*.json)|*.json";
                openFileDialog.DefaultExt = "json";
                string json;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                    json = streamReader.ReadToEnd();
                    try
                    {
                        var objDes = JsonConvert.DeserializeObject(json, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });
                        if (objDes is Letter)
                        {
                            Letter = (Letter)objDes;
                        }
                        else if (objDes is Settings)
                        {
                            Settings = (Settings)objDes;
                        }
                        else
                        {
                            Letter = ((VMMailSender)objDes).Letter;
                            Settings = ((VMMailSender)objDes).Settings;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log = "Данный файл не подходит для загрузки настроек" + Environment.NewLine + 
                            ex.Message + Environment.NewLine + ex.InnerException;                        
                        openFileDialog.Dispose();
                    }              
                }
            }
        }
        #endregion
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
