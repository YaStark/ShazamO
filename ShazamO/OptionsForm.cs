using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShazamO
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            try { numComparing_NSigm.Value = (Decimal)Options.Get.Comparing_NSigm; }
            catch { numComparing_NSigm.Value = (Decimal)Options.Default.Comparing_NSigm; }

            string psr = Options.Get.Converter_PreferredSampleRate.ToString();
            if (comConverter_PreferredSampleRate.Items.Contains(psr))
                comConverter_PreferredSampleRate.SelectedItem = psr;
            else
                comConverter_PreferredSampleRate.SelectedItem = Options.Default.Converter_PreferredSampleRate;


            listLogElement_IgnoredTags.Items.Clear();
            try
            {
                foreach (var item in Options.Get.LogElement_IgnoredTags)
                    listLogElement_IgnoredTags.Items.Add(item);
            }
            catch
            {
                foreach (var item in Options.Default.LogElement_IgnoredTags)
                    listLogElement_IgnoredTags.Items.Add(item);
            }

            try { numInfluenceArea_BorderDeviation.Value = Options.Get.InfluenceArea_BorderDeviation; }
            catch { numInfluenceArea_BorderDeviation.Value = Options.Default.InfluenceArea_BorderDeviation; }

            try { numSignature_SamplesPerSecond.Value = Options.Get.Signature_SamplesPerSecond; }
            catch { numSignature_SamplesPerSecond.Value = Options.Default.Signature_SamplesPerSecond; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonApply_Click(null, null);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            Options.Get.Comparing_NSigm = (float)numComparing_NSigm.Value;
            
            string psr = comConverter_PreferredSampleRate.SelectedItem as string;
            int ipsr = 0;
            Int32.TryParse(psr, out ipsr);
            Options.Get.Converter_PreferredSampleRate = ipsr;

            List<string> ignTags = new List<string>();
            foreach(var item in listLogElement_IgnoredTags.Items) ignTags.Add(item as string);
            Options.Get.LogElement_IgnoredTags = ignTags.Where(s => !String.IsNullOrEmpty(s)).ToArray();

            Options.Get.InfluenceArea_BorderDeviation = (int)numInfluenceArea_BorderDeviation.Value;

            Options.Get.Signature_SamplesPerSecond = (int)numSignature_SamplesPerSecond.Value;
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            numComparing_NSigm.Value = (Decimal)Options.Default.Comparing_NSigm; 

            comConverter_PreferredSampleRate.SelectedItem = Options.Default.Converter_PreferredSampleRate;

            listLogElement_IgnoredTags.Items.Clear();
            foreach (var item in Options.Default.LogElement_IgnoredTags)
                    listLogElement_IgnoredTags.Items.Add(item);

            numInfluenceArea_BorderDeviation.Value = Options.Default.InfluenceArea_BorderDeviation; 

            numSignature_SamplesPerSecond.Value = Options.Default.Signature_SamplesPerSecond;

            buttonApply_Click(null, null);
        }

        private void buttonLogElement_IgnoredTags_add_Click(object sender, EventArgs e)
        {
            string line = textLogElement_IgnoredTags.Text;
            if (listLogElement_IgnoredTags.Items.Contains(line)) return;
            if (String.IsNullOrWhiteSpace(line)) return;
            listLogElement_IgnoredTags.Items.Add(line);
        }

        private void buttonLogElement_IgnoredTags_remove_Click(object sender, EventArgs e)
        {
            if (listLogElement_IgnoredTags.SelectedItem != null)
            {
                listLogElement_IgnoredTags.Items.Remove(listLogElement_IgnoredTags.SelectedItem);
            }
        }

        private void listLogElement_IgnoredTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            textLogElement_IgnoredTags.Text = listLogElement_IgnoredTags.SelectedItem as string;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
