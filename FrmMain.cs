using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;

namespace DllInjector
{
    public partial class frmMain : Form
    {
        private struct ProcessInfo
        {
            public int Id;
            public string ImageKey;
            public Image Image;
            public ListViewItem ListViewItem;
        }

        private readonly IList<ProcessInfo> m_process = new List<ProcessInfo>();
        private readonly ListViewColumnSorter m_columnSorter = new ListViewColumnSorter();

        public frmMain()
        {
            InitializeComponent();

            this.m_columnSorter.Order = SortOrder.Ascending;
            this.m_columnSorter.SortColumn = 0;

            this.lsvProcesses.ListViewItemSorter = this.m_columnSorter;
        }

        private void RefreshProcesses()
        {
            lock (m_process)
            {
                var procs = Process.GetProcesses();
                Process proc;
                ProcessInfo st;
                int i;

                // Remove
                i = 0;
                while (i < this.m_process.Count)
                {
                    st = this.m_process[i];
                    if (procs.Select(e => e.Id).Contains(st.Id))
                        ++i;

                    else
                    {
                        this.lsvProcesses.Items.Remove(st.ListViewItem);

                        //this.imgIcon.Images.RemoveByKey(st.ImageKey);

                        st.Image?.Dispose();//【修改】

                        this.m_process.RemoveAt(i);

                    }
                }

                // Add
                string filename;
                for (i = 0; i < procs.Length; ++i)
                {
                    proc = procs[i];
                    using (proc)
                    {
                        if (this.m_process.Any(e => e.Id == proc.Id))
                            continue;

                        //try
                        //{
                        //    filename = proc.MainModule.FileName;
                        //}
                        //catch
                        //{
                        //    continue;
                        //}



                        filename = NativeMethods.GetProcessFileName(proc);

                        st = new ProcessInfo();

                        st.Id = proc.Id;

                        st.ListViewItem = new ListViewItem(NativeMethods.WhichPlatform(st.Id) + Path.GetFileName(filename));
                        st.ListViewItem.SubItems.Add(proc.Id.ToString());
                        st.ListViewItem.SubItems.Add(filename);
                        st.ListViewItem.ImageKey = filename;
                        st.ListViewItem.Tag = st.Id;

                        st.ImageKey = filename;

                        Icon icon;
                        //try
                        //{
                        //    icon = Icon.ExtractAssociatedIcon(filename);
                        //}
                        //catch
                        //{
                        //    continue;
                        //}
                        if (filename == string.Empty)
                            continue;
                        // =================================================
                        // 【修改 3】
                        //
                        // 只有 ImageList 中还没有这个 Icon 时，
                        // 才创建并添加。
                        //
                        // 如果已经存在：
                        //
                        //     不重新创建
                        //     不重新 Add
                        //     直接使用原来的 ImageKey
                        //
                        // 这样 ImageList 的 Index 就不会因为重复操作
                        // 而发生变化。
                        // =================================================

                        if (!this.imgIcon.Images.ContainsKey(filename))
                        {
                            icon = Icon.ExtractAssociatedIcon(filename);

                            using (icon)
                            {
                                st.Image = new Bitmap(
                                    22,
                                    22,
                                    PixelFormat.Format24bppRgb);

                                using (var g = Graphics.FromImage(st.Image))
                                {
                                    g.CompositingQuality =
                                        CompositingQuality.HighQuality;

                                    g.CompositingMode =
                                        CompositingMode.SourceOver;

                                    g.InterpolationMode =
                                        InterpolationMode.HighQualityBicubic;

                                    g.SmoothingMode =
                                        SmoothingMode.AntiAlias;

                                    g.Clear(this.imgIcon.TransparentColor);

                                    g.DrawIcon(
                                        icon,
                                        new Rectangle(3, 3, 16, 16));
                                }
                            }

                            // =================================================
                            // 【修改 4】
                            //
                            // 只有不存在时才 Add
                            // =================================================
                            this.imgIcon.Images.Add(filename, st.Image);
                        }
                        else
                        {
                            // =================================================
                            // 【修改 5】
                            //
                            // ImageList 已经有这个 Icon。
                            //
                            // 当前 st.Image 没有被创建，所以这里实际上
                            // 不需要做任何事情。
                            //
                            // 保留这个 else 只是为了逻辑清晰。
                            // =================================================
                        }

                        this.lsvProcesses.Items.Add(st.ListViewItem);


                        this.m_process.Add(st);
                    }
                }

                this.lsvProcesses.Sort();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.RefreshProcesses();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshProcesses();
        }

        private void btnDllSelect_Click(object sender, EventArgs e)
        {
            if (this.ofdDll.ShowDialog() == DialogResult.OK)
                this.txtDll.Text = this.ofdDll.FileName;
        }

        private void lsvProcesses_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == this.m_columnSorter.SortColumn)
            {
                if (this.m_columnSorter.Order == SortOrder.Ascending)
                    this.m_columnSorter.Order = SortOrder.Descending;
                else
                    this.m_columnSorter.Order = SortOrder.Ascending;
            }
            else
            {
                this.m_columnSorter.SortColumn = e.Column;
                this.m_columnSorter.Order = SortOrder.Ascending;
            }

            this.lsvProcesses.Sort();
        }

        private void windowFinder1_SelectedWindow(object sender, WindowFinderArgs e)
        {
            int pid;
            if (NativeMethods.GetWindowThreadProcessId(e.Handle, out pid) != 0)
            {
                lock (this.m_process)
                {
                    if (!this.m_process.Any(le => le.Id == pid))
                        this.RefreshProcesses();

                    if (!this.m_process.Any(le => le.Id == pid))
                        return;

                    var st = this.m_process.First(le => le.Id == pid);
                    st.ListViewItem.Selected = true;
                    st.ListViewItem.EnsureVisible();

                    this.lsvProcesses.Select();
                }
            }
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDll.Text))
            {
                MessageBox.Show(this, "Please select dll.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(this.txtDll.Text))
            {
                MessageBox.Show(this, "Dll is not existed.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.lsvProcesses.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select process.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (NativeMethods.Inject((int)this.lsvProcesses.SelectedItems[0].Tag, this.txtDll.Text))
                MessageBox.Show(this, "Success.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(this, "Fail.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void lbl_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "\"https://github.com/RyuaNerin/DllInjector\"" }).Dispose();
        }


    }
}
