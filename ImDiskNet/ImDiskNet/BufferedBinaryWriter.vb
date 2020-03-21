﻿Namespace IO

  ''' <summary>
  ''' Buffered version of the BinaryWriter class. Writes to a MemoryStream internally and flushes and
  ''' writes out contents of MemoryStream when WriteTo() or ToArray() are called.
  ''' </summary>
  <ComVisible(False)>
  Public Class BufferedBinaryWriter
    Inherits BinaryWriter

    ''' <summary>
    ''' Creates a new instance of BufferedBinaryWriter.
    ''' </summary>
    ''' <param name="encoding">Specifies which text encoding to use.</param>
    Public Sub New(encoding As Text.Encoding)
      MyBase.New(New MemoryStream, encoding)
    End Sub

    ''' <summary>
    ''' Creates a new instance of BufferedBinaryWriter using System.Text.Encoding.Unicode text encoding.
    ''' </summary>
    Public Sub New()
      MyBase.New(New MemoryStream, Text.Encoding.Unicode)
    End Sub

    ''' <summary>
    ''' Writes current contents of internal MemoryStream to another stream and resets
    ''' this BufferedBinaryWriter to empty state.
    ''' </summary>
    ''' <param name="stream">Stream object to write out data to. Flush() is automatically called on specified Stream
    ''' after writing.</param>
    Public Sub WriteTo(stream As Stream)
      Flush()
      With DirectCast(BaseStream, MemoryStream)
        .WriteTo(stream)
        .SetLength(0)
        .Position = 0
      End With
      stream.Flush()
    End Sub

    ''' <summary>
    ''' Extracts current contents of internal MemoryStream to a new byte array and resets
    ''' this BufferedBinaryWriter to empty state.
    ''' </summary>
    Public Function ToArray() As Byte()
      Flush()
      With DirectCast(BaseStream, MemoryStream)
        ToArray = .ToArray()
        .SetLength(0)
        .Position = 0
      End With
    End Function

    ''' <summary>
    ''' Clears contents of internal MemoryStream.
    ''' </summary>
    Public Sub Clear()
      If m_Disposed = True Then
        Return
      End If

      With DirectCast(BaseStream, MemoryStream)
        .SetLength(0)
        .Position = 0
      End With
    End Sub

    Protected m_Disposed As Boolean

    Protected Overrides Sub Dispose(disposing As Boolean)
      m_Disposed = True
      MyBase.Dispose(disposing)
    End Sub

  End Class

End Namespace
