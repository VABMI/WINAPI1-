// Implements the manual sorting of items by columns.
class ListViewItemComparer : System.Collections.IComparer
{
    private int col;
    private bool b_sort_ascending;

    public ListViewItemComparer()
    {
        this.Reset();
    }

    public void Reset()
    {
        this.col = -1;
        this.b_sort_ascending=true;
    }

    public void set_column_number(int col)
    {
        if (this.col==col)
            this.b_sort_ascending=!this.b_sort_ascending;
        else
            this.b_sort_ascending=true;
        this.col=col;
    }

    public int Compare(object x, object y)
    {
        try
        {
            if ( (((System.Windows.Forms.ListViewItem)x).SubItems.Count<this.col)
                || (((System.Windows.Forms.ListViewItem)y).SubItems.Count<this.col))
                return 0;
            if (this.b_sort_ascending)
                return System.String.Compare(((System.Windows.Forms.ListViewItem)x).SubItems[this.col].Text, ((System.Windows.Forms.ListViewItem)y).SubItems[this.col].Text);
            // else
            return System.String.Compare(((System.Windows.Forms.ListViewItem)y).SubItems[this.col].Text,((System.Windows.Forms.ListViewItem)x).SubItems[this.col].Text);
        }
        catch
        {
            return 0;
        }
    }
}