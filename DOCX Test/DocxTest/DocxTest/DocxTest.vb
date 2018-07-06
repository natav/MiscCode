Imports System.IO
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing

Public Class DocxTest
    Public Sub New()
        CreateNewWordDocument()
    End Sub

    Public Shared Sub Main()
        CreateNewWordDocument()
    End Sub
    Private Shared Sub CreateNewWordDocument()
        Dim filename As String = String.Format("c:/temp/Docx test/{0}.docx", Guid.NewGuid.ToString)

        Using fs As FileStream = File.Open(filename, FileMode.CreateNew)

            'Dim byteContent As Byte() = System.Text.UTF8Encoding.UTF8.GetBytes("this is a test content")
            'fs.Write(byteContent, 0, byteContent.Length)
            fs.Close()
        End Using

        SaveEmptyFile(filename)

        LoadText(filename, "testing loading text")

    End Sub

    Private Shared Sub SaveEmptyFile(ByVal filename As String)

        'Dim filetransfer As New FileTransferObject
        Using fsTemplate As FileStream = New FileStream("C:/Users/natalyav/Documents/GitHub/legistar/legistar-app/LMS/bin/Debug/LegistarTemplate.docx", FileMode.Open, FileAccess.Read)

            Dim byteWordDocument As Byte()
            Dim filelength As Integer = System.Convert.ToInt32(fsTemplate.Length)
            ReDim byteWordDocument(filelength)
            fsTemplate.Read(byteWordDocument, 0, filelength)
            fsTemplate.Close()

            File.Delete(filename)

            ''works
            'Using fs1 As FileStream = File.OpenWrite(filename)
            '    Dim br1 = New BinaryWriter(fs1)
            '    br1.Write(byteWordDocument, 0, filelength)
            '    br1.Flush()
            '    fs1.Flush()
            'End Using
            ''works

            ''works 1
            Using fs As FileStream = File.OpenWrite(filename)
                fs.Write(byteWordDocument, 0, filelength)
            End Using
            ''works 1

        End Using

    End Sub

    Private Shared Sub LoadText(ByVal filename As String, strText As String)
        Try
            ''File.Delete(filename)
            'Dim utf As New UTF8Encoding
            'Dim content As Byte() = utf.GetBytes(strText)

            'Using fs As FileStream = New FileStream(filename, FileMode.Append, FileAccess.Write)

            '    fs.Write(content, 0, content.Length)

            'End Using
            Using myDocument As WordprocessingDocument = WordprocessingDocument.Open(filename, True)

                Dim p As Paragraph = New Paragraph
                Dim t As Text = New Text("hello, this is a test")
                Dim r As Run = New Run
                r.Append(t)
                p.Append(r)

                myDocument.MainDocumentPart.Document.Body.Append(p)

            End Using


        Catch ex As Exception
            Console.Write(ex.ToString)
        End Try
    End Sub
End Class
