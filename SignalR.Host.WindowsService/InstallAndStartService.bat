%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil /LogToConsole=true "SignalR.Host.WindowsService.exe"
NET START "SignalRHostWindowsService"
sc config ServiceTest start= auto
pause
