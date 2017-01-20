Imports System.Globalization

Public Class CL5
    Public rs As New ADODB.Recordset
    Dim acct_no As String
    Public frq As String
    Public dt As String
    Public dtt As String
    Public ar As String
    Public sql1 As String
    Public rs1 As New ADODB.Recordset
    Public rs2 As New ADODB.Recordset

    Public Sub CL5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GroupBoxDetails.Enabled = False
        ButtonSave.Enabled = False

        ButtonDel.Enabled = False
        ButtonCalculate.Enabled = False
        Me.Text = "CL5-" + info
        'TODO: This line of code loads data into the 'DataSetCL3.VW_CL3' table. You can move, or remove it, as needed.

        Me.VW_CL5TableAdapter.SelectCommand.CommandText = GetParam("fillcl5")
        Me.VW_CL5TableAdapter.Fill(DataSet5.VW_CL5, BranchCode, "%_____________", "%")
        GroupBoxDetails.Text = "Details - " + info
        If flag = True Then
            Sql = GetParam("cat")
            If rssql.State = 1 Then
                rssql.Close()
            End If
            rssql.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset)
            Do Until rssql.EOF
                ComboBoxCat.Items.Add(rssql.Fields("SHRT").Value.ToString)
                rssql.MoveNext()
            Loop

            'Sql = GetParam("qj_sta")
            'If rssql.State = 1 Then
            'rssql.Close()
            ' End If
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
                Sql = "select remarks,nvl(basis,'N/A') as basis,(select short from cl_sta where sta = lpad(trim(final_sta),2,'0')) as final_sta,nvl(base_of_provision,'0.00') as base_of_provision,(select short from cl_sta where sta = lpad(trim(ob_sta),2,'0')) as ob_sta from cl_clmaster where entry_sta = '00' and" & _
                    " ref_date = (select ref_date from cl_refdate where sta = '00')" & _
                    "and acct ='" & acct_no & "'"
                rs.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset)
                txtArrear.Text = Trim(DataGridViewCL3.SelectedRows(0).Cells(22).Value.ToString) + " Month/s"
                TextBoxObjCri.Text = rs.Fields("OB_STA").Value.ToString
                txtSta.Text = rs.Fields("FINAL_STA").Value.ToString
                txtBasis.Text = rs.Fields("BASIS").Value.ToString
                txtBPAmt.Text = rs.Fields("BASE_OF_PROVISION").Value.ToString
                TextBoxRemark.Text = rs.Fields("REMARKS").Value.ToString
                'MessageBox.Show(acct_no)
                LabelAccount.Visible = True
                LabelName.Visible = True
                LabelNatureLoan.Visible = True
                LabelOutstanding.Visible = True
                Label11.Visible = True

                txtPeriodLastDue.Text = "0 Month/s"
                txtTimeEquAmtPaid.Text = "0 Month/s"

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


                'MessageBox.Show(DataGridViewCL3.SelectedRows(0).Cells(15).Value.ToString)
                If Trim(DataGridViewCL3.SelectedRows(0).Cells(15).Value.ToString) = "0" Then
                    ComboBoxCat.Text = "Select"
                Else
                    ComboBoxCat.Text = DataGridViewCL3.SelectedRows(0).Cells(15).Value.ToString
                End If

                Cmbqj.Text = DataGridViewCL3.SelectedRows(0).Cells(6).Value.ToString
                txtfreqsize.Text = DataGridViewCL3.SelectedRows(0).Cells(23).Value.ToString

                If Trim(DataGridViewCL3.SelectedRows(0).Cells(24).Value.ToString) = "0" Then
                    TextBoxInstfreq.Text = "Select"

                ElseIf Trim(DataGridViewCL3.SelectedRows(0).Cells(24).Value.ToString) = "1" Then
                    TextBoxInstfreq.Text = "Monthly"
                ElseIf Trim(DataGridViewCL3.SelectedRows(0).Cells(24).Value.ToString) = "3" Then
                    TextBoxInstfreq.Text = "Quaterly"
                ElseIf Trim(DataGridViewCL3.SelectedRows(0).Cells(24).Value.ToString) = "6" Then
                    TextBoxInstfreq.Text = "Half-Yearly"
                ElseIf Trim(DataGridViewCL3.SelectedRows(0).Cells(24).Value.ToString) = "12" Then
                    TextBoxInstfreq.Text = "Yearly"
                End If
                frq = DataGridViewCL3.SelectedRows(0).Cells(24).Value.ToString
                'MessageBox.Show(DataGridViewCL3.SelectedRows(0).Cells(25).Value.ToString)
                date_format(Trim(DataGridViewCL3.SelectedRows(0).Cells(25).Value.ToString))
                txtdayRD.Text = day
                txtmonRD.Text = month
                txtyearRD.Text = year
                txtPeriodLastDue.Text = DataGridViewCL3.SelectedRows(0).Cells(26).Value.ToString.Trim
                txtLastAmtPaid.Text = DataGridViewCL3.SelectedRows(0).Cells(27).Value.ToString
                txtTimeEquAmtPaid.Text = DataGridViewCL3.SelectedRows(0).Cells(28).Value.ToString.Trim
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ButtonCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCalculate.Click
        Try

            If date_format4save(TextBoxday.Text, TextBoxmon.Text, TextBoxyear.Text) = False Then
                MessageBox.Show("Wrong Sanction/Renewed/Reschedule Date")
                TextBoxday.Text = ""
                TextBoxmon.Text = ""
                TextBoxyear.Text = ""
                Return
            End If


            If txtReschAmt.Text = "" Or txtfreqsize.Text = "" Or txtLastAmtPaid.Text = "" Or _
            txtIntSus.Text = "" Or txtEligSec.Text = "" Then
                MessageBox.Show("Blank field found.Value Needed..")
                Return
            End If

            ' If txtfreqsize.Text = "0.00" Then
            'MessageBox.Show("Installment size cannot be 0.00")
            'txtfreqsize.Text = ""
            'Return
            'End If

      


            If TextBoxInstfreq.Text = "Select" Or TextBoxInstfreq.Text = "N/A" Then
                frq = "0"



            ElseIf TextBoxInstfreq.Text = "Monthly" Then
                frq = "1"
            ElseIf TextBoxInstfreq.Text = "Quarterly" Then
                frq = "3"
            ElseIf TextBoxInstfreq.Text = "Half-Yearly" Then
                frq = "6"
            ElseIf TextBoxInstfreq.Text = "Yearly" Then
                frq = "12"
            End If
            'MessageBox.Show(frq)
            If date_format4save(txtdayRD.Text, txtmonRD.Text, txtyearRD.Text) = False Then

                txtdayRD.Text = ""
                txtmonRD.Text = ""
                txtyearRD.Text = ""

            End If
            dt = Trim(txtyearRD.Text.PadLeft(4, "0")) + Trim(txtmonRD.Text.PadLeft(2, "0")) + Trim(txtdayRD.Text.PadLeft(2, "0"))
            'Dim dtt As String
            'Dim ar As String

            If dt = "00000000" Then
                dt = RefDate
            End If
            dt = date_monthdiff(dt, RefDate)

           
            'MessageBox.Show(dt + " months")
            txtPeriodLastDue.Text = dt
            If txtfreqsize.Text.Trim = "0.00" Or txtfreqsize.Text.Trim = "0" Then
                dtt = 0
            Else
                dtt = Math.Round((txtLastAmtPaid.Text.Trim * frq) / txtfreqsize.Text.Trim, 2)
            End If

            txtTimeEquAmtPaid.Text = dtt.ToString
            ar = Math.Round((dt - dtt), 2)
            If ar < 0 Then
                ar = 0
            End If
            txtArrear.Text = ar.ToString + " Month/s"
            Sql = GetParam("oc_stacl5")
            If rssql.State = 1 Then
                rssql.Close()


                'If rssql.State = 1 Then
                'rssql.Close()
            End If
            Sql = Sql + "" & ar & ")"

            rssql.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset)
            TextBoxObjCri.Text = rssql.Fields("NAME").Value.ToString
            '      txtArrear.Text = Trim(DataGridViewCL3.SelectedRows(0).Cells(14).Value.ToString) + " day/s " + _
            '     Trim(DataGridViewCL3.SelectedRows(0).Cells(22).Value.ToString) + " month/s"

            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim k As Integer = 0

            If rs1.State = 1 Then
                rs1.Close()
            End If
            rs1.Open("select sta from cl_sta where short='" & Cmbqj.Text.Trim & "'", conn, ADODB.CursorTypeEnum.adOpenKeyset)
            i = rs1.Fields("sta").Value
            If rs2.State = 1 Then
                rs2.Close()
            End If
            rs2.Open("select sta from cl_sta where short='" & TextBoxObjCri.Text.Trim & "'", conn, ADODB.CursorTypeEnum.adOpenKeyset)
            j = rs2.Fields("sta").Value
            If i > j Then
                txtSta.Text = Cmbqj.Text
                txtBasis.Text = "QJ"
                k = i
            Else
                txtSta.Text = TextBoxObjCri.Text
                txtBasis.Text = "OB"
                k = j
            End If
            If k >= 31 Then
                txtBPAmt.Text = FormatNumber((DataGridViewCL3.SelectedRows(0).Cells(4).Value - _
                txtIntSus.Text _
                - txtEligSec.Text), 0, TriState.True, TriState.False, TriState.False).ToString
            ElseIf k = 8 Then
                txtBPAmt.Text = FormatNumber(DataGridViewCL3.SelectedRows(0).Cells(4).Value, 2, TriState.True, TriState.False, TriState.False).ToString
            ElseIf k = 30 Then
                txtBPAmt.Text = FormatNumber(DataGridViewCL3.SelectedRows(0).Cells(4).Value - _
                txtIntSus.Text, 2, TriState.True, TriState.False, TriState.False).ToString

            End If
            If txtBPAmt.Text < 0 Then
                txtBPAmt.Text = "0.00"
            End If
            ButtonSave.Enabled = True
          

        Catch ex As Exception
            MsgBox("Data not Valid!!! Amount should be 0.00. Please Check.")
        End Try
    End Sub

    Private Sub TextBoxAcno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxAcno.TextChanged

        Dim acct As String = "%" + TextBoxAcno.Text.PadRight(13, "_")
        Me.VW_CL5TableAdapter.SelectCommand.CommandText = GetParam("fillcl5")
        Me.VW_CL5TableAdapter.Fill(DataSet5.VW_CL5, BranchCode, acct, "%" + TextBoxacname.Text + "%")
    End Sub

    Private Sub TextBoxacname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxacname.TextChanged

        Dim acct As String = "%" + TextBoxacname.Text + "%"
        Me.VW_CL5TableAdapter.SelectCommand.CommandText = GetParam("fillcl5")
        Me.VW_CL5TableAdapter.Fill(DataSet5.VW_CL5, BranchCode, "%" + TextBoxAcno.Text.PadRight(13, "_"), acct)
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        ButtonCalculate_Click(Nothing, Nothing)
        Try
            If TextBoxday.Text = "" Or TextBoxmon.Text = "" Or TextBoxyear.Text = "" Then
                MessageBox.Show("Blank Sanction/Renewed/Rescheduled Date.Value Needed..")
                TextBoxday.Text = ""
                TextBoxmon.Text = ""
                TextBoxyear.Text = ""
                TextBoxday.Focus()
                Return
            End If

            If txtdayRD.Text = "" Or txtmonRD.Text = "" Or txtyearRD.Text = "" Then
                MessageBox.Show("Blank 1st Repayment Due date.Value Needed..")
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
            If (ComboBoxCat.Text = "Select") Then
                MessageBox.Show("Category is not selected.")
                Return
            End If
            'If txtNationalID.Text = "" Then
            'MessageBox.Show("Enter National ID NO.")
            'Return
            'End If
            If Trim(TextBoxyear.Text) + Trim(TextBoxmon.Text) + Trim(TextBoxday.Text) > DataGridViewCL3.SelectedRows(0).Cells(5).Value.ToString Then
                MessageBox.Show("Sanction Date should be less than Expiry Date.")
                TextBoxyear.Text = ""
                TextBoxmon.Text = ""
                TextBoxday.Text = ""
                TextBoxday.Focus()
                Return
            End If

            If Trim(txtyearRD.Text) + Trim(txtmonRD.Text) + Trim(txtdayRD.Text) > Trim(TextBoxyear.Text) + Trim(TextBoxmon.Text) + Trim(TextBoxday.Text) Then
                MessageBox.Show("Date of 1st Repayment Due should be less than Expiry Date.")
                txtyearRD.Text = ""
                txtmonRD.Text = ""
                txtdayRD.Text = ""
                txtdayRD.Focus()
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

            If Trim(txtyearRD.Text) + Trim(txtmonRD.Text) + Trim(txtdayRD.Text) < "19890101" Then
                MessageBox.Show("Invalid Date.")
                txtyearRD.Text = ""
                txtmonRD.Text = ""
                txtdayRD.Text = ""
                txtdayRD.Focus()
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

            If Trim(txtdayRD.Text) > "31" Then
                MessageBox.Show("Invalid Day.")
                txtyearRD.Text = ""
                txtmonRD.Text = ""
                txtdayRD.Text = ""
                txtdayRD.Focus()
                Return
            End If

            If Trim(TextBoxmon.Text) > "12" Or Trim(txtmonRD.Text) > "12" Then
                MessageBox.Show("Invalid Month.")
                TextBoxyear.Text = ""
                TextBoxmon.Text = ""
                TextBoxday.Text = ""
                TextBoxday.Focus()
                Return
            End If

            If Trim(txtmonRD.Text) > "12" Then
                MessageBox.Show("Invalid Month.")
                txtyearRD.Text = ""
                txtmonRD.Text = ""
                txtdayRD.Text = ""
                txtdayRD.Focus()
                Return
            End If
            Dim cat As String = ComboBoxCat.Text
            'If ComboBoxCat.Text = "Select" Then
            'cat = "0"
            'End If
            ButtonSave.Enabled = False
            ButtonDel.Enabled = False

            Sql = "update cl_clmaster set " & _
           "category ='" & cat & _
           "',reschedule_no =lpad('" & txtResceduleNo.Text & _
           "',3,'0'),sanction_date = '" & Trim(TextBoxyear.Text) + Trim(TextBoxmon.Text) + Trim(TextBoxday.Text) & _
           "',sanc_amt =" & txtReschAmt.Text & _
           ",interest_suspense =" & txtIntSus.Text & _
           ",cnsl_val =" & txtEligSec.Text & _
           ",qj_sta = (select sta from cl_sta where short = '" & Cmbqj.Text.ToString & _
           "'),ob_sta= (select sta from cl_sta where short ='" & TextBoxObjCri.Text.ToString & _
           "'),final_sta=(select sta from cl_sta where short ='" & txtSta.Text.ToString & _
           "'),basis ='" & txtBasis.Text & _
           "',base_of_provision= '" & txtBPAmt.Text & _
           "',period_arear_mon='" & Trim(ar) & "', period_arear_day = '0'" & _
           ",remarks = '" & TextBoxRemark.Text & _
           "',lastupdatedby = '" & currentUser & _
           "',lastupdateddate = sysdate" & _
            ",nationalid = '" & txtNationalID.Text & _
           "',mark='M' where acct = '" & acct_no & "' and ref_date = (select ref_date from cl_refdate where sta = '00') and entry_sta = '00'"
            'If txtNationalID.Text <> "" Then
            If rs.State = 1 Then
                rs.Close()
            End If
            rs.Open("select count(*) as cnt from cl_cl4 where acct = '" & acct_no & "' and ref_date = (select ref_date from cl_refdate where sta = '00') and entry_sta = '00'", conn, ADODB.CursorTypeEnum.adOpenKeyset)
            'MessageBox.Show(rssql.RecordCount.ToString)
            If rs.Fields("CNT").Value = 0 Then
                sql1 = "insert into cl_cl4(install_size,install_freq,first_repayment_due_date,first_repayment_due_period," & _
                       "amount_paid,amt_paid_time,acct,ref_date,entry_sta) values" & _
                       "('" & txtfreqsize.Text & "','" & frq & "',trim('" & txtyearRD.Text.PadLeft(4, "0") & "') || trim('" & txtmonRD.Text.PadLeft(2, "0") & "') || trim('" & txtdayRD.Text.PadLeft(2, "0") & "'), trim('" & dt & "')" & _
                       ",'" & txtLastAmtPaid.Text & "',trim('" & dtt & "'),'" & acct_no & "',(select ref_date from cl_refdate where sta = '00'),'00')"
                'MessageBox.Show("update")
                'sql1 = "update cl_cl4 set install_size= '100' where acct = '0000602010000030' and ref_date = (select ref_date from cl_refdate where sta = '00') and entry_sta = '00'"
                '"update cl_cl4 set " & _
                '"install_size= '" & Trim(txtfreqsize.Text) & "'" & _
                '" where acct = '" & acct_no & "' " & _
                '" and ref_date = (select ref_date from cl_refdate where sta = '00')" & _
                '" and entry_sta = '00'"
                'MessageBox.Show(Sql)
                '", install_freq = '" & Trim(frq) & "'," & _
                '"first_repayment_due_date = trim('" & txtyearRD.Text.PadLeft(4, "0") & "'||'" & txtmonRD.Text.PadLeft(2, "0") & "'||'" & txtdayRD.Text.PadLeft(2, "0") & "') ," & _
                '"first_repayment_due_period = Trim('" & dt & "')," & _
                '"amount_paid = '" & txtLastAmtPaid.Text & "'," & _
                '"amt_paid_time = Trim('" & dtt & "')" & _
            Else
                'MessageBox.Show("update")
                'sql1 = "update cl_cl4 set install_size= '100' where acct = '0000602010000030' and ref_date = (select ref_date from cl_refdate where sta = '00') and entry_sta = '00'"
                sql1 = "update cl_cl4 set " & _
                "install_size= '" & Trim(txtfreqsize.Text) & "'" & _
                ", install_freq = '" & Trim(frq) & "'," & _
                "first_repayment_due_date = trim('" & txtyearRD.Text.PadLeft(4, "0") & "'||'" & txtmonRD.Text.PadLeft(2, "0") & "'||'" & txtdayRD.Text.PadLeft(2, "0") & "') ," & _
                "first_repayment_due_period = Trim('" & dt & "')," & _
                "amount_paid = '" & txtLastAmtPaid.Text & "'," & _
                "amt_paid_time = Trim('" & dtt & "')" & _
                " where acct = '" & acct_no & "' " & _
                " and ref_date = (select ref_date from cl_refdate where sta = '00')" & _
                " and entry_sta = '00'"
                'MessageBox.Show(Sql)
                'sql1 = "update cl_cl4 set install_size= '100' where acct = '0000602010000030' and ref_date = (select ref_date from cl_refdate where sta = '00') and entry_sta = '00'"
                'sql1 = "insert into cl_cl4(install_size,install_freq,first_repayment_due_date,first_repayment_due_period," & _
                '       "amount_paid,amt_paid_time,acct,ref_date,entry_sta) values" & _
                '      "('" & txtfreqsize.Text & "','" & frq & "',trim('" & txtyearRD.Text.PadLeft(4, "0") & "') || trim('" & txtmonRD.Text.PadLeft(2, "0") & "') || trim('" & txtdayRD.Text.PadLeft(2, "0") & "'), trim('" & dt & "')" & _
                '        ",'" & txtLastAmtPaid.Text & "',trim('" & dtt & "'),'" & acct_no & "',(select ref_date from cl_refdate where sta = '00'),'00')"
                'conn.Execute(Sql)
            End If
            ' End If
            If MsgBox("Do you want to save the change?", MsgBoxStyle.OkCancel, AcceptButton) = MsgBoxResult.Ok Then
                'MessageBox.Show("1")
                conn.Execute(Sql)
                'MessageBox.Show("2")
                conn.Execute(sql1)
                'MessageBox.Show("3")

                If rssql.State = 1 Then
                    rssql.Close()
                End If

                rssql.Open("Select count(*) as cnt from cl_natid where acct = '" & acct_no & "'", conn, ADODB.CursorTypeEnum.adOpenKeyset)
                If rssql.Fields("CNT").Value = 0 Then
                    conn.Execute("insert into cl_natid (acct,nationalid) values ('" & acct_no & "', '" & txtNationalID.Text.Trim & "')")
                ElseIf rssql.Fields("CNT").Value > 0 Then
                    conn.Execute("update cl_natid set nationalid = '" & txtNationalID.Text.Trim & "' where acct = '" & acct_no & "'")
                End If
                GroupBoxDetails.Enabled = False
                CL5_Load(Nothing, Nothing)
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
            txtfreqsize.Focus()
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
            txtEligSec.Focus()
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
            Cmbqj.Focus()
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
    Private Sub txtmonRD_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmonRD.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtmonRD.Text = txtmonRD.Text.PadLeft(2, "0")
            txtyearRD.Focus()
        End If
    End Sub

    Private Sub txtyearRD_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtyearRD.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtyearRD.Text = txtyearRD.Text.PadLeft(4, "0")
            txtLastAmtPaid.Focus()
        End If
    End Sub

    Private Sub txtdayRD_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdayRD.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtdayRD.Text = txtdayRD.Text.PadLeft(2, "0")
            txtmonRD.Focus()
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



    Private Sub Buttonadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buttonadd.Click
        cl_type = "5"
        Acctlist.Show()
        GroupBoxDetails.Enabled = False
    End Sub


    Private Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        conn.Execute("update cl_clmaster set cl = '',mark = '' where acct = '" & acct_no & "' and ref_date = (select ref_date from cl_refdate where sta = '00') and entry_sta = '00'")
        conn.Execute("delete from cl_cl4 where entry_sta = '00' and ref_date = (select ref_date from cl_refdate where sta = '00') and acct = '" & acct_no & "'")
        MessageBox.Show("Data is deleted successfully")
        GroupBoxDetails.Enabled = False
        ButtonSave.Enabled = False
        ButtonDel.Enabled = False

        CL5_Load(Nothing, Nothing)
    End Sub

    Private Sub ButtonUnmark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        conn.Execute("update cl_clmaster set mark = 'U' where acct = '" & acct_no & "' and ref_date = (select ref_date from cl_refdate where sta = '00') and entry_sta = '00'")
        'conn.Execute("update cl_cl4 set mark = 'U' where entry_sta = '00' and ref_date = (select ref_date from cl_refdate where sta = '00') and acct = '" & acct_no & "'")
        MessageBox.Show("Data is unmarked successfully")
        GroupBoxDetails.Enabled = False
        ButtonSave.Enabled = False
        ButtonDel.Enabled = False

        CL5_Load(Nothing, Nothing)
    End Sub

    Private Sub txtfreqsize_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfreqsize.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        Dim decimalSeparator As String = "."
        If [Char].IsDigit(e.KeyChar) Then
            txtfreqsize.Text = txtfreqsize.Text
            ' Digits are OK
        ElseIf keyInput.Equals(decimalSeparator) Then
            txtfreqsize.Text = txtfreqsize.Text
            ' Decimal separator is OK
        ElseIf e.KeyChar = vbBack Then
            txtfreqsize.Text = txtfreqsize.Text
        ElseIf e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtfreqsize.Text = FormatNumber(txtfreqsize.Text, 2, TriState.True, TriState.False, TriState.False)
            TextBoxInstfreq.Focus()
        Else
            MessageBox.Show("Invalid amount. Only digits and decimal separator is allowed.")
            txtfreqsize.Text = "0.00"
            ' Swallow this invalid key and beep
            e.Handled = True
        End If
    End Sub

    Private Sub txtLastAmtPaid_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLastAmtPaid.KeyPress
        Dim keyInput As String = e.KeyChar.ToString()
        Dim decimalSeparator As String = "."
        If [Char].IsDigit(e.KeyChar) Then
            txtLastAmtPaid.Text = txtLastAmtPaid.Text
            ' Digits are OK
        ElseIf keyInput.Equals(decimalSeparator) Then
            txtLastAmtPaid.Text = txtLastAmtPaid.Text
            ' Decimal separator is OK
        ElseIf e.KeyChar = vbBack Then
            txtLastAmtPaid.Text = txtLastAmtPaid.Text
        ElseIf e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtLastAmtPaid.Text = FormatNumber(txtLastAmtPaid.Text, 2, TriState.True, TriState.False, TriState.False)
            txtIntSus.Focus()
        Else
            MessageBox.Show("Invalid amount. Only digits and decimal separator is allowed.")
            txtLastAmtPaid.Text = "0.00"
            ' Swallow this invalid key and beep
            e.Handled = True
        End If
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
    Private Sub txtdayRD_LostFocus(ByVal sender As Object, _
   ByVal e As System.EventArgs) Handles txtdayRD.LostFocus
        txtdayRD.Text = txtdayRD.Text.PadLeft(2, "0")
    End Sub
    Private Sub txtmonRD_LostFocus(ByVal sender As Object, _
 ByVal e As System.EventArgs) Handles txtmonRD.LostFocus
        txtmonRD.Text = txtmonRD.Text.PadLeft(2, "0")
    End Sub
    Private Sub txtyearRD_LostFocus(ByVal sender As Object, _
 ByVal e As System.EventArgs) Handles txtyearRD.LostFocus
        txtyearRD.Text = txtyearRD.Text.PadLeft(4, "0")
    End Sub

   
    Private Sub ComboBoxCat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxCat.SelectedIndexChanged
        TextBoxRemark.Text = ComboBoxCat.Text
    End Sub
    Private Sub txtNationalID_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNationalID.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtdayRD.Focus()
        End If
    End Sub
End Class
