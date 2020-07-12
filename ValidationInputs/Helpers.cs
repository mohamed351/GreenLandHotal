﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsUtilities
{
   public static class Helpers
    {

        public static void EnableAllControls(GroupBox groupBox, Button button)
        {
            foreach (Control item in groupBox.Controls)
                item.Enabled = true;

            button.Enabled = true;
        }

        public static void DisableAllControls(GroupBox groupBox, Button button)
        {
            foreach (Control item in groupBox.Controls)
            {
                item.Enabled = false;
                item.Text = "";
            }

            button.Enabled = false;
        }
    }
}