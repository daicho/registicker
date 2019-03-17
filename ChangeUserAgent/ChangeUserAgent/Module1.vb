Imports Microsoft.Win32

Module Module1

    Sub Main()
        Using regKey As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION")
            regKey.SetValue("StampRegister.exe", 11001)
        End Using
    End Sub

End Module
