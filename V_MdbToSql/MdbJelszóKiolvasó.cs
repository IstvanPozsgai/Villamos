using System;
using System.IO;
using System.Text;

namespace Villamos
{
    public static class MdbJelszóKiolvasó
    {

        public static string GetPassword(string filePath)
        {
            try
            {
                // Access 2000 dekódoló kulcsok ez a lelke az egész műveletnek, ezek nélkül nem lehet visszafejteni a jelszót
                int[] access2000Decode = {27322,14316,-10911,-1380,-12294,
                                          -6616,10031,24714,1384,13947,
                                          -7223,-20001,25931,17171,16115,
                                          13233,-4088,23417,9390,10876};

                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return "No file Selected";

                int[] retXPwd = new int[18];

                ushort mgCode;

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    //Eredeti VB6-ban be lehetett olvasni a tömböt egyetlen Get utasítással, de C#-ban ezt manuálisan kell megoldani
                    fs.Seek(66, SeekOrigin.Begin);
                    for (int i = 0; i < 18; i++)
                    {
                        // A ReadInt32 automatikusan 4 bájtot olvas és lépteti a kurzort
                        retXPwd[i] = reader.ReadInt16();
                    }

                    // A 103. pozíció (VB6) a 102. offsetnek felel meg
                    fs.Seek(102, SeekOrigin.Begin);
                    mgCode = reader.ReadUInt16();
                }

                // mgCode dekódolása a 18. indexű kulccsal 
                mgCode = (ushort)(mgCode ^ access2000Decode[18]);

                StringBuilder str2000 = new StringBuilder();
                for (int bCnt = 0; bCnt < 18; bCnt++)
                {
                    int wkCode = retXPwd[bCnt] ^ (access2000Decode[bCnt]);

                    if (wkCode < 256)
                    {
                        str2000.Append((char)wkCode);
                    }
                    else
                    {
                        str2000.Append((char)(wkCode ^ mgCode));
                    }
                }

                return str2000.ToString().Replace("\0", ""); // Null karakterek eltávolítása
            }
            catch (Exception ex)
            {
                // Hibakezelés (VB6 ErrHand megfelelője)
                Console.WriteLine("Error with opening file: " + ex.Message);
                return null;
            }

            //VB6 kód Mementóként, hogy lássuk, hogyan nézett ki eredetileg a GetPassword függvény
            //
            //        Private Function GetPassword()
            //On Error GoTo ErrHand
            //Dim Access2000Decode As Variant
            //Dim fFile       As Integer
            //Dim bCnt        As Integer
            //Dim retXPwd(17) As Integer
            //Dim wkCode      As Integer
            //Dim mgCode      As Integer
            //Access2000Decode = Array(&H6ABA, &H37EC, &HD561, &HFA9C, &HCFFA, _
            //                  & HE628, &H272F, &H608A, &H568, &H367B, _
            //                  & HE3C9, &HB1DF, &H654B, &H4313, &H3EF3, _
            //                  & H33B1, &HF008, &H5B79, &H24AE, &H2A7C)
            //If Len(File) > 0 Then
            //    fFile = FreeFile
            //    Open File For Binary As #fFile
            //        Get #fFile, 67, retXPwd
            //        Get #fFile, 103, mgCode
            //    Close #fFile
            //    mgCode = mgCode Xor Access2000Decode(18)
            //    str2000 = vbNullString
            //    For bCnt = 0 To 17
            //        wkCode = retXPwd(bCnt) Xor Access2000Decode(bCnt)
            //        If wkCode< 256 Then
            //            str2000 = str2000 & Chr(wkCode)
            //        Else
            //            str2000 = str2000 & Chr(wkCode Xor mgCode)
            //        End If
            //    Next bCnt
            //Else
            //   str2000 = "No file Selected"
            //End If
            //    Exit Function
            //ErrHand:
            //    MsgBox "Error with opening file", vbCritical, App.Title
            //End Function
        }
    }
}

