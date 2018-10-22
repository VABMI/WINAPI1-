/*
Copyright (C) 2004  Daniel Grunwald
Modified by Jacquelin Potier <jacquelin.potier@free.fr>

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; version 2 of the License.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
*/
using System;
using System.Drawing;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Tools.GUI.Controls.HexViewer
{
	/// <summary>
	/// Description of HexViewer.
	/// </summary>
	public class HexViewer : System.Windows.Forms.UserControl
	{
		#region Constructor
    	Tools.GUI.Components.RichTextBoxSynchronized textView = new Tools.GUI.Components.RichTextBoxSynchronized();
    	Tools.GUI.Components.RichTextBoxSynchronized hexView = new Tools.GUI.Components.RichTextBoxSynchronized();
        Tools.GUI.Components.RichTextBoxSynchronized addrView = new Tools.GUI.Components.RichTextBoxSynchronized();

        System.Collections.ArrayList al_data_infos=new System.Collections.ArrayList();// contains Cdata_infos

        MenuItem menuItem_copy=new MenuItem("Copy");
        MenuItem menuItem_clear=new MenuItem("Clear");
        MenuItem menuItem_save=new MenuItem("Save");
        ContextMenu context_menu=new System.Windows.Forms.ContextMenu();
        RichTextBox rtb_last_clicked;
        float fontWidth = 7;
        
		/// <summary>
		/// Creates a new HexViewer
		/// </summary>
		public HexViewer()
		{
            context_menu.MenuItems.AddRange(new MenuItem[]{this.menuItem_copy,this.menuItem_clear,this.menuItem_save});
            menuItem_copy.Click += new System.EventHandler(this.menuItem_copy_Click);
            menuItem_clear.Click+=new EventHandler(menuItem_clear_Click);
            menuItem_save.Click+=new EventHandler(this.menuItem_save_Click);
            rtb_last_clicked=hexView;
            
			Font f = new Font("Courier New", 8.25F, FontStyle.Regular);
			hexView.Dock = System.Windows.Forms.DockStyle.Left;
            hexView.ScrollBars = RichTextBoxScrollBars.None;
			hexView.TabIndex = 1;
			hexView.WordWrap = false;
			hexView.Font = f;
			
			hexView.AutoWordSelection = false;
			textView.AutoWordSelection = false;
			
			textView.WordWrap = false;
			textView.Dock = System.Windows.Forms.DockStyle.Fill;
			textView.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			textView.TabIndex = 2;
			textView.Font = f;
			textView.DetectUrls = false;
			
			hexView.HideSelection = false;
			textView.HideSelection = false;

            addrView.Dock = System.Windows.Forms.DockStyle.Left;
            addrView.HideSelection = false;
            addrView.ScrollBars = RichTextBoxScrollBars.None;
            addrView.WordWrap = false;
            addrView.Font = f;
            addrView.Width=65;// depending of format


			hexView.SelectionChanged  += new EventHandler(HexSelectionChanged);
			textView.SelectionChanged += new EventHandler(TextSelectionChanged);
            addrView.SelectionChanged+=new EventHandler(addrView_SelectionChanged);

            hexView.MouseUp+=new MouseEventHandler(RichTextBox_MouseUp);
            textView.MouseUp+=new MouseEventHandler(RichTextBox_MouseUp);
			
            addrView.ReadOnly = true;
			hexView.ReadOnly = true;
			textView.ReadOnly = true;
			
            addrView.AddSynchronizedControl(hexView);
            addrView.AddSynchronizedControl(textView);

            hexView.AddSynchronizedControl(addrView);
            hexView.AddSynchronizedControl(textView);

            textView.AddSynchronizedControl(addrView);
            textView.AddSynchronizedControl(hexView);

			this.Controls.Add(textView);
			this.Controls.Add(hexView);
            this.Controls.Add(addrView);
			this.Size = new System.Drawing.Size(424, 280);

		}
		#endregion
		
		void Check()
		{
			if (IsDisposed)
				throw new ObjectDisposedException("HexViewer");
			if (InvokeRequired)
				throw new Exception("Invoke required");
		}
		
		#region Overridden Events
		/// <summary>
		/// Raises the <see cref="Control.SizeChanged">SizeChanged</see> event.
		/// </summary>
		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged(e);
            if (visible_addr)
			    hexView.Width = (int)((ClientSize.Width-addrView.Width) * 0.65);
            else
                hexView.Width = (int)(ClientSize.Width * 0.65);
			UpdateView(false,false);
		}
	
	
		private void TextSelectionChanged(object sender, EventArgs e)
		{
			if (ignoreSelChange) return;
			int pos, row, col;
			pos = textView.SelectionStart;
			row = pos / (byteCount + 1);
			col = pos % (byteCount + 1);
			int startByte = row * byteCount + col;
			
			pos += textView.SelectionLength;
			row = pos / (byteCount + 1);
			col = pos % (byteCount + 1);
			int endByte = row * byteCount + col;
			Select(startByte, endByte - startByte, 1);
		}
		
		private void HexSelectionChanged(object sender, EventArgs e)
		{
			if (ignoreSelChange) return;
			int pos, row;
			pos = hexView.SelectionStart;
			row = pos / (lineChars + 1);
			int startByte = row * byteCount + calcByteCol(pos);
			
			pos += hexView.SelectionLength;
			row = pos / (lineChars + 1);
			int endByte = row * byteCount + calcByteCol(pos);
			Select(startByte, endByte - startByte, 2);
		}

        private void addrView_SelectionChanged(object sender, EventArgs e)
        {
            if (ignoreSelChange) return;
            int pos, row;
            pos = addrView.SelectionStart;
            row=pos/9;//8+\n
            Select(row * byteCount, 0, 0);
        }
		
		int calcByteCol(int pos) {
			int col = pos % (lineChars + 1);
			int bcol = 0;
			while(col > 0) {
				bcol += 1;
				col -= 2;
				if (bcol % byteCount == 0)
					col -= 1;
				else if (blockLength > 0 && bcol % blockLength == 0)
					col -= 1;
			}
			return bcol;
		}
		#endregion
		
		#region Properties / members

        private System.Threading.AutoResetEvent evt_al_data_infos_unlocked=new System.Threading.AutoResetEvent(true);

        bool b_scroll_to_end=false;
        public bool ScrollToEnd
        {
            get
            {
                return this.b_scroll_to_end;
            }
            set
            {
                this.b_scroll_to_end=value;
            }
        }

		bool b_autorefresh=true;
        public bool AutoRefresh
        {
            get
            {
                return this.b_autorefresh;
            }
            set
            {
                this.b_autorefresh=value;
            }
        }
        bool ignoreSelChange;
        Encoding enc = Encoding.GetEncoding("ISO8859-1");
        int old_al_data_infos_count=0;

		/// <summary>
		/// Sets the data to display.
		/// </summary>
		/// <exception cref="InvokeRequiredException">This method is called from an other thread than the thread that created this control.</exception>
		/// <exception cref="ObjectDisposedException">This control was already disposed.</exception>
		public byte[] Data {
			set {
                al_data_infos.Clear();
                old_al_data_infos_count=0;
                if (value==null)
                    return;
                this.AddData(value);
			}
		}
		
		int blockLength = 2;
		
		/// <summary>
		/// Gets/Sets the length of a block of connected bytes. After each block,
		/// a additional space is inserted in the hex view. Line breaks are only
		/// possible after a full block.<br/>
		/// If this property is changed to 0 or a negative value, byte blocks are
		/// disabled.
		/// </summary>
		/// <exception cref="InvokeRequiredException">This method is called from an other thread than the thread that created this control.</exception>
		/// <exception cref="ObjectDisposedException">This control was already disposed.</exception>
		public int BlockLength {
			get {
				return blockLength;
			}
			set {
				if (value == blockLength) return;
				Check();
				blockLength = value;
				UpdateView(true,false);
			}
		}

        bool visible_addr=true;

        /// <summary>
        /// Gets/Sets a value indicating whether the address RichTextBox is displayed.
        /// </summary>
        /// <exception cref="InvokeRequiredException">This method is called from an other thread than the thread that created this control.</exception>
        /// <exception cref="ObjectDisposedException">This control was already disposed.</exception>
        public bool VisibleAddr
        {
            get 
            {
                return visible_addr;
            }
            set 
            {
                if (value == visible_addr) return;
                Check();
                visible_addr = value;
                // comput new size for RichTextBox
                OnSizeChanged(EventArgs.Empty);
                UpdateView(true,false);
            }
        }
		#endregion
		
		#region Selection
        private void GoEnd()
        {
            ignoreSelChange=true;
            hexView.SelectionStart=hexView.Text.Length;
            hexView.ScrollToCaret();
            textView.SelectionStart=hexView.Text.Length;
            textView.ScrollToCaret();
            addrView.SelectionStart=hexView.Text.Length;
            addrView.ScrollToCaret();
            ignoreSelChange=false;
        }

		int selectionStart;
		int selectionLength;
		
		/// <summary>
		/// Selects the specified range.
		/// </summary>
		/// <param name="start">The start index of the selection.</param>
		/// <param name="length">The length of the selection.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// The specified range is not a valid range for the current data block.
		/// </exception>
		/// <exception cref="InvokeRequiredException">This method is called from an other thread than the thread that created this control.</exception>
		/// <exception cref="ObjectDisposedException">This control was already disposed.</exception>
		public void Select(int start, int length)
		{
			Check();
            int len=textView.Text.Length;

			if (start < 0 || start > len)
				throw new ArgumentOutOfRangeException("start", start, "start must be between 0 and " + len.ToString());
			if (length < 0 || start + length > len)
				throw new ArgumentOutOfRangeException("length", length, "length must be between 0 and " + (len - start).ToString());

			Select(start, length, 0);
		}
		
		void Select(int start, int length, int mode)
		{
			bool dirty;
			if (start != selectionStart || length != selectionLength)
				dirty = true;
			else
				dirty = false;
			selectionStart = start;
			selectionLength = length;
			
			int row1 = start / byteCount;
			int col1 = start % byteCount;
			int row2 = (start + length) / byteCount;
			int col2 = (start + length) % byteCount;
			
			int textStart = row1 * (byteCount + 1) + col1;
			int textEnd   = row2 * (byteCount + 1) + col2;
			
			
			int hexStart, hexEnd;
			if (blockLength > 0) {
				int group1 = col1 / blockLength;
				int group2 = col2 / blockLength;
				hexStart = row1 * (lineChars + 1) + col1 * 2 + group1;
				hexEnd   = row2 * (lineChars + 1) + col2 * 2 + group2;
			} else {
				hexStart = row1 * (lineChars + 1) + col1 * 2;
				hexEnd   = row2 * (lineChars + 1) + col2 * 2;
			}
			
			ignoreSelChange = true;
			if (mode != 1) textView.Select(textStart, textEnd - textStart);
			if (mode != 2) hexView.Select(hexStart,   hexEnd -  hexStart);
            addrView.SelectionStart=row2*9;//8+\n
			ignoreSelChange = false;
			if (dirty)
				OnSelectionChanged(EventArgs.Empty);
		}
		
		/// <summary>
		/// Gets/Sets the start position of the selection.
		/// </summary>
		/// <exception cref="InvokeRequiredException">This method is called from an other thread than the thread that created this control.</exception>
		/// <exception cref="ObjectDisposedException">This control was already disposed.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// The specified start position is outside the current data block.
		/// </exception>
		public int SelectionStart {
			get {
				return selectionStart;
			}
			set {
                int len=textView.Text.Length;
				if (value + selectionLength > len)
					Select(value, len - value);
				else
					Select(value, selectionLength);
			}
		}
		
		/// <summary>
		/// Gets/Sets the length of the selection.
		/// </summary>
		/// <exception cref="InvokeRequiredException">This method is called from an other thread than the thread that created this control.</exception>
		/// <exception cref="ObjectDisposedException">This control was already disposed.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// The specified range is not a valid range for the current data block.
		/// </exception>
		public int SelectionLength {
			get {
				return selectionLength;
			}
			set {
				Select(selectionStart, value);
			}
		}
		#endregion

        #region methodes to add data or comments

        public void AddComment(string comment)
        {
            this.AddComment(comment,System.Drawing.Color.Black);
        }

        public void AddComment(string comment, System.Drawing.Color color)
        {
            // remove \r\n to avoid troubles
            string[] str_array=comment.Replace("\r\n","\n").Split(new char[]{'\r','\n'});
            this.evt_al_data_infos_unlocked.WaitOne(1000,false);
            for (int cnt=0;cnt<str_array.Length;cnt++)
                this.al_data_infos.Add(new Cdata_infos(enc.GetBytes(str_array[cnt]),true,color));
            this.evt_al_data_infos_unlocked.Set();
            this.UpdateData();
        }

        public void AddData(byte[] data)
        {
            this.AddData(data,System.Drawing.Color.Black);
        }

        public void AddData(byte[] data,System.Drawing.Color color)
        {
            if (data==null)
                return;
            this.evt_al_data_infos_unlocked.WaitOne(1000,false);
            this.al_data_infos.Add(new Cdata_infos(data,false,color));
            this.evt_al_data_infos_unlocked.Set();
            this.UpdateData();
        }

        private void UpdateData()
        {
            // post message to avoid Invoke Exception 
            Int16 wparam=0;
            Int32 lparam=0;
            // avoid multithreading troubles
            SendMessage(this.Handle,MsgUpdateData,ref wparam,ref lparam);
        }

        public void Clear()
        {
            al_data_infos.Clear();
            old_al_data_infos_count=0;
            this.UpdateData();
        }
        #endregion

		#region UpdateView

        /// <summary>
        /// Needs to be call only if AutoRefresh is put to false
        /// this function is thread safe
        /// </summary>
        public void RefreshData()
        {
            // post message to avoid Invoke Exception 
            Int16 wparam=0;
            Int32 lparam=0;
            // avoid multithreading troubles
            SendMessage(this.Handle,MsgRefreshData,ref wparam,ref lparam);
        }

		int byteCount = 1; // number of bytes in one line
		int lineChars = 1; // number of hex characters in each line
		
		void UpdateView(bool force,bool adding_data)
		{
            this.evt_al_data_infos_unlocked.WaitOne(1000,false);
            System.Collections.ArrayList al_data=this.al_data_infos;// makes local copy to free al_data_infos use
            this.evt_al_data_infos_unlocked.Set();
			if (al_data.Count==0) {
				hexView.Clear();
				textView.Clear();
                addrView.Clear();
				return;
			}

            addrView.Visible=visible_addr;

            float avaSize = hexView.Width - 5;

			int charCount = (int)(avaSize / fontWidth);
			int oldByteCount = byteCount;
			if (blockLength > 0) {
				byteCount = (charCount + 1) / (blockLength * 2 + 1) * blockLength;
				if (byteCount == 0)
					byteCount = blockLength;
				lineChars = byteCount * 2 + byteCount / blockLength - 1;
			} else {
				byteCount = charCount / 2;
				if (byteCount == 0)
					byteCount = 1;
				lineChars = byteCount * 2;
			}
			if (!force && oldByteCount == byteCount) return;

            if (!adding_data)// all should be redraw
                old_al_data_infos_count=0;

            int savedSelectionLength=this.textView.SelectionLength;

            Cdata_infos data_infos;
            int fill_size;
            int i;
            byte by;
            int i_line_num;
            int associated_al_data_infos_element_selection=-1;
            int selection_start_from_al_data_infos_element_start=-1;
            int cnt;
            //////////////////////
            // get selection length and selection start.
            // selection start is relative from begining of element
            //////////////////////
            for (cnt=0;cnt<al_data.Count;cnt++)
            {
                if (this.textView.SelectionStart<((Cdata_infos)al_data[cnt]).text_part.start)
                {
                    associated_al_data_infos_element_selection=cnt-1;
                    selection_start_from_al_data_infos_element_start=this.textView.SelectionStart-((Cdata_infos)al_data[associated_al_data_infos_element_selection]).text_part.start;
                    // remove delta of \n
                    selection_start_from_al_data_infos_element_start-=selection_start_from_al_data_infos_element_start/oldByteCount;
                    savedSelectionLength-=(selection_start_from_al_data_infos_element_start+savedSelectionLength)/oldByteCount-selection_start_from_al_data_infos_element_start/oldByteCount;
                    break;
                }
            }

            //////////////////////
            // Text and Hexa text preparation
            //////////////////////
            for (cnt=this.old_al_data_infos_count;cnt<al_data.Count;cnt++)
            {
                data_infos=(Cdata_infos)al_data[cnt];
                StringBuilder b = new StringBuilder();
                StringBuilder b2 = new StringBuilder();

                for (i = 0; i < data_infos.data.Length; i++) 
                {
                    if (i > 0) 
                    {
                        if (i % byteCount == 0) 
                        {
                            b.Append('\n');
                            b2.Append('\n');
                        } 
                        else if ((blockLength > 0) && (i % blockLength == 0)) 
                        {
                            b.Append(' ');
                        }
                    }
                    by = data_infos.data[i];
                    if (data_infos.b_comment)
                    {
                        b.Append((char)by);
                        b.Append(' ');// to keep synchro when selected between ascii and hexa
                    }
                    else
                        b.Append(by.ToString("X2"));
                    if (by >= 32 && by < 127)
                        b2.Append((char)by);
                    else if (by == 128)
                        b2.Append('€');
                    else if (by > 160)
                        b2.Append(enc.GetChars(new byte[] {by})[0]);
                    else
                        b2.Append('.');
                }

                // add blank data if required to keep select work width text and hexa box
                if ((data_infos.data.Length==0)||(data_infos.data.Length % byteCount!=0))
                {
                    if (data_infos.data.Length==0)
                        fill_size=byteCount;
                    else
                        fill_size=byteCount-(data_infos.data.Length % byteCount);
                    if (fill_size>0)
                    {
                        if (blockLength>0)
                        {
                            if (data_infos.data.Length==0)
                                b.Append(new string (' ',fill_size*2+fill_size/blockLength-1));
                            else
                                b.Append(new string (' ',fill_size*2+fill_size/blockLength));
                        }
                        else
                            b.Append(new string (' ',fill_size*2));
                        b2.Append(new string (' ',fill_size));
                    }
                }

                //////////////////////
                // calculate address
                //////////////////////
                StringBuilder baddr = new StringBuilder();
                for (i_line_num=0;i_line_num<(data_infos.data.Length/byteCount);i_line_num++)
                {
                    baddr.Append((i_line_num*byteCount).ToString("X8"));
                    if ((i_line_num<(data_infos.data.Length/byteCount)-1)||((data_infos.data.Length%byteCount)!=0))
                        baddr.Append("\n");
                }
                if (((data_infos.data.Length%byteCount)!=0)||(data_infos.data.Length==0))
                {
                    if (i_line_num==0)
                        baddr.Append(0.ToString("X8"));
                    else
                        baddr.Append(((i_line_num+1)*byteCount).ToString("X8"));
                }

                //////////////////////
                // adding data
                //////////////////////
                if (cnt==0)
                {
                    data_infos.hexa_part.start=0;
                    data_infos.text_part.start=0;
                    data_infos.addr_part.start=0;

                    hexView.Clear();
                    textView.Clear();
                    addrView.Clear();
                }
                else

                {
                    data_infos.hexa_part.start=hexView.Text.Length+1;
                    data_infos.text_part.start=textView.Text.Length+1;
                    data_infos.addr_part.start=addrView.Text.Length+1;

                    hexView.AppendText("\n");
                    textView.AppendText("\n");
                    addrView.AppendText("\n");
                }

                // the following is used only to make html
                data_infos.hexa_part.str_data=b.ToString();
                data_infos.text_part.str_data=b2.ToString();
                data_infos.addr_part.str_data=baddr.ToString();

                hexView.AppendText(data_infos.hexa_part.str_data);
                textView.AppendText(data_infos.text_part.str_data);
                addrView.AppendText(data_infos.addr_part.str_data);
            }

            //////////////////////
            // applying colors
            //////////////////////
            ignoreSelChange=true;
            addrView.HideSelection=true;
            textView.HideSelection=true;
            hexView.HideSelection=true;

            // play with color only after having added full text (else we get some troubles with color)
            for (cnt=this.old_al_data_infos_count;cnt<al_data.Count;cnt++)
            {
                data_infos=(Cdata_infos)al_data[cnt];
                hexView.Select(data_infos.hexa_part.start,data_infos.hexa_part.str_data.Length);
                hexView.SelectionColor=data_infos.color;
                textView.Select(data_infos.text_part.start,data_infos.text_part.str_data.Length);
                textView.SelectionColor=data_infos.color;
                addrView.Select(data_infos.addr_part.start,data_infos.addr_part.str_data.Length);
                addrView.SelectionColor=data_infos.color;
            }
            this.old_al_data_infos_count=al_data.Count;

            hexView.SelectionLength=0;
            textView.SelectionLength=0;
            addrView.SelectionLength=0;

            if (al_data.Count>1)
            {
                hexView.SelectionStart=hexView.Text.Length;
                textView.SelectionStart=textView.Text.Length;
                addrView.SelectionStart=addrView.Text.Length;
            }
            
            addrView.HideSelection=false;
            textView.HideSelection=false;
            hexView.HideSelection=false;
            ignoreSelChange=false;

            //////////////////////
            /// Restoring selection
            //////////////////////
            if (associated_al_data_infos_element_selection>-1)
            {
                // insert delta due to \n
                selection_start_from_al_data_infos_element_start+=selection_start_from_al_data_infos_element_start/byteCount;
                savedSelectionLength+=(selection_start_from_al_data_infos_element_start+savedSelectionLength)/byteCount-selection_start_from_al_data_infos_element_start/byteCount;
                this.textView.Select(
                    selection_start_from_al_data_infos_element_start+((Cdata_infos)al_data[associated_al_data_infos_element_selection]).text_part.start,
                    savedSelectionLength);
            }

            //////////////////////
            /// Restoring not autotwordselection 
            //////////////////////
            hexView.AutoWordSelection = false;
            textView.AutoWordSelection = false;

		}
		#endregion
		
        #region html generation
        public string GetHtmlCode()
        {
            return this.GetHtmlCode(true);
        }

        public string GetHtmlCode(bool include_headers)
        {
            StringBuilder strb=new StringBuilder();
            this.evt_al_data_infos_unlocked.WaitOne(1000,false);
            System.Collections.ArrayList al_data=this.al_data_infos;// makes local copy to free al_data_infos use
            this.evt_al_data_infos_unlocked.Set();
            Cdata_infos data_infos;
            if (include_headers)
                strb.Append("<html><body>\r\n");
            strb.Append("<table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" width=\"80%\">\r\n");// add line break to make html code understandable
            // add column headers
            strb.Append("<tr><td align=\"center\"><b>Address</b></td><td align=\"center\"><b>Hexa</b></td><td align=\"center\"><b>Text</b></td></tr>\r\n");
            for (int cnt=0;cnt<al_data.Count;cnt++)
            {
                data_infos=(Cdata_infos)al_data[cnt];

                // addr part
                strb.Append("<tr><td align=\"left\" nowrap>");
                // write something like <span style="color:'#FF00FF'">data</span>
                strb.Append("<span style=\"color:'#"+this.GetHtmlColor(data_infos.color)+"'\">");
                // we already have assume there's no \r
                strb.Append(data_infos.addr_part.str_data.Replace("\n","<br>").Replace(" ","&nbsp;"));
                strb.Append("</span></td>\r\n");

                // hexa data part
                strb.Append("<td align=\"left\" width=\"65%\" nowrap>&nbsp;");
                strb.Append("<span style=\"color:'#"+this.GetHtmlColor(data_infos.color)+"'\">");
                // we already have assume there's no \r
                if (data_infos.b_comment)
                    strb.Append(System.Web.HttpUtility.HtmlEncode(data_infos.hexa_part.str_data).Replace("\n","<br>&nbsp;").Replace(" ","&nbsp;"));
                else // no special chars
                    strb.Append(data_infos.hexa_part.str_data.Replace("\n","<br>&nbsp;").Replace(" ","&nbsp;"));
                strb.Append("</span></td>\r\n");

                // text data part
                strb.Append("<td align=\"left\" nowrap>&nbsp;");
                strb.Append("<span style=\"color:'#"+this.GetHtmlColor(data_infos.color)+"'\">");
                // we already have assume there's no \r
                strb.Append(System.Web.HttpUtility.HtmlEncode(data_infos.text_part.str_data).Replace("\n","<br>&nbsp;").Replace(" ","&nbsp;"));
                strb.Append("</span>");
                strb.Append("</td><td>");

                strb.Append("</tr>\r\n");// add line break to make html code understandable
            }
            strb.Append("</table>\r\n");// add line break to make html code understandable
            if (include_headers)
                strb.Append("</body></html>");

            return strb.ToString();
        }

        private string GetHtmlColor(System.Drawing.Color color)
        {
            int i_color=color.ToArgb();
            i_color&=0xFFFFFF;
            return i_color.ToString("X6");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>true on success, false on error</returns>
        public bool GenerateHtmlFile(string filename)
        {
            try
            {
                System.IO.FileStream        fs;
                if (System.IO.File.Exists(filename)) 
                    System.IO.File.Delete(filename);
                fs=new System.IO.FileStream(filename,
                    System.IO.FileMode.OpenOrCreate ,
                    System.IO.FileAccess.Write,
                    System.IO.FileShare.ReadWrite);
                System.IO.StreamWriter sw=new System.IO.StreamWriter(fs);
                sw.Write(this.GetHtmlCode());
                sw.Close();
                fs.Close();
                return true;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message,"Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Show an user interface for selecting file
        /// and generate html file.
        /// </summary>
        /// <returns>true if file is generated, false on error or on user cancel</returns>
        public bool GenerateHtmlFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt="html";
            saveFileDialog.Filter = "html files (*.html;*.htm)|*.html;*.htm|All files (*.*)|*.*"  ;
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.GenerateHtmlFile(saveFileDialog.FileName))
                {
                    System.Windows.Forms.MessageBox.Show("File successfully generated.","Information",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }
        #endregion

		#region Events

		/// <summary>
		/// Is raised whenever the <see cref="Data">data property</see> is changed.
		/// </summary>
		public event EventHandler DataChanged;
		
		/// <summary>
		/// Is raised whenever the selection is changed.
		/// </summary>
		public event EventHandler SelectionChanged;
		
		/// <summary>
		/// Raises the <see cref="DataChanged">DataChanged event</see>.
		/// </summary>
		protected virtual void OnDataChanged(EventArgs e) {
			if (DataChanged != null) {
				DataChanged(this, e);
			}
		}
		
		/// <summary>
		/// Raises the <see cref="SelectionChanged">SelectionChanged event</see>.
		/// </summary>
		protected virtual void OnSelectionChanged(EventArgs e) {
			if (SelectionChanged != null) {
				SelectionChanged(this, e);
			}
		}

        private void RichTextBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
            {
                this.rtb_last_clicked=(RichTextBox)sender;
                this.context_menu.Show((Control)sender,new System.Drawing.Point(e.X,e.Y));
            }        
        }
        private void menuItem_copy_Click(object sender, System.EventArgs e)
        {
            Clipboard.SetDataObject(this.rtb_last_clicked.SelectedText,true);
        }

        private void menuItem_save_Click(object sender, EventArgs e)
        {
            this.GenerateHtmlFile();
        }

        private void menuItem_clear_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        #endregion

        #region WndProc to allow multithreaded access of the Add_Data methode : with Msg all is done in the window thread
        // Message numbers in the third range (0x8000 through 0xBFFF) are available for application
        // to use as private messages. Message in this range do not conflict with system messages.
        private const UInt32 MsgUpdateData=0x8001;
        private const UInt32 MsgRefreshData=0x8002;
/*
        // don't wait msg processing before returning
        // speedest but some messages are lost :( we use sendmessage instead
        [ DllImport( "User32" ,SetLastError=true)]
        private static extern 
            Int32 PostMessage(IntPtr hWnd,
            UInt32 Msg,
            ref Int16 wParam,
            ref Int32 lParam
            );        
*/
        // wait msg processing before returning
        [ DllImport( "User32" ,SetLastError=true)]
        private static extern 
            Int32 SendMessage(IntPtr hWnd,
            UInt32 Msg,
            ref Int16 wParam,
            ref Int32 lParam
            ); 

        protected override void WndProc(ref Message uMsg)
        {
            switch(uMsg.Msg)
            {
                case (int)MsgUpdateData:
                    if (this.b_autorefresh)
                    {
                        Check();
                        Select(0, 0);
                        UpdateView(true,true);
                        OnDataChanged(EventArgs.Empty);
                        if (this.b_scroll_to_end)
                            this.GoEnd();
                    }
                    return;// don't let message be trasmit to base
                case (int)MsgRefreshData:
                    Check();
                    Select(0, 0);
                    UpdateView(true,true);
                    OnDataChanged(EventArgs.Empty);
                    if (this.b_scroll_to_end)
                        this.GoEnd();
                    return;// don't let message be trasmit to base
            }
            base.WndProc(ref uMsg);
        }
        #endregion

    }

    class Cpart_infos
    {
        public int start=0;
        public string str_data="";// added only for html after length field (length field becomes quite useless now)
        public Cpart_infos(){}
    }
    class Cdata_infos
    {
        public byte[] data=null;
        public bool   b_comment=false;
        public System.Drawing.Color color=System.Drawing.Color.Black;
        public Cpart_infos addr_part=new Cpart_infos();
        public Cpart_infos text_part=new Cpart_infos();
        public Cpart_infos hexa_part=new Cpart_infos();
        public Cdata_infos()
        {
        }
        public Cdata_infos(byte[] data,bool b_comment,System.Drawing.Color color)
        {
            this.color=color;
            this.b_comment=b_comment;
            this.data=new byte[data.Length];
            System.Array.Copy(data,0,this.data,0,data.Length);
        }
    }
}
