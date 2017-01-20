Public Class dataimport

    Private Sub dataimport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "IMPORT DATA"
        GroupBox1.Text = info
    End Sub

    Private Sub ButtonDataImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDataImport.Click
        Try


            ButtonDataImport.Enabled = False
            OpenConn()

            cmd.ActiveConnection = conn

            cmd.CommandText = "CL_IMPORT_KCBS"
            cmd.CommandType = ADODB.CommandTypeEnum.adCmdStoredProc
            cmd.Parameters.Append(cmd.CreateParameter("vUname", ADODB.DataTypeEnum.adVarChar, ADODB.ParameterDirectionEnum.adParamInput, 100, currentUser))
            cmd.Execute()
            cmd.Parameters.Delete("vUname")

            
            cmd.CommandText = "CL_IMPORT_CLMASTER"
            cmd.CommandType = ADODB.CommandTypeEnum.adCmdStoredProc
            cmd.Parameters.Append(cmd.CreateParameter("vUser", ADODB.DataTypeEnum.adVarChar, ADODB.ParameterDirectionEnum.adParamInput, 100, currentUser))
            cmd.Execute()
            cmd.Parameters.Delete("vUser")

            cmd.CommandText = "CL_IMPORT_periodArrear"
            cmd.CommandType = ADODB.CommandTypeEnum.adCmdStoredProc
            cmd.Execute()

            MessageBox.Show("Data is successfully imported.")
            ButtonDataImport.Enabled = True
            Me.Close()

        Catch ex As Exception
            cmd.Execute("delete from cl_kcbs where period = '" & RefDate.Trim & "'")
            cmd.Execute("delete from cl_clmaster where ref_date = '" & RefDate & "'")
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class