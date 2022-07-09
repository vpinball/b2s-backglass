Imports System.Text
Imports System.Windows.Forms

Public Class formAbout

    Private Sub formAbout_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim sb As StringBuilder = New StringBuilder()
        sb.AppendLine("Thanks a lot to:")
        sb.AppendLine()
        sb.AppendLine("'Dream7' for a first illumination code and wonderful LEDs.")
        sb.AppendLine("'Rosve' for all his great B2S' ideas.")
        sb.AppendLine("'Flying Dutchman' for images, infos and a lot UVP knowhow.")
        sb.AppendLine("'Grizz' for wonderful backglass and grill images.")
        sb.AppendLine("'Andy1710', 'boogies2', 'darquayle', 'Dazz', 'Dozer316', 'Itchigo', 'luvthatapex', 'Slydog43', 'thewool', 'tipoto', 'tttttwii' and 'Westworld' for supporting me and a lot testing.")
        sb.AppendLine()
        sb.AppendLine("'chriz99' for some backglass images of his highres backglass compilation.")
        txtCredits.Text = sb.ToString()

        Dim cr As StringBuilder = New StringBuilder()
        cr.AppendLine("B2S Backglass Designer " & Application.ProductVersion)
        'cr.AppendLine("(" & My.Application.Info.WorkingSet & ")")  My.Application.Info.Version.ToString &
        cr.AppendLine(My.Application.Info.Copyright.ToString & " by Herweh && B2S Team, All rights reserved.")
        lblCopyright.Text = cr.ToString()

    End Sub

    Private Sub btnDonation_Click(sender As System.Object, e As System.EventArgs) Handles btnDonation.Click

        MessageBox.Show("You can show your appreciation for 'B2S Backglass Designer' and 'B2S Backglass Server' and support future development by donating. " & vbCrLf &
                        "I strongly feel that 'B2S Backglass Designer' and 'B2S Backglass Server' should remain free of charge as my gift to the online community. " & vbCrLf &
                        "Please note that donations to 'B2S Backglass Designer' and 'B2S Backglass Server' are not tax-deductible for income tax purposes." & vbCrLf & vbCrLf &
                        "Send whatever amount you want to send to my PayPal address 'stefan.wuehr@freenet.de'." & vbCrLf & vbCrLf &
                        "Thank you very much.", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

End Class