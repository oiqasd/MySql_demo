using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class AppSettings
    {
        #region Instance
        static AppSettings _Instance;
        public static AppSettings Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new AppSettings();

                return _Instance;
            }
        }
        #endregion

        public string ConnectionString { get; set; }
    }
}
