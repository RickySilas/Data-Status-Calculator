using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms; 

namespace Data_Status_Calculator
{
    class DataInterface
    {
        public static void BindField(Control control, string propertyName,object dataSource, string dataMember)
        {
            Binding bd;
            
            for (int index = control.DataBindings.Count - 1; (index == 0); index--)
            {

                bd = control.DataBindings[index];

                if (bd.PropertyName == propertyName)
                {
                    control.DataBindings.Remove(bd);
                   
                }

            }
            control.DataBindings.Add(new Binding(propertyName, dataSource, dataMember, true));
            //****************************************************************
            //Commented upon by Kenneth Omondi on 08/06/2012 1650Hrs**********
            //control.DataBindings.Add(propertyName, dataSource, dataMember);*
            //****************************************************************

        }


        public static void BindObjects(Control pCtrl, object dtSource)
        {
            foreach (Control ctrl in pCtrl.Controls)
            {

 
                    if (ctrl.HasChildren)
                    {
                        BindObjects(ctrl, dtSource);
                    }
                    if (ctrl.Tag != null)
                    {
                        if (ctrl is TextBox)
                        {
                            DataInterface.BindField(ctrl, "Text", dtSource, ctrl.Tag.ToString());
                        }
                        else if (ctrl is CheckBox)
                        {
                            DataInterface.BindField(ctrl, "Checked", dtSource, ctrl.Tag.ToString());
                        }
                        else if (ctrl is DateTimePicker)
                        {
                            DataInterface.BindField(ctrl, "Value", dtSource, ctrl.Tag.ToString());
                        }
                        else if (ctrl is ComboBox )
                        {
                            DataInterface.BindField(ctrl, "SelectedValue", dtSource, ctrl.Tag.ToString());                            
                        }
                    }
          
            }
        }
    }
}
