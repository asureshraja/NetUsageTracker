Public Class Form1
    Dim a(0 To 1000000) As Char
    Dim b(0 To 1000000) As Char
    Dim c As Boolean
    Dim k As Long
    Dim num As Double
    Dim temp As Double
    Dim fnum As Double
    Dim temper As Double
    Dim bw As Double
    Dim fbw As Double
    Dim unit As String
    Dim funit As String
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing

        Me.Visible = False
        e.Cancel = True
        taskbar.Visible = True
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        unit = "bytes"
        funit = "bytes"
      
        c = True
        taskbar.Visible = True
        Label1.Text = ""
        Timer1.Interval = 1000
        TextBox1.Text = GetSetting("bndwidthmon", "final", "lastsess", "")
        RichTextBox1.Text = ""
        Dim clsProcess As New System.Diagnostics.Process()
        clsProcess.StartInfo.UseShellExecute = False
        clsProcess.StartInfo.RedirectStandardOutput = True
        clsProcess.StartInfo.RedirectStandardError = True
        clsProcess.StartInfo.FileName = "netstat"
        clsProcess.StartInfo.Arguments = "-e"
        clsProcess.StartInfo.CreateNoWindow = True
        clsProcess.Start()
        While (clsProcess.HasExited = False)
            Dim sLine As String = clsProcess.StandardOutput.ReadToEnd
            Me.RichTextBox1.Text &= sLine & vbCrLf
            Exit While
            Application.DoEvents()
        End While

        num = Double.Parse(OnlyNumbers(RichTextBox1.Text))
        temp = num

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        fnum = 0
        SaveSetting("bndwidthmon", "final", "lastsess", fnum)
        End

    End Sub
    Function OnlyNumbers(ByVal Str As String) As String
        Dim i As Long
        a = Str.ToCharArray
        k = 0

        For i = 0 To Len(Str)

            If (Asc(a(i)) > 47 And Asc(a(i)) < 58) Then
                b(k) = a(i)
                k = k + 1
                If (Asc(a(i + 1)) < 47 Or Asc(a(i)) > 57) Then
                    Exit For
                End If
            End If
        Next
        OnlyNumbers = New String(b)

    End Function

    Private Sub taskbar_BalloonTipClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles taskbar.BalloonTipClosed
        c = True
    End Sub

    Private Sub taskbar_BalloonTipShown(ByVal sender As Object, ByVal e As System.EventArgs) Handles taskbar.BalloonTipShown

        c = False

    End Sub

    Private Sub nfi_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles taskbar.MouseClick

        Me.Show()
    End Sub

   

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        RichTextBox1.Text = ""
        Dim clsProcess As New System.Diagnostics.Process()
        clsProcess.StartInfo.UseShellExecute = False
        clsProcess.StartInfo.RedirectStandardOutput = True
        clsProcess.StartInfo.RedirectStandardError = True
        clsProcess.StartInfo.FileName = "netstat"
        clsProcess.StartInfo.Arguments = "-e"
        clsProcess.StartInfo.CreateNoWindow = True
        clsProcess.Start()
        While (clsProcess.HasExited = False)
            Dim sLine As String = clsProcess.StandardOutput.ReadToEnd
            Me.RichTextBox1.Text &= sLine & vbCrLf
            Exit While
            Application.DoEvents()
        End While

        num = Double.Parse(OnlyNumbers(RichTextBox1.Text))
        num = num - temp
        temper = num
        fnum = temper + Val(TextBox1.Text)
        SaveSetting("bndwidthmon", "final", "lastsess", fnum)
        bw = temper
        fbw = fnum
        If (temper > Math.Pow(2, 10) And temper < Math.Pow(2, 20)) Then
            bw = bw / 1024
            unit = "KB"
        ElseIf (temper > Math.Pow(2, 20) And temper < Math.Pow(2, 30)) Then
            bw = bw / 1024
            bw = bw / 1024
            unit = "MB"
        ElseIf (temper > Math.Pow(2, 30) And temper < Math.Pow(2, 40)) Then
            bw = bw / 1024
            bw = bw / 1024
            bw = bw / 1024
            unit = "GB"
        End If
        If (fnum > Math.Pow(2, 10) And fnum < Math.Pow(2, 20)) Then
            fbw = fbw / 1024
            funit = "KB"
        ElseIf (fnum > Math.Pow(2, 20) And fnum < Math.Pow(2, 30)) Then
            fbw = fbw / 1024
            fbw = fbw / 1024
            funit = "MB"
        ElseIf (fnum > Math.Pow(2, 30) And fnum < Math.Pow(2, 40)) Then
            fbw = fbw / 1024
            fbw = fbw / 1024
            fbw = fbw / 1024
            funit = "GB"
        End If
        Label1.Text = Math.Round(bw, 3) & " " & unit
        Label2.Text = Math.Round(fbw, 3) & " " & funit
    End Sub

  
   

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub



    Private Sub taskbar_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles taskbar.MouseMove
        If (c = True) Then
            taskbar.BalloonTipText = "Total used: " & Label2.Text & Chr(13) & "session BW: " & Label1.Text
            taskbar.ShowBalloonTip(3)
        End If
    End Sub

End Class
