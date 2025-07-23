
using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.Layout;
using System.ComponentModel;
using System.Runtime.Remoting;



namespace atmsystem
{
    public partial class Login_frm
    {
        public Login_frm()
        {
            InitializeComponent();

        }

        #region Default Instance

        private static Login_frm defaultInstance;


        public static Login_frm Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new Login_frm();
                    defaultInstance.FormClosed += new FormClosedEventHandler(DefaultInstance_FormClosed);
                }

                return defaultInstance;
            }
        }

        public override bool AllowDrop { get => base.AllowDrop; set => base.AllowDrop = value; }
        public override AnchorStyles Anchor { get => base.Anchor; set => base.Anchor = value; }
        public override Point AutoScrollOffset { get => base.AutoScrollOffset; set => base.AutoScrollOffset = value; }

        public override LayoutEngine LayoutEngine => base.LayoutEngine;

        public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }
        public override ImageLayout BackgroundImageLayout { get => base.BackgroundImageLayout; set => base.BackgroundImageLayout = value; }

        protected override bool CanRaiseEvents => base.CanRaiseEvents;

        public override ContextMenu ContextMenu { get => base.ContextMenu; set => base.ContextMenu = value; }
        public override ContextMenuStrip ContextMenuStrip { get => base.ContextMenuStrip; set => base.ContextMenuStrip = value; }
        public override Cursor Cursor { get => base.Cursor; set => base.Cursor = value; }

        protected override Cursor DefaultCursor => base.DefaultCursor;

        protected override Padding DefaultMargin => base.DefaultMargin;

        protected override Size DefaultMaximumSize => base.DefaultMaximumSize;

        protected override Size DefaultMinimumSize => base.DefaultMinimumSize;

        protected override Padding DefaultPadding => base.DefaultPadding;

        public override DockStyle Dock { get => base.Dock; set => base.Dock = value; }
        protected override bool DoubleBuffered { get => base.DoubleBuffered; set => base.DoubleBuffered = value; }

        public override bool Focused => base.Focused;

        public override Font Font { get => base.Font; set => base.Font = value; }
        public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }
        public override RightToLeft RightToLeft { get => base.RightToLeft; set => base.RightToLeft = value; }

        protected override bool ScaleChildren => base.ScaleChildren;

        public override ISite Site { get => base.Site; set => base.Site = value; }

        protected override bool ShowKeyboardCues => base.ShowKeyboardCues;

        protected override bool ShowFocusCues => base.ShowFocusCues;

        protected override ImeMode ImeModeBase { get => base.ImeModeBase; set => base.ImeModeBase = value; }

        public override Rectangle DisplayRectangle => base.DisplayRectangle;

        public override BindingContext BindingContext { get => base.BindingContext; set => base.BindingContext = value; }

        protected override bool CanEnableIme => base.CanEnableIme;

        public override Size AutoScaleBaseSize { get => base.AutoScaleBaseSize; set => base.AutoScaleBaseSize = value; }
        public override bool AutoScroll { get => base.AutoScroll; set => base.AutoScroll = value; }
        public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = value; }
        public override AutoValidate AutoValidate { get => base.AutoValidate; set => base.AutoValidate = value; }
        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

        protected override CreateParams CreateParams => base.CreateParams;

        protected override ImeMode DefaultImeMode => base.DefaultImeMode;

        protected override Size DefaultSize => base.DefaultSize;

        public override Size MaximumSize { get => base.MaximumSize; set => base.MaximumSize = value; }
        public override Size MinimumSize { get => base.MinimumSize; set => base.MinimumSize = value; }
        public override bool RightToLeftLayout { get => base.RightToLeftLayout; set => base.RightToLeftLayout = value; }

        protected override bool ShowWithoutActivation => base.ShowWithoutActivation;

        public override string Text { get => base.Text; set => base.Text = value; }

        static void DefaultInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            defaultInstance = null;
        }

        #endregion
        readonly System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
        readonly System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        readonly DataSet ds = new DataSet();
        readonly System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection();
        public void Btnlogin_Click(System.Object sender, System.EventArgs e)
        {
            DataTable Log_in = new DataTable();
            try
            {
                if (txtpin.Text == "")
                {
                    MessageBox.Show("Pls Enter both Fields");

                }
                else
                {
                    con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\ATMsystem.accdb";
                    string sql = "SELECT * FROM tblinfo where pin_code = " + txtpin.Text + "";

                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    da.SelectCommand = cmd;
                    da.Fill(Log_in);
                    if (Log_in.Rows.Count > 0)
                    {
                        string Type;
                        string Fullname = default;
                        string accno = default;
                        Type = (string)(Log_in.Rows[0]["type"]);
                        Fullname = (string)(Log_in.Rows[0]["Firstname"]);
                        accno = Convert.ToString((Log_in.Rows[0]["account_no"]));
                        if (Type == "admin")
                        {
                            MessageBox.Show("Welcome " + Fullname + " you login as Administrator ");
                            AdminForm.Default.Show();
                            this.Hide();

                        }
                        else if (Type == "Block")
                        {
                            MessageBox.Show("Your account is currently Block");
                            MessageBox.Show("Contact the Administrator for Help");

                        }
                        else
                        {
                            MessageBox.Show("Welcome " + Fullname);

                            Mainmenu.Default.lblname.Text = Fullname;
                            Mainmenu.Default.lblaccno.Text = accno;
                            Mainmenu.Default.Show();
                            this.Hide();

                        }

                    }
                    else
                    {
                        MessageBox.Show("Yuo are Not Registered!!!");
                        MessageBox.Show("Pls Register if You are New!");


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            txtpin.Text = "";


        }

        public void Llblreg_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) => _ = Atmsystem.Regs_frm.Default;

        public void Login_frm_Load(System.Object sender, System.EventArgs e)
        {

        }

        public void GroupBox1_Enter(System.Object sender, System.EventArgs e)
        {

        }

        private void Txtpin_TextChanged(object sender, EventArgs e)
        {

        }

        public override bool Equals(object obj) => obj is Login_frm frm && EqualityComparer<DataSet>.Default.Equals(ds, frm.ds);
        public override int GetHashCode() => base.GetHashCode();
        public override object InitializeLifetimeService() => base.InitializeLifetimeService();
        public override ObjRef CreateObjRef(Type requestedType) => base.CreateObjRef(requestedType);
        protected override object GetService(Type service) => base.GetService(service);
        protected override AccessibleObject GetAccessibilityObjectById(int objectId) => base.GetAccessibilityObjectById(objectId);
        public override Size GetPreferredSize(Size proposedSize) => base.GetPreferredSize(proposedSize);
        protected override AccessibleObject CreateAccessibilityInstance() => base.CreateAccessibilityInstance();
        protected override void DestroyHandle() => base.DestroyHandle();
        protected override void InitLayout() => base.InitLayout();
        protected override bool IsInputChar(char charCode) => base.IsInputChar(charCode);
        protected override bool IsInputKey(Keys keyData) => base.IsInputKey(keyData);
        protected override void NotifyInvalidate(Rectangle invalidatedArea) => base.NotifyInvalidate(invalidatedArea);
        protected override void OnAutoSizeChanged(EventArgs e) => base.OnAutoSizeChanged(e);
        protected override void OnBackColorChanged(EventArgs e) => base.OnBackColorChanged(e);
        protected override void OnBindingContextChanged(EventArgs e) => base.OnBindingContextChanged(e);
        protected override void OnCausesValidationChanged(EventArgs e) => base.OnCausesValidationChanged(e);
        protected override void OnContextMenuChanged(EventArgs e) => base.OnContextMenuChanged(e);
        protected override void OnContextMenuStripChanged(EventArgs e) => base.OnContextMenuStripChanged(e);
        protected override void OnCursorChanged(EventArgs e) => base.OnCursorChanged(e);
        protected override void OnDockChanged(EventArgs e) => base.OnDockChanged(e);
        protected override void OnForeColorChanged(EventArgs e) => base.OnForeColorChanged(e);
        protected override void OnNotifyMessage(Message m) => base.OnNotifyMessage(m);
        protected override void OnParentBackColorChanged(EventArgs e) => base.OnParentBackColorChanged(e);
        protected override void OnParentBackgroundImageChanged(EventArgs e) => base.OnParentBackgroundImageChanged(e);
        protected override void OnParentBindingContextChanged(EventArgs e) => base.OnParentBindingContextChanged(e);
        protected override void OnParentCursorChanged(EventArgs e) => base.OnParentCursorChanged(e);
        protected override void OnParentEnabledChanged(EventArgs e) => base.OnParentEnabledChanged(e);
        protected override void OnParentFontChanged(EventArgs e) => base.OnParentFontChanged(e);
        protected override void OnParentForeColorChanged(EventArgs e) => base.OnParentForeColorChanged(e);
        protected override void OnParentRightToLeftChanged(EventArgs e) => base.OnParentRightToLeftChanged(e);
        protected override void OnParentVisibleChanged(EventArgs e) => base.OnParentVisibleChanged(e);
        protected override void OnPrint(PaintEventArgs e) => base.OnPrint(e);
        protected override void OnTabIndexChanged(EventArgs e) => base.OnTabIndexChanged(e);
        protected override void OnTabStopChanged(EventArgs e) => base.OnTabStopChanged(e);
        protected override void OnClick(EventArgs e) => base.OnClick(e);
        protected override void OnClientSizeChanged(EventArgs e) => base.OnClientSizeChanged(e);
        protected override void OnControlAdded(ControlEventArgs e) => base.OnControlAdded(e);
        protected override void OnControlRemoved(ControlEventArgs e) => base.OnControlRemoved(e);
        protected override void OnLocationChanged(EventArgs e) => base.OnLocationChanged(e);
        protected override void OnDoubleClick(EventArgs e) => base.OnDoubleClick(e);
        protected override void OnDragEnter(DragEventArgs drgevent) => base.OnDragEnter(drgevent);
        protected override void OnDragOver(DragEventArgs drgevent) => base.OnDragOver(drgevent);
        protected override void OnDragLeave(EventArgs e) => base.OnDragLeave(e);
        protected override void OnDragDrop(DragEventArgs drgevent) => base.OnDragDrop(drgevent);
        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent) => base.OnGiveFeedback(gfbevent);
        protected override void OnGotFocus(EventArgs e) => base.OnGotFocus(e);
        protected override void OnHelpRequested(HelpEventArgs hevent) => base.OnHelpRequested(hevent);
        protected override void OnInvalidated(InvalidateEventArgs e) => base.OnInvalidated(e);
        protected override void OnKeyDown(KeyEventArgs e) => base.OnKeyDown(e);
        protected override void OnKeyPress(KeyPressEventArgs e) => base.OnKeyPress(e);
        protected override void OnKeyUp(KeyEventArgs e) => base.OnKeyUp(e);
        protected override void OnLeave(EventArgs e) => base.OnLeave(e);
        protected override void OnLostFocus(EventArgs e) => base.OnLostFocus(e);
        protected override void OnMarginChanged(EventArgs e) => base.OnMarginChanged(e);
        protected override void OnMouseDoubleClick(MouseEventArgs e) => base.OnMouseDoubleClick(e);
        protected override void OnMouseClick(MouseEventArgs e) => base.OnMouseClick(e);
        protected override void OnMouseCaptureChanged(EventArgs e) => base.OnMouseCaptureChanged(e);
        protected override void OnMouseDown(MouseEventArgs e) => base.OnMouseDown(e);
        protected override void OnMouseEnter(EventArgs e) => base.OnMouseEnter(e);
        protected override void OnMouseLeave(EventArgs e) => base.OnMouseLeave(e);
        protected override void OnMouseHover(EventArgs e) => base.OnMouseHover(e);
        protected override void OnMouseMove(MouseEventArgs e) => base.OnMouseMove(e);
        protected override void OnMouseUp(MouseEventArgs e) => base.OnMouseUp(e);
        protected override void OnMove(EventArgs e) => base.OnMove(e);
        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent) => base.OnQueryContinueDrag(qcdevent);
        protected override void OnRegionChanged(EventArgs e) => base.OnRegionChanged(e);
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e) => base.OnPreviewKeyDown(e);
        protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);
        protected override void OnChangeUICues(UICuesEventArgs e) => base.OnChangeUICues(e);
        protected override void OnSystemColorsChanged(EventArgs e) => base.OnSystemColorsChanged(e);
        protected override void OnValidating(CancelEventArgs e) => base.OnValidating(e);
        protected override void OnValidated(EventArgs e) => base.OnValidated(e);
        public override bool PreProcessMessage(ref Message msg) => base.PreProcessMessage(ref msg);
        protected override bool ProcessKeyEventArgs(ref Message m) => base.ProcessKeyEventArgs(ref m);
        protected override bool ProcessKeyMessage(ref Message m) => base.ProcessKeyMessage(ref m);
        public override void ResetBackColor() => base.ResetBackColor();
        public override void ResetCursor() => base.ResetCursor();
        public override void ResetFont() => base.ResetFont();
        public override void ResetForeColor() => base.ResetForeColor();
        public override void ResetRightToLeft() => base.ResetRightToLeft();
        public override void Refresh() => base.Refresh();
        public override void ResetText() => base.ResetText();
        protected override Size SizeFromClientSize(Size clientSize) => base.SizeFromClientSize(clientSize);
        protected override void OnImeModeChanged(EventArgs e) => base.OnImeModeChanged(e);
        protected override void OnMouseWheel(MouseEventArgs e) => base.OnMouseWheel(e);
        protected override void OnRightToLeftChanged(EventArgs e) => base.OnRightToLeftChanged(e);
        protected override void OnPaintBackground(PaintEventArgs e) => base.OnPaintBackground(e);
        protected override void OnPaddingChanged(EventArgs e) => base.OnPaddingChanged(e);
        protected override Point ScrollToControl(Control activeControl) => base.ScrollToControl(activeControl);
        protected override void OnScroll(ScrollEventArgs se) => base.OnScroll(se);
        protected override void OnAutoValidateChanged(EventArgs e) => base.OnAutoValidateChanged(e);
        protected override void OnParentChanged(EventArgs e) => base.OnParentChanged(e);
        protected override void SetVisibleCore(bool value) => base.SetVisibleCore(value);
        protected override void AdjustFormScrollbars(bool displayScrollbars) => base.AdjustFormScrollbars(displayScrollbars);
        protected override Control.ControlCollection CreateControlsInstance() => base.CreateControlsInstance();
        protected override void CreateHandle() => base.CreateHandle();
        protected override void DefWndProc(ref Message m) => base.DefWndProc(ref m);
        protected override bool ProcessMnemonic(char charCode) => base.ProcessMnemonic(charCode);
        protected override void OnActivated(EventArgs e) => base.OnActivated(e);
        protected override void OnBackgroundImageChanged(EventArgs e) => base.OnBackgroundImageChanged(e);
        protected override void OnBackgroundImageLayoutChanged(EventArgs e) => base.OnBackgroundImageLayoutChanged(e);
        protected override void OnClosing(CancelEventArgs e) => base.OnClosing(e);
        protected override void OnClosed(EventArgs e) => base.OnClosed(e);
        protected override void OnFormClosing(FormClosingEventArgs e) => base.OnFormClosing(e);
        protected override void OnFormClosed(FormClosedEventArgs e) => base.OnFormClosed(e);
        protected override void OnCreateControl() => base.OnCreateControl();
        protected override void OnDeactivate(EventArgs e) => base.OnDeactivate(e);
        protected override void OnEnabledChanged(EventArgs e) => base.OnEnabledChanged(e);
        protected override void OnEnter(EventArgs e) => base.OnEnter(e);
        protected override void OnFontChanged(EventArgs e) => base.OnFontChanged(e);
        protected override void OnHandleCreated(EventArgs e) => base.OnHandleCreated(e);
        protected override void OnHandleDestroyed(EventArgs e) => base.OnHandleDestroyed(e);
        protected override void OnHelpButtonClicked(CancelEventArgs e) => base.OnHelpButtonClicked(e);
        protected override void OnLayout(LayoutEventArgs levent) => base.OnLayout(levent);
        protected override void OnLoad(EventArgs e) => base.OnLoad(e);
        protected override void OnMaximizedBoundsChanged(EventArgs e) => base.OnMaximizedBoundsChanged(e);
        protected override void OnMaximumSizeChanged(EventArgs e) => base.OnMaximumSizeChanged(e);
        protected override void OnMinimumSizeChanged(EventArgs e) => base.OnMinimumSizeChanged(e);
        protected override void OnInputLanguageChanged(InputLanguageChangedEventArgs e) => base.OnInputLanguageChanged(e);
        protected override void OnInputLanguageChanging(InputLanguageChangingEventArgs e) => base.OnInputLanguageChanging(e);
        protected override void OnVisibleChanged(EventArgs e) => base.OnVisibleChanged(e);
        protected override void OnMdiChildActivate(EventArgs e) => base.OnMdiChildActivate(e);
        protected override void OnMenuStart(EventArgs e) => base.OnMenuStart(e);
        protected override void OnMenuComplete(EventArgs e) => base.OnMenuComplete(e);
        protected override void OnPaint(PaintEventArgs e) => base.OnPaint(e);
        protected override void OnResize(EventArgs e) => base.OnResize(e);
        protected override void OnRightToLeftLayoutChanged(EventArgs e) => base.OnRightToLeftLayoutChanged(e);
        protected override void OnShown(EventArgs e) => base.OnShown(e);
        protected override void OnTextChanged(EventArgs e) => base.OnTextChanged(e);
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) => base.ProcessCmdKey(ref msg, keyData);
        protected override bool ProcessDialogKey(Keys keyData) => base.ProcessDialogKey(keyData);
        protected override bool ProcessDialogChar(char charCode) => base.ProcessDialogChar(charCode);
        protected override bool ProcessKeyPreview(ref Message m) => base.ProcessKeyPreview(ref m);
        protected override bool ProcessTabKey(bool forward) => base.ProcessTabKey(forward);
        protected override void Select(bool directed, bool forward) => base.Select(directed, forward);
        protected override void ScaleCore(float x, float y) => base.ScaleCore(x, y);
        protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified) => base.GetScaledBounds(bounds, factor, specified);
        protected override void ScaleControl(SizeF factor, BoundsSpecified specified) => base.ScaleControl(factor, specified);
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) => base.SetBoundsCore(x, y, width, height, specified);
        protected override void SetClientSizeCore(int x, int y) => base.SetClientSizeCore(x, y);
        public override string ToString() => base.ToString();
        protected override void UpdateDefaultButton() => base.UpdateDefaultButton();
        protected override void OnResizeBegin(EventArgs e) => base.OnResizeBegin(e);
        protected override void OnResizeEnd(EventArgs e) => base.OnResizeEnd(e);
        protected override void OnStyleChanged(EventArgs e) => base.OnStyleChanged(e);
        public override bool ValidateChildren() => base.ValidateChildren();
        public override bool ValidateChildren(ValidationConstraints validationConstraints) => base.ValidateChildren(validationConstraints);
        protected override void WndProc(ref Message m) => base.WndProc(ref m);
    }

}
