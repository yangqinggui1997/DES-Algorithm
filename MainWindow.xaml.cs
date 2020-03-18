using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.ComponentModel;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Text;

using MahApps.Metro.Controls;
using MahApps;
    
namespace DES_Algorithm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        const int MONITOR_DEFAULTTONEAREST = 2;
       
        
        // To get a handle to the specified monitor

        [DllImport("user32.dll")]

        static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);



        // Rectangle (used by MONITORINFOEX)

        [StructLayout(LayoutKind.Sequential)]

        public struct RECT

        {

            public int Left;

            public int Top;

            public int Right;

            public int Bottom;

        }



        // Monitor information (used by GetMonitorInfo())

        [StructLayout(LayoutKind.Sequential)]

        public class MONITORINFOEX

        {

            public int cbSize;

            public RECT rcMonitor; // Total area

            public RECT rcWork; // Working area

            public int dwFlags;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]

            public char[] szDevice;

        }



        // To get the working area of the specified monitor

        [DllImport("user32.dll")]

        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX monitorInfo);


        private void Window1_SourceInitialized(object sender, EventArgs e)

        {

            // Make window borderless

            this.WindowStyle = WindowStyle.None;

            this.ResizeMode = ResizeMode.NoResize;



            // Get handle for nearest monitor to this window

            WindowInteropHelper wih = new WindowInteropHelper(this);

            IntPtr hMonitor = MonitorFromWindow(wih.Handle, MONITOR_DEFAULTTONEAREST);



            // Get monitor info

            MONITORINFOEX monitorInfo = new MONITORINFOEX();

            monitorInfo.cbSize = Marshal.SizeOf(monitorInfo);

            GetMonitorInfo(new HandleRef(this, hMonitor), monitorInfo);



            // Create working area dimensions, converted to DPI-independent values

            HwndSource source = HwndSource.FromHwnd(wih.Handle);

            if (source == null) return; // Should never be null

            if (source.CompositionTarget == null) return; // Should never be null

            Matrix matrix = source.CompositionTarget.TransformFromDevice;

            RECT workingArea = monitorInfo.rcWork;

            Point dpiIndependentSize =

                matrix.Transform(

                new Point(

                    workingArea.Right - workingArea.Left,

                    workingArea.Bottom - workingArea.Top));



            // Maximize the window to the device-independent working area ie

            // the area without the taskbar.

            // NOTE - window state must be set to Maximized as this adds certain

            // maximized behaviors eg you can't move a window while it is maximized,

            // such as by calling Window.DragMove

            this.Top = 0;

            this.Left = 0;

            this.MaxWidth = dpiIndependentSize.X;

            this.MaxHeight = dpiIndependentSize.Y;

            this.WindowState = WindowState.Maximized;

        }
        private double top, left;

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
            //hoặc
            //Environment.Exit(0);
        }

        private void btnmaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                Thickness mar = new Thickness(0);
                bor.BorderThickness = mar;
                bor.Margin = mar;
                btnmax.ToolTip = "Restore Down";
                top = Top;
                left = Left;
                Window1_SourceInitialized(sender, e);
            }
            else
            {
                Thickness mar = new Thickness(5);
                Thickness bors = new Thickness(1);
                bor.BorderThickness = bors;
                bor.Margin = mar;
                btnmax.ToolTip = "Maximize";
                WindowState = WindowState.Normal;
                ResizeMode = ResizeMode.CanResize;
                Top = top;
                Left = left;
            }

        }

        private void btnminimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //List Use for DES Algorithm
        //List use for Crypt File
        List<MetroTabItem> metroTabItems = new List<MetroTabItem>();
        List<MetroTabItem> metroTabItemsEncrypt = new List<MetroTabItem>();
        List<MetroTabItem> metroTabItemsDecrypt = new List<MetroTabItem>();
        List<TextBox> txtdic = new List<TextBox>();
        List<TextBox> txtfilecontent = new List<TextBox>();
        List<bool> isEncrypt = new List<bool>();
        List<bool> isDecrypt = new List<bool>();
        List<string> FileEnCryptDictory = new List<string>();
        List<string> FileDeCryptDictory = new List<string>();
        List<string> KeyEncrypt = new List<string>();
        List<string> KeyIVEncrypt = new List<string>();
        List<TextBox> txtencrypt = new List<TextBox>();
        List<TextBox> txtdecrypt = new List<TextBox>();

        //List use for Crypt Text
        List<MetroTabItem> metroTabItemsPara = new List<MetroTabItem>();
        List<MetroTabItem> metroTabItemsEncryptText = new List<MetroTabItem>();
        List<MetroTabItem> metroTabItemsDecryptText = new List<MetroTabItem>();
        List<TextBox> txtpara = new List<TextBox>();
        List<bool> TextisEncrypt = new List<bool>();
        List<bool> TextisDecrypt = new List<bool>();
        List<string> KeyEncryptText = new List<string>();
        List<string> KeyIVEncryptText = new List<string>();
        List<TextBox> txtencryptText = new List<TextBox>();
        List<TextBox> txtdecryptText = new List<TextBox>();
        //End list

        //List Use for 3DES Algorithm
        //List use for Crypt File
        List<MetroTabItem> metroTabItems3DES = new List<MetroTabItem>();
        List<MetroTabItem> metroTabItemsEncrypt3DES = new List<MetroTabItem>();
        List<MetroTabItem> metroTabItemsDecrypt3DES = new List<MetroTabItem>();
        List<TextBox> txtdic3DES = new List<TextBox>();
        List<TextBox> txtfilecontent3DES = new List<TextBox>();
        List<bool> isEncrypt3DES = new List<bool>();
        List<bool> isDecrypt3DES = new List<bool>();
        List<string> FileEnCryptDictory3DES = new List<string>();
        List<string> FileDeCryptDictory3DES = new List<string>();
        List<string> KeyEncrypt3DES = new List<string>();
        List<string> KeyIVEncrypt3DES = new List<string>();
        List<TextBox> txtencrypt3DES = new List<TextBox>();
        List<TextBox> txtdecrypt3DES = new List<TextBox>();

        //List use for Crypt Text
        List<MetroTabItem> metroTabItemsPara3DES = new List<MetroTabItem>();
        List<MetroTabItem> metroTabItemsEncryptText3DES = new List<MetroTabItem>();
        List<MetroTabItem> metroTabItemsDecryptText3DES = new List<MetroTabItem>();
        List<TextBox> txtpara3DES = new List<TextBox>();
        List<bool> TextisEncrypt3DES = new List<bool>();
        List<bool> TextisDecrypt3DES = new List<bool>();
        List<string> KeyEncryptText3DES = new List<string>();
        List<string> KeyIVEncryptText3DES = new List<string>();
        List<TextBox> txtencryptText3DES = new List<TextBox>();
        List<TextBox> txtdecryptText3DES = new List<TextBox>();
        //End list

        int indextabparagraph = 0;
        private object TabContentEncryptText(bool loaiCrypt, int index, bool loaimahoa)
        {
            //Add textbox text encrypt
            TextBox txten = new TextBox();
            txten.Style = FindResource("TextBoxStyle1") as Style;
            txten.Text = "";
            txten.IsReadOnly = true;
            txten.TextWrapping = TextWrapping.Wrap;
            txten.AcceptsReturn = true;
            txten.FontWeight = FontWeights.Normal;
            txten.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            txten.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txten.FontSize = 12;
            txten.Padding = new Thickness(3);

            //Set metroTabItem Encrypt property to add
            MetroTabItem metroTabEncrypt = new MetroTabItem();

            Color col = (Color)ColorConverter.ConvertFromString("#FF007ACC");
            SolidColorBrush colorBrush = new SolidColorBrush(col);
            metroTabEncrypt.Background = colorBrush;
            metroTabEncrypt.Foreground = Brushes.White;
            Thickness thickness = new Thickness(0, 0, 1, 0);
            metroTabEncrypt.BorderThickness = thickness;
            metroTabEncrypt.BorderBrush = Brushes.LightSlateGray;
            metroTabEncrypt.FontWeight = FontWeights.Bold;
            if (loaimahoa)//Chọn mã hoá DES
            {
                //Crypt File
                if (loaiCrypt)//Crypt File
                {
                    metroTabEncrypt.Header = "File " + index;
                    //Add grid contans all control that it uses into metrotabitemsencrypt
                    metroTabEncrypt.Content = txten;
                    //Add TextBox encrypt text into list TextBox file encypt
                    txtencrypt.Add(txten);
                    //add metroTabItem into list metrotabitemEncrypt
                    metroTabItemsEncrypt.Add(metroTabEncrypt);
                }
                else//Crypt Text
                {
                    metroTabEncrypt.Header = "Text " + index;
                    //Add grid contans all control that it uses into metrotabitemsencrypt
                    metroTabEncrypt.Content = txten;
                    //Add TextBox encrypt text into list TextBox text encypt
                    txtencryptText.Add(txten);
                    //add metroTabItem into list metrotabitemEncryptText
                    metroTabItemsEncryptText.Add(metroTabEncrypt);
                }
            }
            else
            {
                //Crypt File
                if (loaiCrypt)//Crypt File
                {
                    metroTabEncrypt.Header = "File " + index;
                    //Add grid contans all control that it uses into metrotabitemsencrypt
                    metroTabEncrypt.Content = txten;
                    //Add TextBox encrypt text into list TextBox file encypt
                    txtencrypt3DES.Add(txten);
                    //add metroTabItem into list metrotabitemEncrypt
                    metroTabItemsEncrypt3DES.Add(metroTabEncrypt);
                }
                else//Crypt Text
                {
                    metroTabEncrypt.Header = "Text " + index;
                    //Add grid contans all control that it uses into metrotabitemsencrypt
                    metroTabEncrypt.Content = txten;
                    //Add TextBox encrypt text into list TextBox text encypt
                    txtencryptText3DES.Add(txten);
                    //add metroTabItem into list metrotabitemEncryptText
                    metroTabItemsEncryptText3DES.Add(metroTabEncrypt);
                }
            }

            return metroTabEncrypt as object;
        }

        private object TabContentParagraph(bool loaimahoa)
        {
            //Add textbox paragraph
            TextBox txtparagraph = new TextBox();
            txtparagraph.Style = FindResource("TextBoxStyle1") as Style;
            txtparagraph.Text = "";
            txtparagraph.TextWrapping = TextWrapping.Wrap;
            txtparagraph.AcceptsReturn = true;
            txtparagraph.FontWeight = FontWeights.Normal;
            txtparagraph.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            txtparagraph.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtparagraph.FontSize = 12;
            txtparagraph.Padding = new Thickness(3);

            //---------------------------------------------------------//

            //Set metroTabItem Encrypt property to add
            MetroTabItem metroTabPara = new MetroTabItem();
            Color col = (Color)ColorConverter.ConvertFromString("#FF007ACC");
            SolidColorBrush colorBrush = new SolidColorBrush(col);
            metroTabPara.Background = colorBrush;
            metroTabPara.Foreground = Brushes.White;
            Thickness thickness = new Thickness(0, 0, 1, 0);
            metroTabPara.BorderThickness = thickness;
            metroTabPara.BorderBrush = Brushes.LightSlateGray;
            metroTabPara.FontWeight = FontWeights.Bold;
            metroTabPara.ContextMenu = new ContextMenu();

            MenuItem menuItem = new MenuItem();

            if (loaimahoa)//Chọn mã hoá DES
            {
                metroTabPara.Header = "Text " + indextabparagraph;
                //Add TextBox Dictory into list TextBox paragraph
                txtpara.Add(txtparagraph);

                menuItem.Header = "Close This";
                menuItem.Click += delegate
                {
                    tabparagraph.Items.Remove(metroTabPara);
                    //if (indextabparagraph > 1)
                    //{
                    //    indextabparagraph--;
                    //}
                    metroTabItemsPara.Remove(metroTabPara);
                    txtpara.Remove(txtparagraph);
                    for (int i = 0; i < metroTabItemsEncryptText.Count; ++i)
                    {
                        if (metroTabItemsEncryptText[i].Header.ToString().Trim() == metroTabPara.Header.ToString().Trim())
                        {
                            tabencrypt.Items.Remove(metroTabItemsEncryptText[i]);
                            metroTabItemsEncryptText.RemoveAt(i);
                            TextisEncrypt.RemoveAt(i);
                            txtencryptText.RemoveAt(i);
                            tabdecrypt.Items.Remove(metroTabItemsDecryptText[i]);
                            metroTabItemsDecryptText.RemoveAt(i);
                            TextisDecrypt.RemoveAt(i);
                            txtdecryptText.RemoveAt(i);
                            KeyEncryptText.RemoveAt(i);
                            KeyIVEncryptText.RemoveAt(i);
                            break;
                        }
                    }
                    if (tabparagraph.Items.Count == 0)
                    {
                        indextabparagraph = 0;//Reset index apply to use to create tabparagraph
                    }

                };
                MenuItem menuItem1 = new MenuItem();
                menuItem1.Header = "Close All But This";
                menuItem1.Click += delegate
                {
                    while (tabparagraph.Items.Count > 1)
                    {
                        for (int j = 0; j < tabparagraph.Items.Count; ++j)
                        {
                            if (tabparagraph.Items[j] != metroTabPara)
                            {
                                tabparagraph.Items.Remove(tabparagraph.Items[j]);
                                metroTabItemsPara.RemoveAt(j);
                                txtpara.RemoveAt(j);
                                for (int i = 0; i < metroTabItemsEncryptText.Count; ++i)
                                {
                                    if (metroTabItemsEncryptText[i].Header.ToString().Trim() != metroTabPara.Header.ToString().Trim())
                                    {
                                        tabencrypt.Items.Remove(metroTabItemsEncryptText[i]);
                                        metroTabItemsEncryptText.RemoveAt(i);
                                        TextisEncrypt.RemoveAt(i);
                                        txtencryptText.RemoveAt(i);
                                        tabdecrypt.Items.Remove(metroTabItemsDecryptText[i]);
                                        metroTabItemsDecryptText.RemoveAt(i);
                                        TextisDecrypt.RemoveAt(i);
                                        txtdecryptText.RemoveAt(i);
                                        KeyEncryptText.RemoveAt(i);
                                        KeyIVEncryptText.RemoveAt(i);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                };
                MenuItem menuItem2 = new MenuItem();
                menuItem2.Header = "Close Left This";
                menuItem2.Click += delegate
                {
                    for (int j = 0; tabparagraph.Items[j] != metroTabPara;)
                    {
                        tabparagraph.Items.Remove(tabparagraph.Items[j]);
                        //if (indextabparagraph > 1)
                        //{
                        //    indextabparagraph--;
                        //}
                        metroTabItemsPara.RemoveAt(j);
                        txtpara.RemoveAt(j);
                        for (int i = 0; i < metroTabItemsEncryptText.Count; ++i)
                        {
                            if (metroTabItemsEncryptText[i].Header.ToString().Trim() != metroTabPara.Header.ToString().Trim())
                            {
                                tabencrypt.Items.Remove(metroTabItemsEncryptText[i]);
                                metroTabItemsEncryptText.RemoveAt(i);
                                TextisEncrypt.RemoveAt(i);
                                txtencryptText.RemoveAt(i);
                                tabdecrypt.Items.Remove(metroTabItemsDecryptText[i]);
                                metroTabItemsDecryptText.RemoveAt(i);
                                TextisDecrypt.RemoveAt(i);
                                txtdecryptText.RemoveAt(i);
                                KeyEncryptText.RemoveAt(i);
                                KeyIVEncryptText.RemoveAt(i);
                                break;
                            }
                        }
                    }
                };
                MenuItem menuItem3 = new MenuItem();
                menuItem3.Header = "Close Right This";
                menuItem3.Click += delegate
                {
                    for (int j = tabparagraph.Items.Count; tabparagraph.Items[--j] != metroTabPara;)
                    {
                        tabparagraph.Items.Remove(tabparagraph.Items[j]);
                        //if (indextabparagraph > 1)
                        //{
                        //    indextabparagraph--;
                        //}
                        metroTabItemsPara.RemoveAt(j);
                        txtpara.RemoveAt(j);
                        for (int i = metroTabItemsEncryptText.Count - 1; i >= 0; --i)
                        {
                            if (metroTabItemsEncryptText[i].Header.ToString().Trim() != metroTabPara.Header.ToString().Trim())
                            {
                                tabencrypt.Items.Remove(metroTabItemsEncryptText[i]);
                                metroTabItemsEncryptText.RemoveAt(i);
                                TextisEncrypt.RemoveAt(i);
                                txtencryptText.RemoveAt(i);
                                tabdecrypt.Items.Remove(metroTabItemsDecryptText[i]);
                                metroTabItemsDecryptText.RemoveAt(i);
                                TextisDecrypt.RemoveAt(i);
                                txtdecryptText.RemoveAt(i);
                                KeyEncryptText.RemoveAt(i);
                                KeyIVEncryptText.RemoveAt(i);
                                break;
                            }
                        }
                    }
                };
                metroTabPara.ContextMenu.Items.Add(menuItem);
                metroTabPara.ContextMenu.Items.Add(menuItem1);
                metroTabPara.ContextMenu.Items.Add(menuItem2);
                metroTabPara.ContextMenu.Items.Add(menuItem3);
                //Add grid contans all control that it uses into metrotabitemsencrypt
                metroTabPara.Content = txtparagraph;

                //add metroTabItem into list metrotabitemEncrypt
                metroTabItemsPara.Add(metroTabPara);

                //Add value for items in list TextisEncrypt and TextisDecrypt
                TextisEncrypt.Add(false);
                TextisDecrypt.Add(false);

                //add value for items in list KeyEncryptText and KeyIVText
                KeyEncryptText.Add(null);
                KeyIVEncryptText.Add(null);
            }
            else
            {
                metroTabPara.Header = "Text " + indextabparagraph3DES;
                //Add TextBox Dictory into list TextBox paragraph
                txtpara3DES.Add(txtparagraph);

                menuItem.Header = "Close This";
                menuItem.Click += delegate
                {
                    tabparagraph3DES.Items.Remove(metroTabPara);
                    metroTabItemsPara3DES.Remove(metroTabPara);
                    txtpara3DES.Remove(txtparagraph);
                    for (int i = 0; i < metroTabItemsEncryptText3DES.Count; ++i)
                    {
                        if (metroTabItemsEncryptText3DES[i].Header.ToString().Trim() == metroTabPara.Header.ToString().Trim())
                        {
                            tabencrypt3DES.Items.Remove(metroTabItemsEncryptText3DES[i]);
                            metroTabItemsEncryptText3DES.RemoveAt(i);
                            TextisEncrypt3DES.RemoveAt(i);
                            txtencryptText3DES.RemoveAt(i);
                            tabdecrypt3DES.Items.Remove(metroTabItemsDecryptText3DES[i]);
                            metroTabItemsDecryptText3DES.RemoveAt(i);
                            TextisDecrypt3DES.RemoveAt(i);
                            txtdecryptText3DES.RemoveAt(i);
                            KeyEncryptText3DES.RemoveAt(i);
                            KeyIVEncryptText3DES.RemoveAt(i);
                            break;
                        }
                    }
                    if (tabparagraph3DES.Items.Count == 0)
                    {
                        indextabparagraph3DES = 0;//Reset index apply to use to create tabparagraph
                    }

                };
                MenuItem menuItem1 = new MenuItem();
                menuItem1.Header = "Close All But This";
                menuItem1.Click += delegate
                {
                    while (tabparagraph3DES.Items.Count > 1)
                    {
                        for (int j = 0; j < tabparagraph3DES.Items.Count; ++j)
                        {
                            if (tabparagraph3DES.Items[j] != metroTabPara)
                            {
                                tabparagraph3DES.Items.Remove(tabparagraph3DES.Items[j]);
                                metroTabItemsPara3DES.RemoveAt(j);
                                txtpara3DES.RemoveAt(j);
                                for (int i = 0; i < metroTabItemsEncryptText3DES.Count; ++i)
                                {
                                    if (metroTabItemsEncryptText3DES[i].Header.ToString().Trim() != metroTabPara.Header.ToString().Trim())
                                    {
                                        tabencrypt3DES.Items.Remove(metroTabItemsEncryptText3DES[i]);
                                        metroTabItemsEncryptText3DES.RemoveAt(i);
                                        TextisEncrypt3DES.RemoveAt(i);
                                        txtencryptText3DES.RemoveAt(i);
                                        tabdecrypt3DES.Items.Remove(metroTabItemsDecryptText3DES[i]);
                                        metroTabItemsDecryptText3DES.RemoveAt(i);
                                        TextisDecrypt3DES.RemoveAt(i);
                                        txtdecryptText3DES.RemoveAt(i);
                                        KeyEncryptText3DES.RemoveAt(i);
                                        KeyIVEncryptText3DES.RemoveAt(i);
                                        break;
                                    }
                                }
                            }
                        }

                    }
                };
                MenuItem menuItem2 = new MenuItem();
                menuItem2.Header = "Close Left This";
                menuItem2.Click += delegate
                {
                    for (int j = 0; tabparagraph3DES.Items[j] != metroTabPara;)
                    {
                        tabparagraph3DES.Items.Remove(tabparagraph3DES.Items[j]);
                        metroTabItemsPara3DES.RemoveAt(j);
                        txtpara3DES.RemoveAt(j);
                        for (int i = 0; i < metroTabItemsEncryptText3DES.Count; ++i)
                        {
                            if (metroTabItemsEncryptText3DES[i].Header.ToString().Trim() != metroTabPara.Header.ToString().Trim())
                            {
                                tabencrypt3DES.Items.Remove(metroTabItemsEncryptText3DES[i]);
                                metroTabItemsEncryptText3DES.RemoveAt(i);
                                TextisEncrypt3DES.RemoveAt(i);
                                txtencryptText3DES.RemoveAt(i);
                                tabdecrypt3DES.Items.Remove(metroTabItemsDecryptText3DES[i]);
                                metroTabItemsDecryptText3DES.RemoveAt(i);
                                TextisDecrypt3DES.RemoveAt(i);
                                txtdecryptText3DES.RemoveAt(i);
                                KeyEncryptText3DES.RemoveAt(i);
                                KeyIVEncryptText3DES.RemoveAt(i);
                                break;
                            }
                        }
                    }
                };
                MenuItem menuItem3 = new MenuItem();
                menuItem3.Header = "Close Right This";
                menuItem3.Click += delegate
                {
                    for (int j = tabparagraph3DES.Items.Count; tabparagraph3DES.Items[--j] != metroTabPara;)
                    {
                        tabparagraph3DES.Items.Remove(tabparagraph3DES.Items[j]);
                        metroTabItemsPara3DES.RemoveAt(j);
                        txtpara3DES.RemoveAt(j);
                        for (int i = metroTabItemsEncryptText3DES.Count - 1; i >= 0; --i)
                        {
                            if (metroTabItemsEncryptText3DES[i].Header.ToString().Trim() != metroTabPara.Header.ToString().Trim())
                            {
                                tabencrypt3DES.Items.Remove(metroTabItemsEncryptText3DES[i]);
                                metroTabItemsEncryptText3DES.RemoveAt(i);
                                TextisEncrypt3DES.RemoveAt(i);
                                txtencryptText3DES.RemoveAt(i);
                                tabdecrypt3DES.Items.Remove(metroTabItemsDecryptText3DES[i]);
                                metroTabItemsDecryptText3DES.RemoveAt(i);
                                TextisDecrypt3DES.RemoveAt(i);
                                txtdecryptText3DES.RemoveAt(i);
                                KeyEncryptText3DES.RemoveAt(i);
                                KeyIVEncryptText3DES.RemoveAt(i);
                                break;
                            }
                        }
                    }
                };
                metroTabPara.ContextMenu.Items.Add(menuItem);
                metroTabPara.ContextMenu.Items.Add(menuItem1);
                metroTabPara.ContextMenu.Items.Add(menuItem2);
                metroTabPara.ContextMenu.Items.Add(menuItem3);
                //Add grid contans all control that it uses into metrotabitemsencrypt
                metroTabPara.Content = txtparagraph;

                //add metroTabItem into list metrotabitemEncrypt
                metroTabItemsPara3DES.Add(metroTabPara);

                //Add value for items in list TextisEncrypt and TextisDecrypt
                TextisEncrypt3DES.Add(false);
                TextisDecrypt3DES.Add(false);

                //add value for items in list KeyEncryptText and KeyIVText
                KeyEncryptText3DES.Add(null);
                KeyIVEncryptText3DES.Add(null);
            }

            return metroTabPara as object;
        }

        private object TabContentDecryptText(bool loaiCrypt, int index, bool loaimahoa)
        {
            //Add textbox text encrypt
            TextBox txtde = new TextBox();
            txtde.Style = FindResource("TextBoxStyle1") as Style;
            txtde.Text = "";
            txtde.IsReadOnly = true;
            txtde.TextWrapping = TextWrapping.Wrap;
            txtde.AcceptsReturn = true;
            txtde.FontWeight = FontWeights.Normal;
            txtde.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            txtde.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtde.FontSize = 12;
            txtde.Padding = new Thickness(3);

            //Set metroTabItem Encrypt property to add
            MetroTabItem metroTabDecrypt = new MetroTabItem();

            Color col = (Color)ColorConverter.ConvertFromString("#FF007ACC");
            SolidColorBrush colorBrush = new SolidColorBrush(col);
            metroTabDecrypt.Background = colorBrush;
            metroTabDecrypt.Foreground = Brushes.White;
            Thickness thickness = new Thickness(0, 0, 1, 0);
            metroTabDecrypt.BorderThickness = thickness;
            metroTabDecrypt.BorderBrush = Brushes.LightSlateGray;
            metroTabDecrypt.FontWeight = FontWeights.Bold;
            if (loaimahoa)//Chọn mã hoá DES
            {
                if (loaiCrypt)//Crypt File
                {
                    metroTabDecrypt.Header = "File " + index;
                    //Add grid contans all control that it uses into metrotabitemsencrypt
                    metroTabDecrypt.Content = txtde;
                    //Add TextBox encrypt text into list TextBox file encypt
                    txtdecrypt.Add(txtde);
                    //add metroTabItem into list metrotabitemdecrypt
                    metroTabItemsDecrypt.Add(metroTabDecrypt);
                }
                else//Crypt Text
                {
                    metroTabDecrypt.Header = "Text " + index;
                    //Add grid contans all control that it uses into metrotabitemsencrypt
                    metroTabDecrypt.Content = txtde;
                    //Add TextBox encrypt text into list TextBox text encypt
                    txtdecryptText.Add(txtde);
                    //add metroTabItem into list metrotabitemdecrypt
                    metroTabItemsDecryptText.Add(metroTabDecrypt);
                }
            }
            else
            {
                if (loaiCrypt)//Crypt File
                {
                    metroTabDecrypt.Header = "File " + index;
                    //Add grid contans all control that it uses into metrotabitemsencrypt
                    metroTabDecrypt.Content = txtde;
                    //Add TextBox encrypt text into list TextBox file encypt
                    txtdecrypt3DES.Add(txtde);
                    //add metroTabItem into list metrotabitemdecrypt
                    metroTabItemsDecrypt3DES.Add(metroTabDecrypt);
                }
                else//Crypt Text
                {
                    metroTabDecrypt.Header = "Text " + index;
                    //Add grid contans all control that it uses into metrotabitemsencrypt
                    metroTabDecrypt.Content = txtde;
                    //Add TextBox encrypt text into list TextBox text encypt
                    txtdecryptText3DES.Add(txtde);
                    //add metroTabItem into list metrotabitemdecrypt
                    metroTabItemsDecryptText3DES.Add(metroTabDecrypt);
                }

            }

            return metroTabDecrypt as object;
        }

        private object metroTabitem(bool loaimahoa)
        {
            //Grid contains GroupBox
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(55) });
            grid.RowDefinitions.Add(new RowDefinition());

            //GroupBox Contains TextBox 
            GroupBox groupBox = new GroupBox();
            groupBox.Header = "Đường dẫn file";
            groupBox.Foreground = Brushes.White;
            groupBox.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            groupBox.SetValue(TextOptions.TextRenderingModeProperty, TextRenderingMode.ClearType);
            groupBox.FontFamily = new FontFamily("Microsoft Tai Le");
            groupBox.FontStyle = FontStyles.Normal;
            groupBox.FontSize = 13;
            groupBox.Style = FindResource("GroupBoxStyle1") as Style;
            groupBox.Padding = new Thickness(0);
            groupBox.FontWeight = FontWeights.Normal;
            groupBox.Margin = new Thickness(5);
            groupBox.ContextMenu = new ContextMenu();
            groupBox.ContextMenu.Visibility = Visibility.Hidden;

            GroupBox groupBoxcontent = new GroupBox();
            groupBoxcontent.Header = "Nội dung file import";
            groupBoxcontent.Foreground = Brushes.White;
            groupBoxcontent.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            groupBoxcontent.SetValue(TextOptions.TextRenderingModeProperty, TextRenderingMode.ClearType);
            groupBoxcontent.FontFamily = new FontFamily("Microsoft Tai Le");
            groupBoxcontent.FontStyle = FontStyles.Normal;
            groupBoxcontent.FontSize = 13;
            groupBoxcontent.Style = FindResource("GroupBoxStyle1") as Style;
            groupBoxcontent.Padding = new Thickness(0);
            groupBoxcontent.FontWeight = FontWeights.Normal;
            groupBoxcontent.Margin = new Thickness(5);
            groupBoxcontent.ContextMenu = new ContextMenu();
            groupBoxcontent.ContextMenu.Visibility = Visibility.Hidden;

            //TextBox Dictory file import
            TextBox txtdictor = new TextBox();
            txtdictor.Style = FindResource("TextBoxStyle1") as Style;
            txtdictor.IsReadOnly = true;
            txtdictor.TextWrapping = TextWrapping.Wrap;

            txtdictor.FontWeight = FontWeights.Normal;
            txtdictor.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            txtdictor.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtdictor.FontSize = 12;
            txtdictor.VerticalContentAlignment = VerticalAlignment.Center;

            //Add textbox text content of file
            TextBox txtfilecon = new TextBox();
            txtfilecon.Style = FindResource("TextBoxStyle1") as Style;
            txtfilecon.IsReadOnly = true;
            txtfilecon.TextWrapping = TextWrapping.Wrap;
            txtfilecon.AcceptsReturn = true;
            txtfilecon.FontWeight = FontWeights.Normal;
            txtfilecon.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            txtfilecon.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtfilecon.FontSize = 12;
            txtfilecon.Padding = new Thickness(3);


            //Add TextBox has new property into GroupBox dictory
            groupBox.Content = txtdictor;

            //Add groupbox  dictory into grid

            Grid.SetColumn(groupBox, 0);
            Grid.SetRow(groupBox, 0);
            grid.Children.Add(groupBox);

            //---------------------------------------------------------//

            //Add TextBox has new property into GroupBox file content
            groupBoxcontent.Content = txtfilecon;

            //Add groupbox file content into grid
            Grid.SetColumn(groupBoxcontent, 0);
            Grid.SetRow(groupBoxcontent, 1);
            grid.Children.Add(groupBoxcontent);

            //Set metroTabItem property to add
            MetroTabItem metroTab = new MetroTabItem();

            Color col = (Color)ColorConverter.ConvertFromString("#FF007ACC");
            SolidColorBrush colorBrush = new SolidColorBrush(col);
            metroTab.Background = colorBrush;
            metroTab.Foreground = Brushes.White;
            Thickness thickness = new Thickness(0, 0, 1, 0);
            metroTab.BorderThickness = thickness;
            metroTab.BorderBrush = Brushes.LightSlateGray;
            metroTab.FontWeight = FontWeights.Bold;
            metroTab.ContextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem();

            if(loaimahoa)//Chọn mã hoá DES
            {
                metroTab.Header = "File " + indextabfile;
                menuItem.Header = "Close This";
                menuItem.Click += delegate
                {
                    tabimportfile.Items.Remove(metroTab);
                    //if (indextabfile > 1)
                    //{
                    //    indextabfile--;
                    //}
                    metroTabItems.Remove(metroTab);
                    txtfilecontent.Remove(txtfilecon);
                    for (int i = 0; i < metroTabItemsEncrypt.Count; ++i)
                    {
                        if (metroTabItemsEncrypt[i].Header.ToString().Trim() == metroTab.Header.ToString().Trim())
                        {
                            tabencrypt.Items.Remove(metroTabItemsEncrypt[i]);
                            metroTabItemsEncrypt.RemoveAt(i);
                            isEncrypt.RemoveAt(i);
                            txtencrypt.RemoveAt(i);
                            tabdecrypt.Items.Remove(metroTabItemsDecrypt[i]);
                            metroTabItemsDecrypt.RemoveAt(i);
                            isDecrypt.RemoveAt(i);
                            txtdecrypt.RemoveAt(i);
                            KeyEncrypt.RemoveAt(i);
                            KeyIVEncrypt.RemoveAt(i);
                            break;
                        }
                    }
                    if (tabimportfile.Items.Count == 0)
                    {
                        indextabfile = 0;//Reset index apply to use to create tabpfile
                    }

                };
                MenuItem menuItem1 = new MenuItem();
                menuItem1.Header = "Close All But This";
                menuItem1.Click += delegate
                {
                    while (tabimportfile.Items.Count > 1)
                    {
                        for (int j = 0; j < tabimportfile.Items.Count; ++j)
                        {
                            if (tabimportfile.Items[j] != metroTab)
                            {
                                tabimportfile.Items.Remove(tabimportfile.Items[j]);
                                //if (indextabfile > 1)
                                //{
                                //    indextabfile--;
                                //}
                                metroTabItems.RemoveAt(j);
                                txtfilecontent.RemoveAt(j);
                                for (int i = 0; i < metroTabItemsEncrypt.Count; ++i)
                                {
                                    if (metroTabItemsEncrypt[i].Header.ToString().Trim() != metroTab.Header.ToString().Trim())
                                    {
                                        tabencrypt.Items.Remove(metroTabItemsEncrypt[i]);
                                        metroTabItemsEncrypt.RemoveAt(i);
                                        isEncrypt.RemoveAt(i);
                                        txtencrypt.RemoveAt(i);
                                        tabdecrypt.Items.Remove(metroTabItemsDecrypt[i]);
                                        metroTabItemsDecrypt.RemoveAt(i);
                                        isDecrypt.RemoveAt(i);
                                        txtdecrypt.RemoveAt(i);
                                        KeyEncrypt.RemoveAt(i);
                                        KeyIVEncrypt.RemoveAt(i);
                                        break;
                                    }
                                }
                            }
                        }

                    }
                };
                MenuItem menuItem2 = new MenuItem();
                menuItem2.Header = "Close Left This";
                menuItem2.Click += delegate
                {
                    for (int j = 0; tabimportfile.Items[j] != metroTab;)
                    {
                        tabimportfile.Items.Remove(tabimportfile.Items[j]);
                        //if (indextabfile > 1)
                        //{
                        //    indextabfile--;
                        //}
                        metroTabItems.RemoveAt(j);
                        txtfilecontent.RemoveAt(j);
                        for (int i = 0; i < metroTabItemsEncrypt.Count; ++i)
                        {
                            if (metroTabItemsEncrypt[i].Header.ToString().Trim() != metroTab.Header.ToString().Trim())
                            {
                                tabencrypt.Items.Remove(metroTabItemsEncrypt[i]);
                                metroTabItemsEncrypt.RemoveAt(i);
                                isEncrypt.RemoveAt(i);
                                txtencrypt.RemoveAt(i);
                                tabdecrypt.Items.Remove(metroTabItemsDecrypt[i]);
                                metroTabItemsDecrypt.RemoveAt(i);
                                isDecrypt.RemoveAt(i);
                                txtdecrypt.RemoveAt(i);
                                KeyEncrypt.RemoveAt(i);
                                KeyIVEncrypt.RemoveAt(i);
                                break;
                            }
                        }
                    }
                };
                MenuItem menuItem3 = new MenuItem();
                menuItem3.Header = "Close Right This";
                menuItem3.Click += delegate
                {
                    for (int j = tabimportfile.Items.Count; tabimportfile.Items[--j] != metroTab;)
                    {
                        tabimportfile.Items.Remove(tabimportfile.Items[j]);
                        //if (indextabfile > 1)
                        //{
                        //    indextabfile--;
                        //}
                        metroTabItems.RemoveAt(j);
                        txtfilecontent.RemoveAt(j);
                        for (int i = metroTabItemsEncrypt.Count - 1; i >= 0; --i)
                        {
                            if (metroTabItemsEncrypt[i].Header.ToString().Trim() != metroTab.Header.ToString().Trim())
                            {
                                tabencrypt.Items.Remove(metroTabItemsEncrypt[i]);
                                metroTabItemsEncrypt.RemoveAt(i);
                                isEncrypt.RemoveAt(i);
                                txtencrypt.RemoveAt(i);
                                tabdecrypt.Items.Remove(metroTabItemsDecrypt[i]);
                                metroTabItemsDecrypt.RemoveAt(i);
                                isDecrypt.RemoveAt(i);
                                txtdecrypt.RemoveAt(i);
                                KeyEncrypt.RemoveAt(i);
                                KeyIVEncrypt.RemoveAt(i);
                                break;
                            }
                        }
                    }
                };
                metroTab.ContextMenu.Items.Add(menuItem);
                metroTab.ContextMenu.Items.Add(menuItem1);
                metroTab.ContextMenu.Items.Add(menuItem2);
                metroTab.ContextMenu.Items.Add(menuItem3);
                //Add grid contans all control that it uses into metrotabitem
                metroTab.Content = grid;
                //add metroTabItem into list metrotabitem
                metroTabItems.Add(metroTab);

                //Add value of item in list isEncrypt and isDecrypt, initial they have false value
                isDecrypt.Add(false);
                isEncrypt.Add(false);

                //Add value of item in list FileEncryptDictory and FileDecryptDictory, initial they have null value
                FileEnCryptDictory.Add(null);
                FileDeCryptDictory.Add(null);

                //Key Encrypt, initial is null
                KeyEncrypt.Add(null);

                //Has IV Key, initial is null
                KeyIVEncrypt.Add(null);

                //Add TextBox Dictory into list TextBox dictory
                txtdic.Add(txtdictor);

                //Add TextBox file contant into list TextBox file content
                txtfilecontent.Add(txtfilecon);
            }
            else
            {
                metroTab.Header = "File " + indextabfile3DES;
                menuItem.Header = "Close This";
                menuItem.Click += delegate
                {
                    tabimportfile3DES.Items.Remove(metroTab);
                    metroTabItems3DES.Remove(metroTab);
                    txtfilecontent3DES.Remove(txtfilecon);
                    for (int i = 0; i < metroTabItemsEncrypt3DES.Count; ++i)
                    {
                        if (metroTabItemsEncrypt3DES[i].Header.ToString().Trim() == metroTab.Header.ToString().Trim())
                        {
                            tabencrypt3DES.Items.Remove(metroTabItemsEncrypt3DES[i]);
                            metroTabItemsEncrypt3DES.RemoveAt(i);
                            isEncrypt3DES.RemoveAt(i);
                            txtencrypt3DES.RemoveAt(i);
                            tabdecrypt3DES.Items.Remove(metroTabItemsDecrypt3DES[i]);
                            metroTabItemsDecrypt3DES.RemoveAt(i);
                            isDecrypt3DES.RemoveAt(i);
                            txtdecrypt3DES.RemoveAt(i);
                            KeyEncrypt3DES.RemoveAt(i);
                            KeyIVEncrypt3DES.RemoveAt(i);
                            break;
                        }
                    }
                    if (tabimportfile3DES.Items.Count == 0)
                    {
                        indextabfile3DES = 0;//Reset index apply to use to create tabpfile
                    }

                };
                MenuItem menuItem1 = new MenuItem();
                menuItem1.Header = "Close All But This";
                menuItem1.Click += delegate
                {
                    while (tabimportfile3DES.Items.Count > 1)
                    {
                        for (int j = 0; j < tabimportfile3DES.Items.Count; ++j)
                        {
                            if (tabimportfile3DES.Items[j] != metroTab)
                            {
                                tabimportfile3DES.Items.Remove(tabimportfile3DES.Items[j]);
                                metroTabItems3DES.RemoveAt(j);
                                txtfilecontent3DES.RemoveAt(j);
                                for (int i = 0; i < metroTabItemsEncrypt3DES.Count; ++i)
                                {
                                    if (metroTabItemsEncrypt3DES[i].Header.ToString().Trim() != metroTab.Header.ToString().Trim())
                                    {
                                        tabencrypt3DES.Items.Remove(metroTabItemsEncrypt3DES[i]);
                                        metroTabItemsEncrypt3DES.RemoveAt(i);
                                        isEncrypt3DES.RemoveAt(i);
                                        txtencrypt3DES.RemoveAt(i);
                                        tabdecrypt3DES.Items.Remove(metroTabItemsDecrypt3DES[i]);
                                        metroTabItemsDecrypt3DES.RemoveAt(i);
                                        isDecrypt3DES.RemoveAt(i);
                                        txtdecrypt3DES.RemoveAt(i);
                                        KeyEncrypt3DES.RemoveAt(i);
                                        KeyIVEncrypt3DES.RemoveAt(i);
                                        break;
                                    }
                                }
                            }
                        }

                    }
                };
                MenuItem menuItem2 = new MenuItem();
                menuItem2.Header = "Close Left This";
                menuItem2.Click += delegate
                {
                    for (int j = 0; tabimportfile3DES.Items[j] != metroTab;)
                    {
                        tabimportfile3DES.Items.Remove(tabimportfile3DES.Items[j]);
                        metroTabItems3DES.RemoveAt(j);
                        txtfilecontent3DES.RemoveAt(j);
                        for (int i = 0; i < metroTabItemsEncrypt3DES.Count; ++i)
                        {
                            if (metroTabItemsEncrypt3DES[i].Header.ToString().Trim() != metroTab.Header.ToString().Trim())
                            {
                                tabencrypt3DES.Items.Remove(metroTabItemsEncrypt3DES[i]);
                                metroTabItemsEncrypt3DES.RemoveAt(i);
                                isEncrypt3DES.RemoveAt(i);
                                txtencrypt3DES.RemoveAt(i);
                                tabdecrypt3DES.Items.Remove(metroTabItemsDecrypt3DES[i]);
                                metroTabItemsDecrypt3DES.RemoveAt(i);
                                isDecrypt3DES.RemoveAt(i);
                                txtdecrypt3DES.RemoveAt(i);
                                KeyEncrypt3DES.RemoveAt(i);
                                KeyIVEncrypt3DES.RemoveAt(i);
                                break;
                            }
                        }
                    }
                };
                MenuItem menuItem3 = new MenuItem();
                menuItem3.Header = "Close Right This";
                menuItem3.Click += delegate
                {
                    for (int j = tabimportfile3DES.Items.Count; tabimportfile3DES.Items[--j] != metroTab;)
                    {
                        tabimportfile3DES.Items.Remove(tabimportfile3DES.Items[j]);
                        metroTabItems3DES.RemoveAt(j);
                        txtfilecontent3DES.RemoveAt(j);
                        for (int i = metroTabItemsEncrypt3DES.Count - 1; i >= 0; --i)
                        {
                            if (metroTabItemsEncrypt3DES[i].Header.ToString().Trim() != metroTab.Header.ToString().Trim())
                            {
                                tabencrypt3DES.Items.Remove(metroTabItemsEncrypt3DES[i]);
                                metroTabItemsEncrypt3DES.RemoveAt(i);
                                isEncrypt3DES.RemoveAt(i);
                                txtencrypt3DES.RemoveAt(i);
                                tabdecrypt3DES.Items.Remove(metroTabItemsDecrypt3DES[i]);
                                metroTabItemsDecrypt3DES.RemoveAt(i);
                                isDecrypt3DES.RemoveAt(i);
                                txtdecrypt3DES.RemoveAt(i);
                                KeyEncrypt3DES.RemoveAt(i);
                                KeyIVEncrypt3DES.RemoveAt(i);
                                break;
                            }
                        }
                    }
                };
                metroTab.ContextMenu.Items.Add(menuItem);
                metroTab.ContextMenu.Items.Add(menuItem1);
                metroTab.ContextMenu.Items.Add(menuItem2);
                metroTab.ContextMenu.Items.Add(menuItem3);
                //Add grid contans all control that it uses into metrotabitem
                metroTab.Content = grid;

                //add metroTabItem into list metrotabitem
                metroTabItems3DES.Add(metroTab);

                //Add value of item in list isEncrypt and isDecrypt, initial they have false value
                isDecrypt3DES.Add(false);
                isEncrypt3DES.Add(false);

                //Add value of item in list FileEncryptDictory and FileDecryptDictory, initial they have null value
                FileEnCryptDictory3DES.Add(null);
                FileDeCryptDictory3DES.Add(null);

                //Key Encrypt, initial is null
                KeyEncrypt3DES.Add(null);

                //Has IV Key, initial is null
                KeyIVEncrypt3DES.Add(null);

                //Add TextBox Dictory into list TextBox dictory
                txtdic3DES.Add(txtdictor);

                //Add TextBox file contant into list TextBox file content
                txtfilecontent3DES.Add(txtfilecon);
            }
            
            return metroTab as object;
        }

        int indextabfile = 0;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //DES handle

        private void textencrypt(TextBox MessageIn, TextBox MessageOut, int index, string key, string iv)
        {
            byte[] Results = null;
            var UTF8 = new UTF8Encoding();
            var DESAlgorithm = new DESCryptoServiceProvider();
            try
            {
                DESAlgorithm.Key = Encoding.ASCII.GetBytes(key);
                DESAlgorithm.IV = Encoding.ASCII.GetBytes(iv);

                var DataToEncrypt = UTF8.GetBytes(MessageIn.Text);

                Results = DESAlgorithm.CreateEncryptor(DESAlgorithm.Key, DESAlgorithm.IV).TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                DESAlgorithm.Clear();
            }

            MessageOut.Text = Convert.ToBase64String(Results);

            TextisEncrypt[index] = true;
            KeyEncryptText[index] = key;
            if (key != iv)
            {
                KeyIVEncryptText[index] = iv;
            }
        }

        private void textdecrypt(TextBox MessageIn, TextBox MessageOut, int index, string key, string iv)
        {
            byte[] Results = null;
            var UTF8 = new UTF8Encoding();
            var DESAlgorithm = new DESCryptoServiceProvider();

            try
            {
                DESAlgorithm.Key = Encoding.ASCII.GetBytes(key);
                DESAlgorithm.IV = Encoding.ASCII.GetBytes(iv);

                var DataToDecrypt = Convert.FromBase64String(MessageIn.Text);

                Results = DESAlgorithm.CreateDecryptor(DESAlgorithm.Key, DESAlgorithm.IV).TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                DESAlgorithm.Clear();
            }

            MessageOut.Text = UTF8.GetString(Results);
            //Update value for items of TextisDecrypt relate
            TextisDecrypt[index] = true;
        }

        private void fileEncrypt(string Sourcefilename, string Destinatefilename, int index, string key, string iv)
        {
            try
            {
                DESCryptoServiceProvider myDESProvider = new DESCryptoServiceProvider();
                myDESProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
                myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);

                ICryptoTransform myICryptoTransform = myDESProvider.CreateEncryptor(myDESProvider.Key, myDESProvider.IV);
                FileStream ProcessFileStream = new FileStream(Sourcefilename, FileMode.Open, FileAccess.Read);

                FileStream ResultFileStream = new FileStream(Destinatefilename, FileMode.Create, FileAccess.Write);
                CryptoStream myCryptoStream = new CryptoStream(ResultFileStream, myICryptoTransform, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[ProcessFileStream.Length];
                ProcessFileStream.Read(bytearrayinput, 0, bytearrayinput.Length);
                myCryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                myCryptoStream.Close();
                ProcessFileStream.Close();
                ResultFileStream.Close();

                //Read data from file contains encrypt text to textbox in groupbox encrypt text
                txtencrypt[index].Text = File.ReadAllText(Destinatefilename);
                //Update value for items of isEncrypt, KeyEncypt and FileEncryptDictory relate
                isEncrypt[index] = true;
                FileEnCryptDictory[index] = Destinatefilename;
                KeyEncrypt[index] = key;
                if (key != iv)
                {
                    KeyIVEncrypt[index] = iv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fileDecrypt(string Sourcefilename, string Destinatefilename, int index, string key, string iv)
        {
            try
            {
                DESCryptoServiceProvider myDESProvider = new DESCryptoServiceProvider();
                myDESProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
                myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);

                ICryptoTransform myICryptoTransform = myDESProvider.CreateDecryptor(myDESProvider.Key, myDESProvider.IV);
                FileStream ProcessFileStream = new FileStream(Sourcefilename, FileMode.Open, FileAccess.Read);

                FileStream ResultFileStream = new FileStream(Destinatefilename, FileMode.Create, FileAccess.Write);
                CryptoStream myCryptoStream = new CryptoStream(ResultFileStream, myICryptoTransform, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[ProcessFileStream.Length];
                ProcessFileStream.Read(bytearrayinput, 0, bytearrayinput.Length);
                myCryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                myCryptoStream.Close();
                ProcessFileStream.Close();
                ResultFileStream.Close();

                //Read data from file contains decrypt text to textbox in groupbox decrypt text
                txtdecrypt[index].Text = File.ReadAllText(Destinatefilename);
                //Update value for items of isEncrypt, KeyEncypt and FileEncryptDictory relate
                isDecrypt[index] = true;
                FileDeCryptDictory[index] = Destinatefilename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnimport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Chọn file import!";
            openFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFile.ShowDialog() == true)
            {
                tabimportfile.Items.Add(metroTabitem(true));
                tabencrypt.Items.Add(TabContentEncryptText(true, indextabfile, true));
                tabdecrypt.Items.Add(TabContentDecryptText(true, indextabfile, true));
                txtdic[indextabfile].Text = openFile.FileName;
                txtfilecontent[txtfilecontent.Count - 1].Text = File.ReadAllText(openFile.FileName);
                indextabfile++;
            }
        }

        private void btndecrypt_Click(object sender, RoutedEventArgs e)
        {
            if (rdbfile.IsChecked == false && rdbtext.IsChecked == false)
            {
                MessageBox.Show("Bạn chưa chọn loại đối tượng mã hoá!");
                return;
            }
            else
            {
                //Chọn mã hoá file
                if (rdbfile.IsChecked == true)
                {
                    if (tabimportfile.Items.Count == 0)
                    {
                        MessageBox.Show("Bạn chưa import file mã hoá!");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < tabimportfile.Items.Count; ++i)
                        {
                            if (metroTabItems[i].IsSelected == true)
                            {
                                if (isEncrypt[i] == false || !File.Exists(FileEnCryptDictory[i]))
                                {
                                    MessageBox.Show("File chưa được mã hoá!");
                                    break;
                                }
                                else if (isDecrypt[i] == true && File.Exists(FileDeCryptDictory[i]))
                                {
                                    MessageBox.Show("File đã được giải mã! File giải mã nằm trong " + FileDeCryptDictory[i] + " và trong group 'Kết quả giả mã' tại Tab " + metroTabItemsDecrypt[i].Header + ".");
                                    break;
                                }
                                else if (isEncrypt[i] == true && File.Exists(FileEnCryptDictory[i]))
                                {
                                    if (txtkey.Text == "")
                                    {
                                        MessageBox.Show("Bạn phải nhập key!");
                                        break;
                                    }
                                    else
                                    {
                                        if (txtkey.Text == KeyEncrypt[i])
                                        {
                                            if (KeyIVEncrypt[i] != null)
                                            {
                                                if (txtiv.Text != KeyIVEncrypt[i])
                                                {
                                                    MessageBox.Show("Key IV sai! Key IV mã hoá đã dùng là " + KeyIVEncrypt[i] + ".");
                                                    break;
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        SaveFileDialog saveDialog = new SaveFileDialog();
                                                        saveDialog.Filter = "Text Files (*.txt)|*.txt";
                                                        saveDialog.Title = "Chọn đường dẵn lưu file giải mã!";
                                                        if (saveDialog.ShowDialog() == true)
                                                        {
                                                            if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                                                            {
                                                                MessageBox.Show("File lưu phải có dạng text (.txt)!");
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                //Decrypt file content
                                                                fileDecrypt(FileEnCryptDictory[i], saveDialog.FileName, i, txtkey.Text, txtiv.Text);
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        MessageBox.Show(ex.Message);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    SaveFileDialog saveDialog = new SaveFileDialog();
                                                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                                                    saveDialog.Title = "Chọn đường dẵn lưu file giải mã!";
                                                    if (saveDialog.ShowDialog() == true)
                                                    {
                                                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                                                        {
                                                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            //Decrypt file content
                                                            fileDecrypt(FileEnCryptDictory[i], saveDialog.FileName, i, txtkey.Text, txtkey.Text);
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Key sai! Key đã chọn để mã hoá là " + KeyEncrypt[i]);
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }

                //Chọn mã hoá text
                if (rdbtext.IsChecked == true)
                {
                    for (int i = 0; i < tabparagraph.Items.Count; ++i)
                    {
                        if (metroTabItemsPara[i].IsSelected == true)
                        {
                            if (txtpara[i].Text == "")
                            {
                                MessageBox.Show("Bạn chưa nhập dữ liệu text để mã hoá!");
                                break;
                            }
                            else
                            {
                                if (TextisEncrypt[i] == false && txtencryptText[i].Text == "")
                                {
                                    MessageBox.Show("Text chưa được mã hoá!");
                                    break;
                                }
                                else if (TextisDecrypt[i] == true && txtdecryptText[i].Text != "")
                                {
                                    MessageBox.Show("Text đã được giải mã! Text giải mã nằm trong group 'Kết quả giải mã' tại Tab " + metroTabItemsDecryptText[i].Header + ".");
                                    break;
                                }
                                else
                                {
                                    if (txtkey.Text == "")
                                    {
                                        MessageBox.Show("Bạn phải nhập key!");
                                        break;
                                    }
                                    else
                                    {
                                        if (txtkey.Text == KeyEncryptText[i])
                                        {
                                            if (KeyIVEncryptText[i] != null)
                                            {
                                                if (txtiv.Text != KeyIVEncryptText[i])
                                                {
                                                    MessageBox.Show("Key IV sai! Key IV mã hoá đã dùng là " + KeyIVEncryptText[i] + ".");
                                                    break;
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        //Decrypt Text content
                                                        textdecrypt(txtencryptText[i], txtdecryptText[i], i, txtkey.Text, txtiv.Text);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        MessageBox.Show(ex.Message);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    //Decrypt Text content
                                                    textdecrypt(txtencryptText[i], txtdecryptText[i], i, txtkey.Text, txtkey.Text);
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Key sai! Key đã chọn để mã hoá là " + KeyEncryptText[i]);
                                            break;
                                        }
                                    }

                                }
                                break;
                            }
                        }
                    }

                }
            }
        }

        private void btnencrypt_Click(object sender, RoutedEventArgs e)
        {
            if (rdbfile.IsChecked == false && rdbtext.IsChecked == false)
            {
                MessageBox.Show("Bạn chưa chọn loại đối tượng mã hoá!");
                return;
            }
            else
            {
                //Chọn mã hoá file
                if (rdbfile.IsChecked == true)
                {
                    if (tabimportfile.Items.Count == 0)
                    {
                        MessageBox.Show("Bạn chưa import file mã hoá!");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < tabimportfile.Items.Count; ++i)
                        {
                            if (metroTabItems[i].IsSelected == true)
                            {
                                if (isEncrypt[i] == true && File.Exists(FileEnCryptDictory[i]))
                                {
                                    MessageBox.Show("File đã mã hoá! File mã hoá nằm trong " + FileEnCryptDictory[i] + " và trong group 'Kết quả giải mã' tại Tab " + metroTabItemsEncrypt[i].Header + ".");
                                    return;
                                }
                                else
                                {
                                    if (txtkey.Text == "")
                                    {
                                        MessageBox.Show("Bạn phải nhập key!");
                                        break;
                                    }
                                    else
                                    {
                                        if (txtiv.Text != "")
                                        {
                                            try
                                            {
                                                SaveFileDialog saveDialog = new SaveFileDialog();
                                                saveDialog.Filter = "Text Files (*.txt)|*.txt";
                                                saveDialog.Title = "Chọn đường dẵn lưu file mã hoá!";
                                                if (saveDialog.ShowDialog() == true)
                                                {
                                                    if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                                                    {
                                                        MessageBox.Show("File lưu phải có dạng text (.txt)!");
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        //Encrypt file content
                                                        fileEncrypt(txtdic[i].Text, saveDialog.FileName, i, txtkey.Text, txtiv.Text);
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                SaveFileDialog saveDialog = new SaveFileDialog();
                                                saveDialog.Filter = "Text Files (*.txt)|*.txt";
                                                saveDialog.Title = "Chọn đường dẵn lưu file mã hoá!";
                                                if (saveDialog.ShowDialog() == true)
                                                {
                                                    if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                                                    {
                                                        MessageBox.Show("File lưu phải có dạng text (.txt)!");
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        //Encrypt file content
                                                        fileEncrypt(txtdic[i].Text, saveDialog.FileName, i, txtkey.Text, txtkey.Text);
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }
                                        }
                                    }

                                }
                                break;
                            }
                        }
                    }
                }

                //Chọn mã hoá text
                if (rdbtext.IsChecked == true)
                {
                    for (int i = 0; i < tabparagraph.Items.Count; ++i)
                    {
                        if (metroTabItemsPara[i].IsSelected == true)
                        {
                            if (txtpara[i].Text == "")
                            {
                                MessageBox.Show("Bạn chưa nhập dữ liệu text để mã hoá!");
                                break;
                            }
                            else
                            {
                                if (TextisEncrypt[i] == true && txtencryptText[i].Text != "")
                                {
                                    MessageBox.Show("Text đã mã hoá! Text mã hoá nằm trong group 'Kết quả mã hoá' tại Tab " + metroTabItemsEncryptText[i].Header + ".");
                                    break;
                                }
                                else
                                {
                                    if (txtkey.Text == "")
                                    {
                                        MessageBox.Show("Bạn phải nhập key!");
                                        break;
                                    }
                                    else
                                    {
                                        if (txtiv.Text != "")
                                        {
                                            try
                                            {
                                                //Encrypt text content
                                                textencrypt(txtpara[i], txtencryptText[i], i, txtkey.Text, txtiv.Text);
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                //Encrypt text content
                                                textencrypt(txtpara[i], txtencryptText[i], i, txtkey.Text, txtkey.Text);
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }
                                        }
                                    }

                                }
                                break;
                            }
                        }
                    }

                }
            }
        }

        private void rdbfile_Checked(object sender, RoutedEventArgs e)
        {
            if (rdbfile.IsChecked == true)
            {
                btnimport.IsEnabled = true;
                if (tabparagraph.Items.Count > 0)
                {
                    for (int i = 0; i < txtpara.Count; ++i)
                    {
                        if (txtpara[i].Text == "")
                        {
                            tabparagraph.Items.Remove(metroTabItemsPara[i]);//Xoá tabitem khi chọn mã hoá file với điều kiện đoạn text rỗng.
                            tabencrypt.Items.Remove(metroTabItemsEncryptText[i]);//Xoá tabitem trong group kết quả mã hoá tương ứng.
                            tabdecrypt.Items.Remove(metroTabItemsDecryptText[i]);//Xoá tabitem trong group kết quả giải mã tương ứng.

                            metroTabItemsPara.RemoveAt(i);
                            metroTabItemsDecryptText.RemoveAt(i);
                            metroTabItemsEncryptText.RemoveAt(i);
                            txtpara.RemoveAt(i);
                            TextisDecrypt.RemoveAt(i);
                            TextisEncrypt.RemoveAt(i);
                            KeyEncryptText.RemoveAt(i);
                            KeyIVEncryptText.RemoveAt(i);
                            txtencryptText.RemoveAt(i);
                            txtdecryptText.RemoveAt(i);
                            //if (indextabparagraph > 1)
                            //{
                            //    indextabparagraph--;
                            //}
                        }
                    }
                }
            }
        }

        private void rdbtext_Checked(object sender, RoutedEventArgs e)
        {
            if (rdbtext.IsChecked == true)
            {
                tabparagraph.Items.Add(TabContentParagraph(true));
                tabencrypt.Items.Add(TabContentEncryptText(false, indextabparagraph, true));
                tabdecrypt.Items.Add(TabContentDecryptText(false, indextabparagraph, true));
                indextabparagraph++;
                btnimport.IsEnabled = false;
            }
        }

        private void btnexportencrypt_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < metroTabItemsEncrypt.Count; ++i)
            {
                if (metroTabItemsEncrypt[i].IsSelected == true)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveDialog.Title = "Chọn đường dẵn export file mã hoá!";
                    if (saveDialog.ShowDialog() == true)
                    {
                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                        {
                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                            break;
                        }
                        else
                        {
                            try
                            {
                                //Write to file
                                StreamWriter sw = new StreamWriter(saveDialog.FileName);
                                sw.Write(txtencrypt[i].Text);
                                sw.Close();
                                MessageBox.Show("Export thành công!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < metroTabItemsEncryptText.Count; ++i)
            {
                if (metroTabItemsEncryptText[i].IsSelected == true)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveDialog.Title = "Chọn đường dẵn export file mã hoá!";
                    if (saveDialog.ShowDialog() == true)
                    {
                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                        {
                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                            break;
                        }
                        else
                        {
                            try
                            {
                                //Write to file
                                StreamWriter sw = new StreamWriter(saveDialog.FileName);
                                sw.Write(txtencryptText[i].Text);
                                sw.Close();
                                MessageBox.Show("Export thành công!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void btnexportdecrypt_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < metroTabItemsDecrypt.Count; ++i)
            {
                if (metroTabItemsDecrypt[i].IsSelected == true)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveDialog.Title = "Chọn đường dẵn export file giải mã!";
                    if (saveDialog.ShowDialog() == true)
                    {
                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                        {
                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                            break;
                        }
                        else
                        {
                            try
                            {
                                //Write to file
                                StreamWriter sw = new StreamWriter(saveDialog.FileName);
                                sw.Write(txtdecrypt[i].Text);
                                sw.Close();
                                MessageBox.Show("Export thành công!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < metroTabItemsDecryptText.Count; ++i)
            {
                if (metroTabItemsDecryptText[i].IsSelected == true)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveDialog.Title = "Chọn đường dẵn export file mã hoá!";
                    if (saveDialog.ShowDialog() == true)
                    {
                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                        {
                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                            break;
                        }
                        else
                        {
                            try
                            {
                                //Write to file
                                StreamWriter sw = new StreamWriter(saveDialog.FileName);
                                sw.Write(txtdecryptText[i].Text);
                                sw.Close();
                                MessageBox.Show("Export thành công!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }


        //3DES handle
        int indextabparagraph3DES = 0;

        int indextabfile3DES = 0;

        private void textencrypt3DES(TextBox MessageIn, TextBox MessageOut, int index, string key, string iv)
        {            
            byte[] Results = null;
            var UTF8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            var TDESAlgorithm = new TripleDESCryptoServiceProvider();//Tạo đối tượng mã hoá 3DES
            try
            {
                TDESAlgorithm.Key = hashProvider.ComputeHash(UTF8.GetBytes(key));
                if (iv != "")
                {
                    TDESAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(iv);
                }
                else
                {
                    TDESAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes("nonkeyiv");
                }
                var DataToEncrypt = UTF8.GetBytes(MessageIn.Text);
                Results = TDESAlgorithm.CreateEncryptor(TDESAlgorithm.Key, TDESAlgorithm.IV).TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                TDESAlgorithm.Clear();
                hashProvider.Clear();
            }

            MessageOut.Text = Convert.ToBase64String(Results);

            TextisEncrypt3DES[index] = true;
            KeyEncryptText3DES[index] = key;
            KeyIVEncryptText3DES[index] = iv;
        }

        private void textdecrypt3DES(TextBox MessageIn, TextBox MessageOut, int index, string key, string iv)
        {
            byte[] Results = null;
            var UTF8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            var TDESAlgorithm = new TripleDESCryptoServiceProvider();

            try
            {
                TDESAlgorithm.Key = hashProvider.ComputeHash(UTF8.GetBytes(key));
                if (iv != "")
                {
                    TDESAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes(iv);
                }
                else
                {
                    TDESAlgorithm.IV = ASCIIEncoding.ASCII.GetBytes("nonkeyiv");
                }
                var DataToDecrypt = Convert.FromBase64String(MessageIn.Text);

                Results = TDESAlgorithm.CreateDecryptor(TDESAlgorithm.Key, TDESAlgorithm.IV).TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                TDESAlgorithm.Clear();
                hashProvider.Clear();
            }

            MessageOut.Text = UTF8.GetString(Results);
            //Update value for items of TextisDecrypt relate
            TextisDecrypt3DES[index] = true;
        }

        private void fileEncrypt3DES(string Sourcefilename, string Destinatefilename, int index, string key, string iv)
        {
            var UTF8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();

            try
            {
                TripleDESCryptoServiceProvider myDESProvider = new TripleDESCryptoServiceProvider();
                myDESProvider.Key = hashProvider.ComputeHash(UTF8.GetBytes(key));
                if (iv != "")
                {
                    myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
                }
                else
                {
                    myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes("nonkeyiv");
                }

                ICryptoTransform myICryptoTransform = myDESProvider.CreateEncryptor(myDESProvider.Key, myDESProvider.IV);
                FileStream ProcessFileStream = new FileStream(Sourcefilename, FileMode.Open, FileAccess.Read);

                FileStream ResultFileStream = new FileStream(Destinatefilename, FileMode.Create, FileAccess.Write);
                CryptoStream myCryptoStream = new CryptoStream(ResultFileStream, myICryptoTransform, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[ProcessFileStream.Length];
                ProcessFileStream.Read(bytearrayinput, 0, bytearrayinput.Length);
                myCryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                myCryptoStream.Close();
                hashProvider.Clear();
                ProcessFileStream.Close();
                ResultFileStream.Close();

                //Read data from file contains encrypt text to textbox in groupbox encrypt text
                txtencrypt3DES[index].Text = File.ReadAllText(Destinatefilename);
                //Update value for items of isEncrypt, KeyEncypt and FileEncryptDictory relate
                isEncrypt3DES[index] = true;
                FileEnCryptDictory3DES[index] = Destinatefilename;
                KeyEncrypt3DES[index] = key;
                KeyIVEncrypt3DES[index] = iv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fileDecrypt3DES(string Sourcefilename, string Destinatefilename, int index, string key, string iv)
        {
            var UTF8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();

            try
            {
                TripleDESCryptoServiceProvider myDESProvider = new TripleDESCryptoServiceProvider();
                myDESProvider.Key = hashProvider.ComputeHash(UTF8.GetBytes(key));
                if (iv != "")
                {
                    myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
                }
                else
                {
                    myDESProvider.IV = ASCIIEncoding.ASCII.GetBytes("nonkeyiv");
                }

                ICryptoTransform myICryptoTransform = myDESProvider.CreateDecryptor(myDESProvider.Key, myDESProvider.IV);
                FileStream ProcessFileStream = new FileStream(Sourcefilename, FileMode.Open, FileAccess.Read);

                FileStream ResultFileStream = new FileStream(Destinatefilename, FileMode.Create, FileAccess.Write);
                CryptoStream myCryptoStream = new CryptoStream(ResultFileStream, myICryptoTransform, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[ProcessFileStream.Length];
                ProcessFileStream.Read(bytearrayinput, 0, bytearrayinput.Length);
                myCryptoStream.Write(bytearrayinput, 0, bytearrayinput.Length);
                myCryptoStream.Close();
                hashProvider.Clear();
                ProcessFileStream.Close();
                ResultFileStream.Close();

                //Read data from file contains decrypt text to textbox in groupbox decrypt text
                txtdecrypt3DES[index].Text = File.ReadAllText(Destinatefilename);
                //Update value for items of isEncrypt, KeyEncypt and FileEncryptDictory relate
                isDecrypt3DES[index] = true;
                FileDeCryptDictory3DES[index] = Destinatefilename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnimport3DES_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Chọn file import!";
            openFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFile.ShowDialog() == true)
            {
                tabimportfile3DES.Items.Add(metroTabitem(false));
                tabencrypt3DES.Items.Add(TabContentEncryptText(true, indextabfile3DES, false));
                tabdecrypt3DES.Items.Add(TabContentDecryptText(true, indextabfile3DES, false));
                txtdic3DES[indextabfile3DES].Text = openFile.FileName;
                txtfilecontent3DES[txtfilecontent3DES.Count - 1].Text = File.ReadAllText(openFile.FileName);
                indextabfile3DES++;
            }
        }

        private void btnencrypt3DES_Click(object sender, RoutedEventArgs e)
        {
            if (rdbfile3DES.IsChecked == false && rdbtext3DES.IsChecked == false)
            {
                MessageBox.Show("Bạn chưa chọn loại đối tượng mã hoá!");
                return;
            }
            else
            {
                //Chọn mã hoá file
                if (rdbfile3DES.IsChecked == true)
                {
                    if (tabimportfile3DES.Items.Count == 0)
                    {
                        MessageBox.Show("Bạn chưa import file mã hoá!");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < tabimportfile3DES.Items.Count; ++i)
                        {
                            if (metroTabItems3DES[i].IsSelected == true)
                            {
                                if (isEncrypt3DES[i] == true && File.Exists(FileEnCryptDictory3DES[i]))
                                {
                                    MessageBox.Show("File đã mã hoá! File mã hoá nằm trong " + FileEnCryptDictory3DES[i] + " và trong group 'Kết quả giải mã' tại Tab " + metroTabItemsEncrypt3DES[i].Header + ".");
                                    return;
                                }
                                else
                                {
                                    if (txtkey3DES.Text == "")
                                    {
                                        MessageBox.Show("Bạn phải nhập key!");
                                        break;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            SaveFileDialog saveDialog = new SaveFileDialog();
                                            saveDialog.Filter = "Text Files (*.txt)|*.txt";
                                            saveDialog.Title = "Chọn đường dẵn lưu file mã hoá!";
                                            if (saveDialog.ShowDialog() == true)
                                            {
                                                if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                                                {
                                                    MessageBox.Show("File lưu phải có dạng text (.txt)!");
                                                    return;
                                                }
                                                else
                                                {
                                                    //Encrypt file content
                                                    fileEncrypt3DES(txtdic3DES[i].Text, saveDialog.FileName, i, txtkey3DES.Text, txtiv3DES.Text);
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }

                //Chọn mã hoá text
                if (rdbtext3DES.IsChecked == true)
                {
                    for (int i = 0; i < tabparagraph3DES.Items.Count; ++i)
                    {
                        if (metroTabItemsPara3DES[i].IsSelected == true)
                        {
                            if (txtpara3DES[i].Text == "")
                            {
                                MessageBox.Show("Bạn chưa nhập dữ liệu text để mã hoá!");
                                break;
                            }
                            else
                            {
                                if (TextisEncrypt3DES[i] == true && txtencryptText3DES[i].Text != "")
                                {
                                    MessageBox.Show("Text đã mã hoá! Text mã hoá nằm trong group 'Kết quả mã hoá' tại Tab " + metroTabItemsEncryptText3DES[i].Header + ".");
                                    break;
                                }
                                else
                                {
                                    if (txtkey3DES.Text == "")
                                    {
                                        MessageBox.Show("Bạn phải nhập key!");
                                        break;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            //Encrypt text content
                                            textencrypt3DES(txtpara3DES[i], txtencryptText3DES[i], i, txtkey3DES.Text, txtiv3DES.Text);
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }

                                }
                                break;
                            }
                        }
                    }

                }
            }
        }

        private void btndecrypt3DES_Click(object sender, RoutedEventArgs e)
        {
            if (rdbfile3DES.IsChecked == false && rdbtext3DES.IsChecked == false)
            {
                MessageBox.Show("Bạn chưa chọn loại đối tượng mã hoá!");
                return;
            }
            else
            {
                //Chọn mã hoá file
                if (rdbfile3DES.IsChecked == true)
                {
                    if (tabimportfile3DES.Items.Count == 0)
                    {
                        MessageBox.Show("Bạn chưa import file mã hoá!");
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < tabimportfile3DES.Items.Count; ++i)
                        {
                            if (metroTabItems3DES[i].IsSelected == true)
                            {
                                if (isEncrypt3DES[i] == false || !File.Exists(FileEnCryptDictory3DES[i]))
                                {
                                    MessageBox.Show("File chưa được mã hoá!");
                                    break;
                                }
                                else if (isDecrypt3DES[i] == true && File.Exists(FileDeCryptDictory3DES[i]))
                                {
                                    MessageBox.Show("File đã được giải mã! File giải mã nằm trong " + FileDeCryptDictory3DES[i] + " và trong group 'Kết quả giả mã' tại Tab " + metroTabItemsDecrypt3DES[i].Header + ".");
                                    break;
                                }
                                else if (isEncrypt3DES[i] == true && File.Exists(FileEnCryptDictory3DES[i]))
                                {
                                    if (txtkey3DES.Text == "")
                                    {
                                        MessageBox.Show("Bạn phải nhập key!");
                                        break;
                                    }
                                    else
                                    {
                                        if (txtkey3DES.Text == KeyEncrypt3DES[i])
                                        {
                                            if (txtiv3DES.Text != KeyIVEncrypt3DES[i])
                                            {
                                                MessageBox.Show("Key IV sai! Key IV mã hoá đã dùng là " + KeyIVEncrypt3DES[i] + ".");
                                                break;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    SaveFileDialog saveDialog = new SaveFileDialog();
                                                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                                                    saveDialog.Title = "Chọn đường dẵn lưu file giải mã!";
                                                    if (saveDialog.ShowDialog() == true)
                                                    {
                                                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                                                        {
                                                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            //Decrypt file content
                                                            fileDecrypt3DES(FileEnCryptDictory3DES[i], saveDialog.FileName, i, txtkey3DES.Text, txtiv3DES.Text);
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Key sai! Key đã chọn để mã hoá là " + KeyEncrypt3DES[i]);
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }

                //Chọn mã hoá text
                if (rdbtext3DES.IsChecked == true)
                {
                    for (int i = 0; i < tabparagraph3DES.Items.Count; ++i)
                    {
                        if (metroTabItemsPara3DES[i].IsSelected == true)
                        {
                            if (txtpara3DES[i].Text == "")
                            {
                                MessageBox.Show("Bạn chưa nhập dữ liệu text để mã hoá!");
                                break;
                            }
                            else
                            {
                                if (TextisEncrypt3DES[i] == false && txtencryptText3DES[i].Text == "")
                                {
                                    MessageBox.Show("Text chưa được mã hoá!");
                                    break;
                                }
                                else if (TextisDecrypt3DES[i] == true && txtdecryptText3DES[i].Text != "")
                                {
                                    MessageBox.Show("Text đã được giải mã! Text giải mã nằm trong group 'Kết quả giải mã' tại Tab " + metroTabItemsDecryptText3DES[i].Header + ".");
                                    break;
                                }
                                else
                                {
                                    if (txtkey3DES.Text == "")
                                    {
                                        MessageBox.Show("Bạn phải nhập key!");
                                        break;
                                    }
                                    else
                                    {
                                        if (txtkey3DES.Text == KeyEncryptText3DES[i])
                                        {
                                            if (txtiv3DES.Text != KeyIVEncryptText3DES[i])
                                            {
                                                MessageBox.Show("Key IV sai! Key IV mã hoá đã dùng là " + KeyIVEncryptText3DES[i] + ".");
                                                break;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    //Decrypt Text content
                                                    textdecrypt3DES(txtencryptText3DES[i], txtdecryptText3DES[i], i, txtkey3DES.Text, txtiv3DES.Text);
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Key sai! Key đã chọn để mã hoá là " + KeyEncryptText3DES[i]);
                                            break;
                                        }
                                    }

                                }
                                break;
                            }
                        }
                    }

                }
            }
        }

        private void btnexportencrypt3DES_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < metroTabItemsEncrypt3DES.Count; ++i)
            {
                if (metroTabItemsEncrypt3DES[i].IsSelected == true)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveDialog.Title = "Chọn đường dẵn export file mã hoá!";
                    if (saveDialog.ShowDialog() == true)
                    {
                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                        {
                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                            break;
                        }
                        else
                        {
                            try
                            {
                                //Write to file
                                StreamWriter sw = new StreamWriter(saveDialog.FileName);
                                sw.Write(txtencrypt3DES[i].Text);
                                sw.Close();
                                MessageBox.Show("Export thành công!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < metroTabItemsEncryptText3DES.Count; ++i)
            {
                if (metroTabItemsEncryptText3DES[i].IsSelected == true)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveDialog.Title = "Chọn đường dẵn export file mã hoá!";
                    if (saveDialog.ShowDialog() == true)
                    {
                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                        {
                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                            break;
                        }
                        else
                        {
                            try
                            {
                                //Write to file
                                StreamWriter sw = new StreamWriter(saveDialog.FileName);
                                sw.Write(txtencryptText3DES[i].Text);
                                sw.Close();
                                MessageBox.Show("Export thành công!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void btnexportdecrypt3DES_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < metroTabItemsDecrypt3DES.Count; ++i)
            {
                if (metroTabItemsDecrypt3DES[i].IsSelected == true)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveDialog.Title = "Chọn đường dẵn export file giải mã!";
                    if (saveDialog.ShowDialog() == true)
                    {
                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                        {
                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                            break;
                        }
                        else
                        {
                            try
                            {
                                //Write to file
                                StreamWriter sw = new StreamWriter(saveDialog.FileName);
                                sw.Write(txtdecrypt3DES[i].Text);
                                sw.Close();
                                MessageBox.Show("Export thành công!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < metroTabItemsDecryptText3DES.Count; ++i)
            {
                if (metroTabItemsDecryptText3DES[i].IsSelected == true)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveDialog.Title = "Chọn đường dẵn export file mã hoá!";
                    if (saveDialog.ShowDialog() == true)
                    {
                        if (saveDialog.FileName.Substring(saveDialog.FileName.Length - 4, 4) != ".txt")
                        {
                            MessageBox.Show("File lưu phải có dạng text (.txt)!");
                            break;
                        }
                        else
                        {
                            try
                            {
                                //Write to file
                                StreamWriter sw = new StreamWriter(saveDialog.FileName);
                                sw.Write(txtdecryptText3DES[i].Text);
                                sw.Close();
                                MessageBox.Show("Export thành công!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void rdbfile3DES_Checked(object sender, RoutedEventArgs e)
        {
            if (rdbfile3DES.IsChecked == true)
            {
                btnimport3DES.IsEnabled = true;
                if (tabparagraph3DES.Items.Count > 0)
                {
                    for (int i = 0; i < txtpara3DES.Count; ++i)
                    {
                        if (txtpara3DES[i].Text == "")
                        {
                            tabparagraph3DES.Items.Remove(metroTabItemsPara3DES[i]);//Xoá tabitem khi chọn mã hoá file với điều kiện đoạn text rỗng.
                            tabencrypt3DES.Items.Remove(metroTabItemsEncryptText3DES[i]);//Xoá tabitem trong group kết quả mã hoá tương ứng.
                            tabdecrypt3DES.Items.Remove(metroTabItemsDecryptText3DES[i]);//Xoá tabitem trong group kết quả giải mã tương ứng.

                            metroTabItemsPara3DES.RemoveAt(i);
                            metroTabItemsDecryptText3DES.RemoveAt(i);
                            metroTabItemsEncryptText3DES.RemoveAt(i);
                            txtpara3DES.RemoveAt(i);
                            TextisDecrypt3DES.RemoveAt(i);
                            TextisEncrypt3DES.RemoveAt(i);
                            KeyEncryptText3DES.RemoveAt(i);
                            KeyIVEncryptText3DES.RemoveAt(i);
                            txtencryptText3DES.RemoveAt(i);
                            txtdecryptText3DES.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void tbci3des_GotFocus(object sender, RoutedEventArgs e)
        {
            lblcrypt.Content = "[] Mã hoá 3DES";
        }

        private void tbcdes_GotFocus(object sender, RoutedEventArgs e)
        {
            lblcrypt.Content = "[] Mã hoá DES";
        }

        private void rdbtext3DES_Checked(object sender, RoutedEventArgs e)
        {
            if (rdbtext3DES.IsChecked == true)
            {
                tabparagraph3DES.Items.Add(TabContentParagraph(false));
                tabencrypt3DES.Items.Add(TabContentEncryptText(false, indextabparagraph3DES, false));
                tabdecrypt3DES.Items.Add(TabContentDecryptText(false, indextabparagraph3DES, false));
                indextabparagraph3DES++;
                btnimport3DES.IsEnabled = false;
            }
        }
    }
}
