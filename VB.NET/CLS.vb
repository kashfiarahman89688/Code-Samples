Imports System.Globalization

Public Class CLS
    Dim acct_no As String
    Public ar As String
    Public rs As New ADODB.Recordset
    Public rs1 As New ADODB.Recordset
    Public rs2 As New ADODB.Recordset

    Public Sub CLS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GroupBoxDetails.Enabled = False
        ButtonSave.Enabled = False
        ButtonCalculate.Enabled = False
        Me.Text = "CL(Staff Loan)-" + info
        'TODO: This line of code loads data into the 'DataSetCL3.VW_CL3' table. You can move, or remove it, as needed.

        Me.VW_CLSTableAdapter.SelectCommand.CommandText = GetParam("fillclS")
        Me.VW_CLSTableAdapter.Fill(DataSet7.VW_CLS, BranchCode, "%_____________", "%")
        GroupBoxDetails.Text = "Details - " + info
        If flag = True Then
            'Sql = GetParam("cat")
            'If rssql.State = 1 Then
            'rssql.Close()
            'End If
            'rssql.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset)
            'Do Until rssql.EOF
            'ComboBoxCat.Items.Add(rssql.Fields("SHRT").Value.ToString)
            'rssql.MoveNext()
            'Loop

            'Sql = GetParam("qj_sta")
            'If rssql.State = 1 Then
            'rssql.Close()
            'End If
            'rssql.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset)
            'Do Until rssql.EOF
            'Cmbqj.Items.Add(rssql.Fields("SHORT").Value.ToString)
            'rssql.MoveNext()
            'Loop
        End If
        flag = False

    End Sub

    Private Sub DataGridViewCL3_CellValueNeeded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles DataGridViewCL3.CellValueNeeded
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = Me.Serial.Index Then
            e.Value = e.RowIndex + 1
        End If
    End Sub



    Private Sub DataGridViewCL3_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridViewCL3.DoubleClick
        Try
            GroupBoxDetails.Enabled = True
            ButtonCalculate.Enabled = True
            ButtonDel.Enabled = True
            txtSta.Text = ""
            txtBasis.Text = ""
            txtBPAmt.Text = ""
            Dim acct As Array

            acct = DataGridViewCL3.SelectedRows(0).Cells(1).Value.ToString.Split("-")
            acct_no = acct(0) + acct(1) + acct(2)
            acct_no = acct_no.PadLeft(16, "0")

            If rssql.State = 1 Then
                rssql.Close()
            End If
            rssql.Open("select nvl(mark,'N') mark from cl_clmaster where ref_date = (select ref_date from cl_refdate where sta = '00') and acct = '" & acct_no & "'", conn, ADODB.CursorTypeEnum.adOpenKeyset)
            'MessageBox.Show("M")
            If rssql.Fields("mark").Value.ToString.Trim = "M" Then
                MsgBox("This A/c is already updated. Do you want to update again?", MsgBoxStyle.YesNo)
            End If

            If MsgBoxResult.Yes = MsgBoxResult.Yes Or rssql.Fields("MARK").Value.ToString.Trim = "N" Then

                If rssql.State = 1 Then
                    rssql.Close()
                End If

                rssql.Open("select nvl((Select nationalid from cl_clmaster where ref_date = (select ref_date from cl_refdate where sta = '00') and acct = '" & acct_no & "'),'') as nationalid from dual", conn, ADODB.CursorTypeEnum.adOpenKeyset)
                txtNationalID.Text = rssql.Fields("NATIONALID").Value.ToString.Trim
                If rs.State = 1 Then
                    rs.Close()
                End If
                Sql = "select remarks,nvl(basis,'N/A') as basis,(select short from cl_sta where sta = final_sta) as final_sta,nvl(base_of_provision,'0.00') as base_of_provision,(select short from cl_sta where sta = ob_sta) as ob_sta from cl_clmaster where entry_sta = '00' and" & _
                    " ref_date = (select ref_date from cl_refdate where sta = '00')" & _
                    "and acct ='" & acct_no & "'"
                rs.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset)
                'txtArrear.Text = Trim(DataGridViewCL3.SelectedRows(0).Cells(22).Value.ToString) + " Month/s"
                'TextBoxObjCri.Text = rs.Fields("OB_STA").Value.ToString
                'txtSta.Text = rs.Fields("FINAL_STA").Value.ToString
                'txtBasis.Text = rs.Fields("BASIS").Value.ToString
                txtBPAmt.Text = rs.Fields("BASE_OF_PROVISION").Value.ToString
                'TextBoxRemark.Text = rs.Fields("REMARKS").Value.ToString

                'MessageBox.Show(acct_no)
                LabelAccount.Visible = True
                LabelName.Visible = True
                LabelNatureLoan.Visible = True
                LabelOutstanding.Visible = True
                Label11.Visible = True

                LabelAccount.Text = DataGridViewCL3.SelectedRows(0).Cells(1).Value.ToString
                LabelName.Text = DataGridViewCL3.SelectedRows(0).Cells(2).Value.ToString
                LabelNatureLoan.Text = DataGridViewCL3.SelectedRows(0).Cells(3).Value.ToString
                LabelOutstanding.Text = FormatNumber(DataGridViewCL3.SelectedRows(0).Cells(4).Value.ToString, 2, TriState.True, TriState.False, TriState.False).ToString
                Label11.Text = DataGridViewCL3.SelectedRows(0).Cells(5).Value.ToString.Substring(6, 2) + "-" & _
                DataGridViewCL3.SelectedRows(0).Cells(5).Value.ToString.Substring(4, 2) + "-" & _
                +DataGridViewCL3.SelectedRows(0).Cells(5).Value.ToString.Substring(0, 4)
                'LabelQJ.Text = DataGridViewCL3.SelectedRows(0).Cells(6).Value.ToString


                txtResceduleNo.Text = DataGridViewCL3.SelectedRows(0).Cells(8).Value.ToString
                txtReschAmt.Text = FormatNumber(DataGridViewCL3.SelectedRows(0).Cells(12).Value.ToString, 2, TriState.True, TriState.False, TriState.False).ToString
                txtIntSus.Text = FormatNumber(DataGridViewCL3.SelectedRows(0).Cells(17).Value.ToString, 2, TriState.True, TriState.False, TriState.False).ToString
                txtEligSec.Text = FormatNumber(DataGridViewCL3.SelectedRows(0).Cells(19).Value.ToString, 0, TriState.True, TriState.False, TriState.False).ToString

                date_format(DataGridViewCL3.SelectedRows(0).Cells(10).Value.ToString)
                TextBoxday.Text = day
                TextBoxmon.Text = month
                TextBoxyear.Text = year
                Dim dt As String
                dt = DataGridViewCL3.SelectedRows(0).Cells(5).Value.ToString

                'MessageBox.Show(DataGridViewCL3.SelectedRows(0).Cells(15).Value.ToString)
                'If Trim(DataGridViewCL3.SelectedRows(0).Cells(15).Value.ToString) = "0" Then
                'ComboBoxCat.Text = "Select"
                'Else
                'ComboBoxCat.Text = DataGridViewCL3.SelectedRows(0).Cells(15).Value.ToString
                'End If
                TextBoxCat.Text = "STF"
                Cmbqj.Text = DataGridViewCL3.SelectedRows(0).Cells(6).Value.ToString
                ar = date_monthdiff(dt, RefDate)
                If ar < 0 Then
                    ar = 0
                End If
                'txtArrear.Text = ar.ToString + " Month/s"
                Sql = GetParam("oc_staclS")
                If rssql.State = 1 Then
                    rssql.Close()
                End If

                rssql.Open(Sql + "" & ar.Trim & ")", conn, ADODB.CursorTypeEnum.adOpenKeyset)
                'TextBoxObjCri.Text = rssql.Fields("NAME").Value.ToString
                'txtArrear.Text = Trim(DataGridViewCL3.SelectedRows(0).Cells(14).Value.ToString) + " day/s " + _
                'Trim(DataGridViewCL3.SelectedRows(0).Cells(22).Value.ToString) + " month/s"

                'Dim i As Integer
                'Dim j As Integer
                'Dim k As Integer
                ' If DataGridViewCL3.SelectedRows(0).Cells(14).Value.ToString = "0" Then
                'txtArrear.Text = "0 day"
                'Else
                '   i = Math.Floor(DataGridViewCL3.SelectedRows(0).Cells(14).Value / 365)
                '  j = Math.Floor((DataGridViewCL3.SelectedRows(0).Cells(14).Value - (i * 365)) / 30)
                ' k = DataGridViewCL3.SelectedRows(0).Cells(14).Value - ((i * 365) + (j * 30))
                'MessageBox.Show(DataGridViewCL3.SelectedRows(0).Cells(14).Value.ToString)
                'txtArrear.Text = i.ToString + " year/s " + j.ToString + " month/s " + k.ToString + " day/s"
                'End If
                'If rs.State = 1 Then
                'rs.Close()
                'End If
                'Sql = "select nvl(basis,'N/A') as basis,lpad(final_sta,2,'0')||'-'||(select short from cl_sta where sta = final_sta) as final_sta,nvl(base_of_provision,'0.00') as base_of_provision,lpad(ob_sta,2,'0')||'-'||(select short from cl_sta where sta = ob_sta) as ob_sta from cl_clmaster where entry_sta = '00' and" & _
                '    " ref_date = (select ref_date from cl_refdate where sta = '00')" & _
                '    "and acct ='" & acct_no & "'"
                'rs.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset)
                'txtArrear.Text = Trim(DataGridViewCL3.SelectedRows(0).Cells(22).Value.ToString) + " Month/s"
                'TextBoxObjCri.Text = rs.Fields("OB_STA").Value.ToString
                'txtSta.Text = rs.Fields("FINAL_STA").Value.ToString
                'txtBasis.Text = rs.Fields("BASIS").Value.ToString
                'txtBPAmt.Text = rs.Fields("BASE_OF_PROVISION").Value.ToString
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ButtonCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCalculate.Click
        Try
            ButtonSave.Enabled = True


            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim k As Integer = 0

            If rs1.State = 1 Then
                rs1.Close()
            End If
            rs1.Open("select sta from cl_sta where short='" & Cmbqj.Text.Trim & "'", conn, ADODB.CursorTypeEnum.adOpenKeyset)
            i = rs1.Fields("sta").Value
            ' If rs2.State = 1 Then
            'rs2.Close()
            'End If
            'rs2.Open("select sta from cl_sta where short='" & TextBoxObjCri.Text.Trim & "'", conn, ADODB.CursorTypeEnum.adOpenKeyset)
            'j = rs2.Fields("sta").Value

            'If i > j Then
            txtSta.Text = Cmbqj.Text
            txtBasis.Text = "QJ"
            k = i
            'Else
            'txtSta.Text = TextBoxObjCri.Text
            'txtBasis.Text = "OB"
            'k = j
            'End If
            If k >= 31 Then
                txtBPAmt.Text = FormatNumber((FormatNumber(LabelOutstanding.Text, 2) - _
                FormatNumber(txtIntSus.Text, 2) _
                - FormatNumber(txtEligSec.Text, 0)), 2, TriState.True, TriState.False, TriState.False).ToString
            ElseIf k = 8 Then
                txtBPAmt.Text = LabelOutstanding.Text
            ElseIf k = 30 Then
                txtBPAmt.Text = FormatNumber(LabelOutstanding.Text, 2) - _
                FormatNumber(txtIntSus.Text, 2)

            End If
            If txtBPAmt.Text < 0 Then
                txtBPAmt.Text = "0.00"
            End If
        Catch ex As Exception
            MsgBox("Data not Valid!!!")
        End Try
    End Sub

    Private Sub TextBoxAcno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxAcno.TextChanged

        Dim acct As String = "%" + TextBoxAcno.Text.PadRight(13, "_")
        Me.VW_CLSTableAdapter.SelectCommand.CommandText = GetParam("fillclS")
        Me.VW_CLSTableAdapter.Fill(DataSet7.VW_CLS, BranchCode, acct, "%" + TextBoxacname.Text + "%")
    End Sub

    Private Sub TextBoxacname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxacname.TextChanged

        Dim acct As String = "%" + TextBoxacname.Text + "%"
        Me.VW_CLSTableAdapter.SelectCommand.CommandText = GetParam("fillclS")
        Me.VW_CLSTableAdapter.Fill(DataSet7.VW_CLS, BranchCode, "%" + TextBoxAcno.Text.PadRight(13, "_"), acct)
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        Try
            If TextBoxday.Text = "" Or TextBoxmon.Text = "" Or TextBoxyear.Text = "" Then
                MessageBox.Show("Blank Sanction/Renewed/Rescheduled Amount.Value Needed..")
                TextBoxday.Text = ""
                TextBoxmon.Text = ""
                TextBoxyear.Text = ""
                TextBoxday.Focus()
                Return
            End If
            If txtIntSus.Text = "" Then
                txtIntSus.Text = "0.00"
            End If

            If txtEligSec.Text = "" Then
                txtEligSec.Text = "0"
            End If

            ' If (ComboBoxCat.Text = "Select") Then
            'MessageBox.Show("Category is not selected.")
            ' Return
            ' End If
            'If txtNationalID.Text = "" Then
            'MessageBox.Show("Enter National ID NO.")
            'Return
            'End If
            If Trim(TextBoxyear.Text) + Trim(TextBoxmon.Text) + Trim(TextBoxday.Text) > DataGridViewCL3.SelectedRows(0).Cells(5).Value.ToString Then
                MessageBox.Show("Date should be less than Expiry Date.")
                TextBoxyear.Text = ""
                TextBoxmon.Text = ""
                TextBoxday.Text = ""
                TextBoxday.Focus()
                Return
            End If

            If Trim(TextBoxyear.Text) + Trim(TextBoxmon.Text) + Trim(TextBoxday.Text) < "19890101" Then
                MessageBox.Show("Invalid Date.")
                TextBoxyear.Text = ""
                TextBoxmon.Text = ""
                TextBoxday.Text = ""
                TextBoxday.Focus()
                Return
            End If

            If Trim(TextBoxday.Text) > "31" Then
                MessageBox.Show("Invalid Day.")
                TextBoxyear.Text = ""
                TextBoxmon.Text = ""
                TextBoxday.Text = ""
                TextBoxday.Focus()
                Return
            End If

            If Trim(TextBoxmon.Text) > "12" Then
                MessageBox.Show("Invalid Month.")
                TextBoxyear.Text = ""
                TextBoxmon.Text = ""
                TextBoxday.Text = ""
                TextBoxday.Focus()
                Return
            End If
            ' Dim cat As String = ComboBoxCat.Text
            ' If ComboBoxCat.Text = "Select" Then
            'cat = "0"
            'End If
            ButtonSave.Enabled = False
            ButtonDel.Enabled = False

            Sql = "update cl_clmaster set " & _
           "category ='STF'" & _
           ",reschedule_no =lpad('" & txtResceduleNo.Text & _
           "',3,'0'),sanction_date = '" & Trim(TextBoxyear.Text) + Trim(TextBoxmon.Text) + Trim(TextBoxday.Text) & _
           "',sanc_amt =" & txtReschAmt.Text & _
           ",interest_suspense =" & txtIntSus.Text & _
           ",cnsl_val =" & txtEligSec.Text & _
            ",qj_sta = (select sta from cl_sta where short = '" & Cmbqj.Text.ToString & _
           "'),ob_sta= (select sta from cl_sta where short ='" & TextBoxObjCri.Text.ToString & _
           "'),final_sta=(select sta from cl_sta where short = '" & txtSta.Text.ToString & _
           "'),basis ='" & txtBasis.Text & _
           "',period_arear_mon='" & Trim(ar) & "', period_arear_day = '00'" & _
           ",base_of_provision= " & txtBPAmt.Text & _
           ",remarks = '" & TextBoxRemark.Text & _
           "',lastupdatedby = '" & currentUser & _
           "',lastupdateddate = sysdate" & _
            ",nationalid = '" & txtNationalID.Text & _
           "',mark='M' where acct = '" & acct_no & "'"
            If MsgBox("Do you want to save the change?", MsgBoxStyle.OkCancel, AcceptButton) = MsgBoxResult.Ok Then
                conn.Execute(Sql)
                If txtNationalID.Text <> "" Then
                    If rssql.State = 1 Then
                        rssql.Close()
                    End If

                    rssql.Open("Select count(*) as cnt from cl_natid where acct = '" & acct_no & "'", conn, ADODB.CursorTypeEnum.adOpenKeyset)
                    If rssql.Fields("CNT").Value = 0 Then
                        conn.Execute("insert into cl_natid (acct,nationalid) values ('" & acct_no & "', '" & txtNationalID.Text.Trim & "')")
                    ElseIf rssql.Fields("CNT").Value > 0 Then
                        conn.Execute("update cl_natid set nationalid = '" & txtNationalID.Text.Trim & "' where acct = '" & acct_no & "'")
                    End If
                End If
                GroupBoxDetails.Enabled = False
                CLS_Load(Nothing, Nothing)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    Private Sub txtReschAmt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReschAmt.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        Dim decimalSeparator As String = "."
        If [Char].IsDigit(e.KeyChar) Then
            txtReschAmt.Text = txtReschAmt.Text
            ' Digits are OK
        ElseIf keyInput.Equals(decimalSeparator) Then
            txtReschAmt.Text = txtReschAmt.Text
            ' Decimal separator is OK
        ElseIf e.KeyChar = vbBack Then
            txtReschAmt.Text = txtReschAmt.Text
        ElseIf e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtIntSus.Focus()
            txtReschAmt.Text = FormatNumber(txtReschAmt.Text, 2, TriState.True, TriState.False, TriState.False)
        Else
            MessageBox.Show("Invalid amount. Only digits and decimal separator is allowed.")
            txtReschAmt.Text = txtReschAmt.Text
            ' Swallow this invalid key and beep
            e.Handled = True
        End If

    End Sub

    Private Sub txtIntSus_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntSus.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        Dim decimalSeparator As String = "."
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtIntSus.Text = FormatNumber(txtIntSus.Text, 2, TriState.True, TriState.False, TriState.False)
            txtNationalID.Focus()
        ElseIf keyInput.Equals(decimalSeparator) Then
            txtIntSus.Text = txtIntSus.Text
            ' Decimal separator is OK
        ElseIf e.KeyChar = vbBack Then
            txtIntSus.Text = txtIntSus.Text
        ElseIf [Char].IsDigit(e.KeyChar) Then
            txtIntSus.Text = txtIntSus.Text
            ' Digits are OK
            ' Backspace key is OK
            '    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            '    {
            '     // Let the edit control handle control and alt key combinations
            '    }
        Else
            MessageBox.Show("Invalid amount. Only digits and decimal separator is allowed.")
            txtIntSus.Text = txtIntSus.Text
            ' Swallow this invalid key and beep
            e.Handled = True
        End If
    End Sub

    Private Sub txtEligSec_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEligSec.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        Dim decimalSeparator As String = "."
        If [Char].IsDigit(e.KeyChar) Then
            txtEligSec.Text = txtEligSec.Text
            ' Digits are OK
            'ElseIf keyInput.Equals(decimalSeparator) Then
            ' txtEligSec.Text = txtEligSec.Text
            ' Decimal separator is OK
        ElseIf e.KeyChar = vbBack Then
            txtEligSec.Text = txtEligSec.Text
        ElseIf e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            'txtEligSec.Text = FormatNumber(txtEligSec.Text, 2, TriState.True, TriState.False, TriState.False)
            ButtonCalculate.Focus()
        Else
            MessageBox.Show("Invalid amount. Only digit is allowed.")
            txtEligSec.Text = txtEligSec.Text
            ' Swallow this invalid key and beep
            e.Handled = True
        End If
    End Sub




    Private Sub txtResceduleNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtResceduleNo.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtResceduleNo.Text = txtResceduleNo.Text.PadLeft(3, "0")
            TextBoxday.Focus()
        End If
    End Sub

    Private Sub TextBoxmon_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxmon.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            TextBoxmon.Text = TextBoxmon.Text.PadLeft(2, "0")
            TextBoxyear.Focus()
        End If
    End Sub

    Private Sub TextBoxyear_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxyear.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            TextBoxyear.Text = TextBoxyear.Text.PadLeft(4, "0")
            txtReschAmt.Focus()
        End If
    End Sub

    Private Sub TextBoxday_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxday.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            TextBoxday.Text = TextBoxday.Text.PadLeft(2, "0")
            TextBoxmon.Focus()
        End If
    End Sub

    'Private Sub DataGridViewCL3_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewCL3.CellValueChanged
    'Dim colIndex As Integer = e.ColumnIndex
    'Dim ROWIndex As Integer = e.RowIndex
    'Dim rrow As DataGridViewRow = DataGridViewCL3.Rows(ROWIndex)

    'MessageBox.Show(rrow.Cells(colIndex).Value.ToString)
    'If ROWIndex >= 0 AndAlso colIndex >= 0 Then

    '     If rrow.Cells(colIndex).Value.ToString = "M" Then
    '          rrow.DefaultCellStyle.BackColor = Color.LightPink
    '       End If
    '    End If
    ' End Sub

    Private Sub DataGridViewCL3_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewCL3.CellFormatting

    End Sub


    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label19.Click

    End Sub

    Private Sub Buttonadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buttonadd.Click
        cl_type = "S"
        Acctlist.Show()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        conn.Execute("update cl_clmaster set cl = '',mark = '' where acct = '" & acct_no & "' and ref_date = (select ref_date from cl_refdate where sta = '00') and entry_sta = '00'")
        'conn.Execute("update cl_cl4 set entry_sta = '09' where entry_sta = '00' and ref_date = (select ref_date from cl_refdate where sta = '00') and acct = '" & acct_no & "'")
        MessageBox.Show("Data is deleted successfully")
        GroupBoxDetails.Enabled = False
        CLS_Load(Nothing, Nothing)
    End Sub

    Private Sub ButtonUnmark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        conn.Execute("update cl_clmaster set mark = 'U' where acct = '" & acct_no & "' and ref_date = (select ref_date from cl_refdate where sta = '00') and entry_sta = '00'")
        'conn.Execute("update cl_cl4 set mark = 'U' where entry_sta = '00' and ref_date = (select ref_date from cl_refdate where sta = '00') and acct = '" & acct_no & "'")
        MessageBox.Show("Data is unmarked successfully")
        GroupBoxDetails.Enabled = False
        CLS_Load(Nothing, Nothing)
    End Sub
    Private Sub TextBoxday_LostFocus(ByVal sender As Object, _
   ByVal e As System.EventArgs) Handles TextBoxday.LostFocus
        TextBoxday.Text = TextBoxday.Text.PadLeft(2, "0")
    End Sub
    Private Sub TextBoxmon_LostFocus(ByVal sender As Object, _
 ByVal e As System.EventArgs) Handles TextBoxmon.LostFocus
        TextBoxmon.Text = TextBoxmon.Text.PadLeft(2, "0")
    End Sub
    Private Sub TextBoxyear_LostFocus(ByVal sender As Object, _
 ByVal e As System.EventArgs) Handles TextBoxyear.LostFocus
        TextBoxyear.Text = TextBoxyear.Text.PadLeft(4, "0")
    End Sub

    'Private Sub ComboBoxCat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxCat.SelectedIndexChanged
    ' TextBoxRemark.Text = ComboBoxCat.Text
    'End Sub
    Private Sub txtNationalID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNationalID.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtEligSec.Focus()
        End If
    End Sub

   
    
End Class
